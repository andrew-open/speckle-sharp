using System.Linq;
using Autodesk.Revit.UI;
using DUI3;
using DUI3.Bindings;
using Speckle.ConnectorRevitDUI3.Utils;
using Speckle.Core.Kits;

namespace Speckle.ConnectorRevitDUI3.Bindings;

public class SelectionBinding : ISelectionBinding
{
  public string Name { get; set; } = "selectionBinding";
  public IBridge Parent { get; set; }
  private static UIApplication RevitApp { get; set; }

  private static readonly ISpeckleConverter Converter = ConverterProvider.GetConverterInstance();
  
  public SelectionBinding()
  {
    RevitApp = RevitAppProvider.RevitApp;

    RevitApp.SelectionChanged += (_,_) => RevitIdleManager.SubscribeToIdle(OnSelectionChanged);
    RevitApp.ViewActivated += (_, _) =>
    {
      Parent?.SendToBrowser(SelectionBindingEvents.SetSelection, new SelectionInfo());
    };
  }

  private void OnSelectionChanged()
  {
    var selectionInfo = GetSelection();
    Parent?.SendToBrowser(SelectionBindingEvents.SetSelection, selectionInfo);
  }

  public SelectionInfo GetSelection()
  {
    var allElements = RevitApp.ActiveUIDocument.Selection.GetElementIds().Select(id => RevitApp.ActiveUIDocument.Document.GetElement(id)).ToList(); 
    var supportedElements = allElements.Where(el => Converter.CanConvertToSpeckle(el)).ToList();
    
    var cats = supportedElements.Select(el => el.Category?.Name ?? el.Name).Distinct().ToList();
    var ids = supportedElements.Select(el => el.Id.IntegerValue.ToString()).ToList();

    if (ids.Count < allElements.Count)
    {
      
    }
    
    return new SelectionInfo()
    {
      SelectedObjectIds = ids,
      Summary = $"{supportedElements.Count} objects ({string.Join(", ", cats)})",
      Warning = ids.Count < allElements.Count ? $"{allElements.Count - ids.Count} elements are not supported." : null
    };
  }
}
