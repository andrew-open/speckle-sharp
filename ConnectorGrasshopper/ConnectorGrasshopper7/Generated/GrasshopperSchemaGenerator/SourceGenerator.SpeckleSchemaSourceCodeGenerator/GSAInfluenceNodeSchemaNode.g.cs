﻿//<auto-generated/>
using System;
using System.Linq;
using Grasshopper.Kernel;
using ConnectorGrasshopperUtils;
using ConnectorGrasshopper;

namespace ConnectorGrasshopper.SchemaNodes.AutoGenerated {
    public class GSAInfluenceNodeSchemaComponent: CreateSchemaObjectBase {
        
        static GSAInfluenceNodeSchemaComponent() {
          SpeckleGHSettings.SettingsChanged += (_, args) =>
          {
            if (!args.Key.StartsWith("Speckle2:tabs.")) return;
            var proxy = Grasshopper.Instances.ComponentServer.ObjectProxies.FirstOrDefault(p => p.Guid == internalGuid);
            if (proxy == null) return;
            proxy.Exposure = internalExposure;
          };
        }
        
        public GSAInfluenceNodeSchemaComponent(): base("GSAInfluenceBeam", "GSAInfluenceBeam", "Creates a Speckle structural node influence effect for GSA (for an influence analysis)", "GSA", "Bridge"){}
        
        internal static string internalCategory => "Speckle 2 Autogenerated";
        internal static Guid internalGuid => new Guid("76ec3054-fa3c-cf19-4144-5cc0f97913b6");
        internal static GH_Exposure internalExposure => SpeckleGHSettings.GetTabVisibility(internalCategory)
          ? GH_Exposure.tertiary
          : GH_Exposure.hidden;

        public override GH_Exposure Exposure => internalExposure;
        public override Guid ComponentGuid => internalGuid;

        public override void AddedToDocument(GH_Document document){
          SelectedConstructor = CSOUtils.FindConstructor("Objects.Structural.GSA.Bridge.GSAInfluenceNode.ctor(System.Int32,System.String,System.Double,Objects.Structural.GSA.Bridge.InfluenceType,Objects.Structural.Loading.LoadDirection,Objects.Structural.Geometry.Node,Objects.Structural.Geometry.Axis)", "Objects.Structural.GSA.Bridge.GSAInfluenceNode");
          base.AddedToDocument(document);
        }
  }
}
