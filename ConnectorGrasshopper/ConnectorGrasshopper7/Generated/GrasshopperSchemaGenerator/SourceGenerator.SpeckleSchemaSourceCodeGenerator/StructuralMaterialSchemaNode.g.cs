﻿//<auto-generated/>
using System;
using System.Linq;
using Grasshopper.Kernel;
using ConnectorGrasshopperUtils;
using ConnectorGrasshopper;

namespace ConnectorGrasshopper.SchemaNodes.AutoGenerated {
    public class StructuralMaterialSchemaComponent: CreateSchemaObjectBase {
        
        static StructuralMaterialSchemaComponent() {
          SpeckleGHSettings.SettingsChanged += (_, args) =>
          {
            if (!args.Key.StartsWith("Speckle2:tabs.")) return;
            var proxy = Grasshopper.Instances.ComponentServer.ObjectProxies.FirstOrDefault(p => p.Guid == internalGuid);
            if (proxy == null) return;
            proxy.Exposure = internalExposure;
          };
        }
        
        public StructuralMaterialSchemaComponent(): base("Structural Material", "Structural Material", "Creates a Speckle structural material", "Structural", "Materials"){}
        
        internal static string internalCategory => "Speckle 2 Autogenerated";
        internal static Guid internalGuid => new Guid("875fa507-5cae-fec4-b6f3-8b5e2a17c143");
        internal static GH_Exposure internalExposure => SpeckleGHSettings.GetTabVisibility(internalCategory)
          ? GH_Exposure.tertiary
          : GH_Exposure.hidden;

        public override GH_Exposure Exposure => internalExposure;
        public override Guid ComponentGuid => internalGuid;

        public override void AddedToDocument(GH_Document document){
          SelectedConstructor = CSOUtils.FindConstructor("Objects.Structural.Materials.StructuralMaterial.ctor(System.String,Objects.Structural.MaterialType,System.String,System.String,System.String)", "Objects.Structural.Materials.StructuralMaterial");
          base.AddedToDocument(document);
        }
  }
}
