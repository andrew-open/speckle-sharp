using System;
using System.Collections.Generic;
using System.Linq;
using ConnectorRhinoWebUI.Utils;
using DUI3;
using DUI3.Bindings;
using DUI3.Models;
using Rhino;
using Speckle.Core.Models;

namespace ConnectorRhinoWebUI.Bindings;

public class SendBinding : ISendBinding
{
  public string Name { get; set; } = "sendBinding";
  public IBridge Parent { get; set; }
  private readonly DocumentModelStore _store;

  private HashSet<string> ChangedObjectIds { get; set; } = new();
  
  public SendBinding(DocumentModelStore store)
  {
    _store = store;
    
    RhinoDoc.LayerTableEvent += (_, _) =>
    {
      Parent?.SendToBrowser(SendBindingEvents.FiltersNeedRefresh);
    };
    
    RhinoDoc.AddRhinoObject += (_, e) =>
    {
      if (!_store.IsDocumentInit) return;
      ChangedObjectIds.Add(e.ObjectId.ToString());
      RhinoIdleManager.SubscribeToIdle(RunExpirationChecks);
    };
    
    RhinoDoc.DeleteRhinoObject += (_, e) =>
    {
      if (!_store.IsDocumentInit) return;
      ChangedObjectIds.Add(e.ObjectId.ToString());
      RhinoIdleManager.SubscribeToIdle(RunExpirationChecks);
    };
    
    RhinoDoc.ReplaceRhinoObject += (_, e) =>
    {
      if (!_store.IsDocumentInit) return;
      ChangedObjectIds.Add(e.NewRhinoObject.Id.ToString());
      ChangedObjectIds.Add(e.OldRhinoObject.Id.ToString());
      RhinoIdleManager.SubscribeToIdle(RunExpirationChecks);
    }; 
  }

  public List<ISendFilter> GetSendFilters()
  {
    return new List<ISendFilter>()
    {
      new RhinoEverythingFilter(),
      new RhinoSelectionFilter(),
      new RhinoLayerFilter()
    };
  }

  public void Send(string cardId)
  {
    var model = _store.GetModelById(cardId) as SenderModelCard;
    if (model == null) return; // TODO: throw
    
    var objectIds = model.SendFilter.GetObjectIds();
    var convertedObjects = new List<Base>();

    var converter = ConverterProvider.GetConverterInstance();
    converter.SetContextDocument(RhinoDoc.ActiveDoc);
    
    var count = 0;
    foreach (var id in objectIds)
    {
      count++;
      var myObject = RhinoDoc.ActiveDoc.Objects.FindId(new Guid(id));
      try
      {
        var converted = converter.ConvertToSpeckle(myObject);
        // TODO: check if converted is null
        convertedObjects.Add(converted);
      }
      catch (Exception e)
      {
        // TODO
        // ProgressReport.AddStuff();
      }

      var args = new SenderProgressArgs()
      {
        Id= cardId,
        Status = "Converting",
        Progress = count/objectIds.Count
      };
      Parent.SendToBrowser(SendBindingEvents.SenderProgress, args );
    }

    var baseCollection = new Base();
    baseCollection["@elements"] = convertedObjects;
    //throw new System.NotImplementedException();
  }

  public void CancelSend(string modelId)
  {
    //throw new System.NotImplementedException();
  }

  public void Highlight(string modelId)
  {
    throw new System.NotImplementedException();
  }
  
  private void RunExpirationChecks()
  {
    var senders = _store.GetSenders();
    var objectIdsList = ChangedObjectIds.ToArray();
    var expiredSenderIds = new List<string>();
    
    foreach (var sender in senders)
    {
      var isExpired = sender.SendFilter.CheckExpiry(objectIdsList);
      if (isExpired)
      {
        expiredSenderIds.Add(sender.Id);
      }
    }
    Parent.SendToBrowser(SendBindingEvents.SendersExpired, expiredSenderIds);
    ChangedObjectIds = new HashSet<string>();
  }
}
