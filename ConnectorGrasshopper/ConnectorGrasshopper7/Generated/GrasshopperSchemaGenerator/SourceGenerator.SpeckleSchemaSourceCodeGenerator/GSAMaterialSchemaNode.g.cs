﻿//<auto-generated/>
using System;
using System.Linq;
using Grasshopper.Kernel;
using ConnectorGrasshopperUtils;
using ConnectorGrasshopper;

namespace ConnectorGrasshopper.SchemaNodes.AutoGenerated {
    public class GSAMaterialSchemaComponent: CreateSchemaObjectBase {
        
        static GSAMaterialSchemaComponent() {
          SpeckleGHSettings.SettingsChanged += (_, args) =>
          {
            if (!args.Key.StartsWith("Speckle2:tabs.")) return;
            var proxy = Grasshopper.Instances.ComponentServer.ObjectProxies.FirstOrDefault(p => p.Guid == internalGuid);
            if (proxy == null) return;
            proxy.Exposure = internalExposure;
          };
        }
        
        public GSAMaterialSchemaComponent(): base("GSAMaterial", "GSAMaterial", "Creates a Speckle structural material for GSA", "GSA", "Materials"){}
        
        internal static string internalCategory => "Speckle 2 Autogenerated";
        internal static Guid internalGuid => new Guid("63c50a50-79e1-e05a-ed21-e6f35ac4d397");
        internal static GH_Exposure internalExposure => SpeckleGHSettings.GetTabVisibility(internalCategory)
          ? GH_Exposure.tertiary
          : GH_Exposure.hidden;

        public override GH_Exposure Exposure => internalExposure;
        public override Guid ComponentGuid => internalGuid;

        public override void AddedToDocument(GH_Document document){
          SelectedConstructor = CSOUtils.FindConstructor("Objects.Structural.GSA.Materials.GSAMaterial.ctor(System.Int32,System.String,Objects.Structural.MaterialType,System.String,System.String,System.String,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.String)", "Objects.Structural.GSA.Materials.GSAMaterial");
          base.AddedToDocument(document);
        }
  }
}
