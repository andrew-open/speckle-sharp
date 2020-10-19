﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using Speckle.Core.Api;
using Speckle.DesktopUI.Utils;
using Stylet;

namespace Speckle.DesktopUI.Streams
{
  public class StreamViewModel : Screen, IHandle<ApplicationEvent>, IHandle<StreamUpdatedEvent>
  {
    private readonly IEventAggregator _events;
    private readonly ViewManager _viewManager;
    private readonly IDialogFactory _dialogFactory;
    private readonly ConnectorBindings _bindings;
    private StreamsRepository _repo;

    public StreamViewModel(
      IEventAggregator events,
      ViewManager viewManager,
      IDialogFactory dialogFactory,
      StreamsRepository streamsRepo,
      ConnectorBindings bindings)
    {
      _events = events;
      _viewManager = viewManager;
      _dialogFactory = dialogFactory;
      _repo = streamsRepo;
      _bindings = bindings;

      _events.Subscribe(this);
    }

    public ProgressReport Progress { get; set; } = new ProgressReport();

    private StreamState _streamState;

    public StreamState StreamState
    {
      get => _streamState;
      set
      {
        SetAndNotify(ref _streamState, value);
        Stream = StreamState.Stream;
        Branch = StreamState.Stream.branches.items[ 0 ];
      }
    }

    private Stream _stream;

    public Stream Stream
    {
      get => _stream;
      set => SetAndNotify(ref _stream, value);
    }

    private Branch _branch;

    public Branch Branch
    {
      get => _branch;
      set => SetAndNotify(ref _branch, value);
    }

    public async void ConvertAndSendObjects()
    {
      StreamState.IsSending = true;

      var res = await _repo.ConvertAndSend(StreamState, Progress);
      if ( res != null )
      {
        StreamState = res;
        _events.Publish(new StreamUpdatedEvent() {StreamId = Stream.id});
      }

      StreamState.IsSending = false;
    }

    public async void ConvertAndReceiveObjects()
    {
      StreamState.IsReceiving = true;

      var res = await _repo.ConvertAndReceive(StreamState, Progress);
      if ( res == null ) return;

      StreamState = res;
      StreamState.IsReceiving = false;
    }

    public async void ShowStreamUpdateDialog(StreamState state)
    {
      var viewmodel = _dialogFactory.CreateStreamUpdateDialog();
      viewmodel.StreamState = StreamState;
      var view = _viewManager.CreateAndBindViewForModelIfNecessary(viewmodel);

      var result = await DialogHost.Show(view, "StreamDialogHost");
    }

    public async void ShowShareDialog(StreamState state)
    {
      var viewmodel = _dialogFactory.CreateShareStreamDialogViewModel();
      viewmodel.StreamState = StreamState;
      var view = _viewManager.CreateAndBindViewForModelIfNecessary(viewmodel);

      var result = await DialogHost.Show(view, "StreamDialogHost");
    }

    public void RemoveStream()
    {
      _bindings.RemoveStream(Stream.id);
      _events.Publish(new StreamRemovedEvent() {StreamId = Stream.id});
      RequestClose();
    }

    public async void DeleteStream()
    {
      try
      {
        var deleted = await StreamState.Client.StreamDelete(Stream.id);
        if ( !deleted )
        {
          // should we still remove the stream from Client if they can't delete?
          _events.Publish(new ShowNotificationEvent() {Notification = "Could not delete stream from server"});
          return;
        }

        _bindings.RemoveStream(Stream.id);
        _events.Publish(new StreamRemovedEvent() {StreamId = Stream.id});
        RequestClose();
      }
      catch ( Exception e )
      {
        _events.Publish(new ShowNotificationEvent() {Notification = $"Error: {e}"});
      }
    }

    // TODO figure out how to call this from parent instead of
    // rewriting the method here
    public void CopyStreamId(string streamId)
    {
      Clipboard.SetText(streamId);
      _events.Publish(new ShowNotificationEvent() {Notification = "Stream ID copied to clipboard!"});
    }

    public void OpenHelpLink(string url)
    {
      Link.OpenInBrowser(url);
    }

    public void Handle(ApplicationEvent message)
    {
      switch ( message.Type )
      {
        case ApplicationEvent.EventType.DocumentClosed:
        {
          RequestClose();
          return;
        }
        default:
          return;
      }
    }

    public async void Handle(StreamUpdatedEvent message)
    {
      if ( message.StreamId != StreamState.Stream.id ) return;
      StreamState.Stream = await StreamState.Client.StreamGet(StreamState.Stream.id);
      Stream = StreamState.Stream;
    }
  }
}
