using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CefSharp;
using CefSharp.JavascriptBinding;
using CefSharp.Wpf;
using ConnectorRhinoWebUI.App;
using ConnectorRhinoWebUI.Bindings;
using ConnectorRhinoWebUI.State;
using DUI3;
using DUI3.App;
using DUI3.Bindings;
using DUI3.State;
using Rhino;

namespace ConnectorRhinoWebUI
{
  /// <summary>
  /// Interaction logic for SpeckleWebUIPanelCef.xaml
  /// </summary>
  public partial class SpeckleWebUIPanelCef : UserControl
  {
    public SpeckleWebUIPanelCef()
    {
   
      CefSharpSettings.ConcurrentTaskExecution = true;

      InitializeComponent();
      Browser.IsBrowserInitializedChanged += Browser_IsBrowserInitializedChanged;
    }

    private void Browser_IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      if (Browser.IsBrowserInitialized)
        Browser.ShowDevTools();
      
      // All this sounds like a factory function of sorts, somewhere.

      var executeScriptAsyncMethod = (string script) => {
        Debug.WriteLine(script);
        Browser.EvaluateScriptAsync(script);
      };

      var showDevToolsMethod = () => Browser.ShowDevTools();

      RhinoDocumentState documentState = new RhinoDocumentState(RhinoDoc.ActiveDoc);
      UserState userState = new UserState();
      SpeckleState speckleState = new SpeckleState();
      RhinoAppState appState = new RhinoAppState(userState, speckleState, documentState);
      SpeckleApp app = new SpeckleApp(appState);

      var baseBindings = new BasicConnectorBindingRhino(app); // They don't need to be created here, but wherever it makes sense in the app
      var baseBindingsBridge = new DUI3.BrowserBridge(Browser, baseBindings, executeScriptAsyncMethod, showDevToolsMethod);

      var testBinding = new TestBinding();
      var testBindingBridge = new DUI3.BrowserBridge(Browser, testBinding, executeScriptAsyncMethod, showDevToolsMethod);

      // NOTE: could be moved - later - in the bridge class itself. Alternatively, we might need an abstraction that does all the work here
      // ie, takes a binding and lobs it into the browser.
      Browser.JavascriptObjectRepository.NameConverter = null;
      Browser.JavascriptObjectRepository.Register(baseBindingsBridge.FrontendBoundName, baseBindingsBridge, true);
      Browser.JavascriptObjectRepository.Register(testBindingBridge.FrontendBoundName, testBindingBridge, true);
      
      // Config bindings
      var configBindings = new ConfigBinding(app);
      var configBindingsBridge = new BrowserBridge(
        Browser,
        configBindings,
        executeScriptAsyncMethod,
        showDevToolsMethod);
      Browser.JavascriptObjectRepository.Register(configBindingsBridge.FrontendBoundName, configBindingsBridge, true);
    }
  }
}
