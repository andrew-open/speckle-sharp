﻿//<auto-generated/>
using System;
using System.Linq;
using Grasshopper.Kernel;
using ConnectorGrasshopperUtils;
using ConnectorGrasshopper;

namespace ConnectorGrasshopper.SchemaNodes.AutoGenerated {
    public class AreaSchemaComponent: CreateSchemaObjectBase {
        
        static AreaSchemaComponent() {
          SpeckleGHSettings.SettingsChanged += (_, args) =>
          {
            if (!args.Key.StartsWith("Speckle2:tabs.")) return;
            var proxy = Grasshopper.Instances.ComponentServer.ObjectProxies.FirstOrDefault(p => p.Guid == internalGuid);
            if (proxy == null) return;
            proxy.Exposure = internalExposure;
          };
        }
        
        public AreaSchemaComponent(): base("Area", "Area", "Creates a Speckle area", "BIM", "Other"){}
        
        internal static string internalCategory => "Speckle 2 Autogenerated";
        internal static Guid internalGuid => new Guid("3a74ac38-2aa1-f535-1bb4-ec18a330713e");
        internal static GH_Exposure internalExposure => SpeckleGHSettings.GetTabVisibility(internalCategory)
          ? GH_Exposure.tertiary
          : GH_Exposure.hidden;

        public override GH_Exposure Exposure => internalExposure;
        public override Guid ComponentGuid => internalGuid;

        public override void AddedToDocument(GH_Document document){
          SelectedConstructor = CSOUtils.FindConstructor("Objects.BuiltElements.Area.ctor(System.String,System.String,Objects.BuiltElements.Level,Objects.Geometry.Point)", "Objects.BuiltElements.Area");
          base.AddedToDocument(document);
        }
  }
}
