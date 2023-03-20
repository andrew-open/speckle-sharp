﻿//<auto-generated/>
using System;
using System.Linq;
using Grasshopper.Kernel;
using ConnectorGrasshopperUtils;
using ConnectorGrasshopper;

namespace ConnectorGrasshopper.SchemaNodes.AutoGenerated {
    public class GSAPolylineSchemaComponent: CreateSchemaObjectBase {
        
        static GSAPolylineSchemaComponent() {
          SpeckleGHSettings.SettingsChanged += (_, args) =>
          {
            if (!args.Key.StartsWith("Speckle2:tabs.")) return;
            var proxy = Grasshopper.Instances.ComponentServer.ObjectProxies.FirstOrDefault(p => p.Guid == internalGuid);
            if (proxy == null) return;
            proxy.Exposure = internalExposure;
          };
        }
        
        public GSAPolylineSchemaComponent(): base("GSAPolyline", "GSAPolyline", "Creates a Speckle structural polyline for GSA", "GSA", "Geometry"){}
        
        internal static string internalCategory => "Speckle 2 Autogenerated";
        internal static Guid internalGuid => new Guid("6f5f6b11-99ae-e067-3ec2-d4eb740bd9f7");
        internal static GH_Exposure internalExposure => SpeckleGHSettings.GetTabVisibility(internalCategory)
          ? GH_Exposure.tertiary
          : GH_Exposure.hidden;

        public override GH_Exposure Exposure => internalExposure;
        public override Guid ComponentGuid => internalGuid;

        public override void AddedToDocument(GH_Document document){
          SelectedConstructor = CSOUtils.FindConstructor("Objects.Structural.GSA.Loading.GSAPolyline.ctor(System.String,System.Int32,System.Collections.Generic.IEnumerable`1[System.Double],System.String,Objects.Structural.GSA.Geometry.GSAGridPlane)", "Objects.Structural.GSA.Loading.GSAPolyline");
          base.AddedToDocument(document);
        }
  }
}
