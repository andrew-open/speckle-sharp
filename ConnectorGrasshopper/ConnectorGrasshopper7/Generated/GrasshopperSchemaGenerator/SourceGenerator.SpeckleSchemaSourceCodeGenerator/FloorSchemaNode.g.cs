﻿//<auto-generated/>
using System;
using System.Linq;
using Grasshopper.Kernel;
using ConnectorGrasshopperUtils;
using ConnectorGrasshopper;

namespace ConnectorGrasshopper.SchemaNodes.AutoGenerated {
    public class FloorSchemaComponent: CreateSchemaObjectBase {
        
        static FloorSchemaComponent() {
          SpeckleGHSettings.SettingsChanged += (_, args) =>
          {
            if (!args.Key.StartsWith("Speckle2:tabs.")) return;
            var proxy = Grasshopper.Instances.ComponentServer.ObjectProxies.FirstOrDefault(p => p.Guid == internalGuid);
            if (proxy == null) return;
            proxy.Exposure = internalExposure;
          };
        }
        
        public FloorSchemaComponent(): base("Floor", "Floor", "Creates a Speckle floor", "BIM", "Architecture"){}
        
        internal static string internalCategory => "Speckle 2 Autogenerated";
        internal static Guid internalGuid => new Guid("9bf29115-ebb0-f2cb-1c5b-11eb754c830d");
        internal static GH_Exposure internalExposure => SpeckleGHSettings.GetTabVisibility(internalCategory)
          ? GH_Exposure.tertiary
          : GH_Exposure.hidden;

        public override GH_Exposure Exposure => internalExposure;
        public override Guid ComponentGuid => internalGuid;

        public override void AddedToDocument(GH_Document document){
          SelectedConstructor = CSOUtils.FindConstructor("Objects.BuiltElements.Floor.ctor(Objects.ICurve,System.Collections.Generic.List`1[Objects.ICurve],System.Collections.Generic.List`1[Speckle.Core.Models.Base])", "Objects.BuiltElements.Floor");
          base.AddedToDocument(document);
        }
  }
}
