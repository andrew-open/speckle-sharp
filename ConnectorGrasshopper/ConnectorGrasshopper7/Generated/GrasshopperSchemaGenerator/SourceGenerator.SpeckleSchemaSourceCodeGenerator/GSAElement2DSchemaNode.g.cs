﻿//<auto-generated/>
using System;
using System.Linq;
using Grasshopper.Kernel;
using ConnectorGrasshopperUtils;
using ConnectorGrasshopper;

namespace ConnectorGrasshopper.SchemaNodes.AutoGenerated {
    public class GSAElement2DSchemaComponent: CreateSchemaObjectBase {
        
        static GSAElement2DSchemaComponent() {
          SpeckleGHSettings.SettingsChanged += (_, args) =>
          {
            if (!args.Key.StartsWith("Speckle2:tabs.")) return;
            var proxy = Grasshopper.Instances.ComponentServer.ObjectProxies.FirstOrDefault(p => p.Guid == internalGuid);
            if (proxy == null) return;
            proxy.Exposure = internalExposure;
          };
        }
        
        public GSAElement2DSchemaComponent(): base("GSAElement2D", "GSAElement2D", "Creates a Speckle structural 2D element for GSA", "GSA", "Geometry"){}
        
        internal static string internalCategory => "Speckle 2 Autogenerated";
        internal static Guid internalGuid => new Guid("5f639072-afcb-2284-f204-b612b6f9c234");
        internal static GH_Exposure internalExposure => SpeckleGHSettings.GetTabVisibility(internalCategory)
          ? GH_Exposure.tertiary
          : GH_Exposure.hidden;

        public override GH_Exposure Exposure => internalExposure;
        public override Guid ComponentGuid => internalGuid;

        public override void AddedToDocument(GH_Document document){
          SelectedConstructor = CSOUtils.FindConstructor("Objects.Structural.GSA.Geometry.GSAElement2D.ctor(System.Int32,System.Collections.Generic.List`1[Objects.Structural.Geometry.Node],Objects.Structural.Properties.Property2D,Objects.Structural.Geometry.ElementType2D,System.String,System.Double,System.Double,System.Int32,System.String,System.Boolean)", "Objects.Structural.GSA.Geometry.GSAElement2D");
          base.AddedToDocument(document);
        }
  }
}
