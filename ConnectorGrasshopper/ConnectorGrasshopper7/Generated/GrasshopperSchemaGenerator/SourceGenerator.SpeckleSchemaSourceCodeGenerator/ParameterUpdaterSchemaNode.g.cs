﻿//<auto-generated/>
using System;
using System.Linq;
using Grasshopper.Kernel;
using ConnectorGrasshopperUtils;
using ConnectorGrasshopper;

namespace ConnectorGrasshopper.SchemaNodes.AutoGenerated {
    public class ParameterUpdaterSchemaComponent: CreateSchemaObjectBase {
        
        static ParameterUpdaterSchemaComponent() {
          SpeckleGHSettings.SettingsChanged += (_, args) =>
          {
            if (!args.Key.StartsWith("Speckle2:tabs.")) return;
            var proxy = Grasshopper.Instances.ComponentServer.ObjectProxies.FirstOrDefault(p => p.Guid == internalGuid);
            if (proxy == null) return;
            proxy.Exposure = internalExposure;
          };
        }
        
        public ParameterUpdaterSchemaComponent(): base("ParameterUpdater", "ParameterUpdater", "Updates parameters on a Revit element by id", "Revit", "Families"){}
        
        internal static string internalCategory => "Speckle 2 Autogenerated";
        internal static Guid internalGuid => new Guid("79316354-077b-df74-8e3c-cc441e870982");
        internal static GH_Exposure internalExposure => SpeckleGHSettings.GetTabVisibility(internalCategory)
          ? GH_Exposure.tertiary
          : GH_Exposure.hidden;

        public override GH_Exposure Exposure => internalExposure;
        public override Guid ComponentGuid => internalGuid;

        public override void AddedToDocument(GH_Document document){
          SelectedConstructor = CSOUtils.FindConstructor("Objects.BuiltElements.Revit.ParameterUpdater.ctor(System.String,System.Collections.Generic.List`1[Objects.BuiltElements.Revit.Parameter])", "Objects.BuiltElements.Revit.ParameterUpdater");
          base.AddedToDocument(document);
        }
  }
}
