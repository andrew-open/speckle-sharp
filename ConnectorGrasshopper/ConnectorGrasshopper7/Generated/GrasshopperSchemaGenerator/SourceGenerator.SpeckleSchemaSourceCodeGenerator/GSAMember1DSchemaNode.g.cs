﻿//<auto-generated/>
using System;
using System.Linq;
using Grasshopper.Kernel;
using ConnectorGrasshopperUtils;
using ConnectorGrasshopper;

namespace ConnectorGrasshopper.SchemaNodes.AutoGenerated {
    public class GSAMember1DSchemaComponent: CreateSchemaObjectBase {
        
        static GSAMember1DSchemaComponent() {
          SpeckleGHSettings.SettingsChanged += (_, args) =>
          {
            if (!args.Key.StartsWith("Speckle2:tabs.")) return;
            var proxy = Grasshopper.Instances.ComponentServer.ObjectProxies.FirstOrDefault(p => p.Guid == internalGuid);
            if (proxy == null) return;
            proxy.Exposure = internalExposure;
          };
        }
        
        public GSAMember1DSchemaComponent(): base("GSAMember1D (from local axis)", "GSAMember1D (from local axis)", "Creates a Speckle structural 1D member for GSA (from local axis)", "GSA", "Geometry"){}
        
        internal static string internalCategory => "Speckle 2 Autogenerated";
        internal static Guid internalGuid => new Guid("59484049-4494-1862-52fb-1b47846fa251");
        internal static GH_Exposure internalExposure => SpeckleGHSettings.GetTabVisibility(internalCategory)
          ? GH_Exposure.tertiary
          : GH_Exposure.hidden;

        public override GH_Exposure Exposure => internalExposure;
        public override Guid ComponentGuid => internalGuid;

        public override void AddedToDocument(GH_Document document){
          SelectedConstructor = CSOUtils.FindConstructor("Objects.Structural.GSA.Geometry.GSAMember1D.ctor(System.Int32,Objects.Geometry.Line,Objects.Structural.Properties.Property1D,Objects.Structural.Geometry.ElementType1D,Objects.Structural.Geometry.Restraint,Objects.Structural.Geometry.Restraint,Objects.Geometry.Vector,Objects.Geometry.Vector,Objects.Geometry.Plane)", "Objects.Structural.GSA.Geometry.GSAMember1D");
          base.AddedToDocument(document);
        }
  }
}
