﻿//<auto-generated/>
using System;
using System.Linq;
using Grasshopper.Kernel;
using ConnectorGrasshopperUtils;
using ConnectorGrasshopper;

namespace ConnectorGrasshopper.SchemaNodes.AutoGenerated {
    public class ModelSettingsSchemaComponent: CreateSchemaObjectBase {
        
        static ModelSettingsSchemaComponent() {
          SpeckleGHSettings.SettingsChanged += (_, args) =>
          {
            if (!args.Key.StartsWith("Speckle2:tabs.")) return;
            var proxy = Grasshopper.Instances.ComponentServer.ObjectProxies.FirstOrDefault(p => p.Guid == internalGuid);
            if (proxy == null) return;
            proxy.Exposure = internalExposure;
          };
        }
        
        public ModelSettingsSchemaComponent(): base("ModelSettings", "ModelSettings", "Creates a Speckle object which describes design and analysis settings for the structural model", "Structural", "Analysis"){}
        
        internal static string internalCategory => "Speckle 2 Autogenerated";
        internal static Guid internalGuid => new Guid("9586ea39-768b-39a5-016a-e49c6e23f64f");
        internal static GH_Exposure internalExposure => SpeckleGHSettings.GetTabVisibility(internalCategory)
          ? GH_Exposure.tertiary
          : GH_Exposure.hidden;

        public override GH_Exposure Exposure => internalExposure;
        public override Guid ComponentGuid => internalGuid;

        public override void AddedToDocument(GH_Document document){
          SelectedConstructor = CSOUtils.FindConstructor("Objects.Structural.Analysis.ModelSettings.ctor(Objects.Structural.Analysis.ModelUnits,System.String,System.String,System.Double)", "Objects.Structural.Analysis.ModelSettings");
          base.AddedToDocument(document);
        }
  }
}
