﻿//<auto-generated/>
using System;
using System.Linq;
using Grasshopper.Kernel;
using ConnectorGrasshopperUtils;
using ConnectorGrasshopper;

namespace ConnectorGrasshopper.SchemaNodes.AutoGenerated {
    public class CSIElement1DSchemaComponent: CreateSchemaObjectBase {
        
        static CSIElement1DSchemaComponent() {
          SpeckleGHSettings.SettingsChanged += (_, args) =>
          {
            if (!args.Key.StartsWith("Speckle2:tabs.")) return;
            var proxy = Grasshopper.Instances.ComponentServer.ObjectProxies.FirstOrDefault(p => p.Guid == internalGuid);
            if (proxy == null) return;
            proxy.Exposure = internalExposure;
          };
        }
        
        public CSIElement1DSchemaComponent(): base("Element1D (from local axis)", "Element1D (from local axis)", "Creates a Speckle CSI 1D element (from local axis)", "CSI", "Geometry"){}
        
        internal static string internalCategory => "Speckle 2 Autogenerated";
        internal static Guid internalGuid => new Guid("fda86560-0365-1fdb-5f66-f335f5b3497c");
        internal static GH_Exposure internalExposure => SpeckleGHSettings.GetTabVisibility(internalCategory)
          ? GH_Exposure.tertiary
          : GH_Exposure.hidden;

        public override GH_Exposure Exposure => internalExposure;
        public override Guid ComponentGuid => internalGuid;

        public override void AddedToDocument(GH_Document document){
          SelectedConstructor = CSOUtils.FindConstructor("Objects.Structural.CSI.Geometry.CSIElement1D.ctor(Objects.Geometry.Line,Objects.Structural.Properties.Property1D,Objects.Structural.Geometry.ElementType1D,System.String,Objects.Structural.Geometry.Restraint,Objects.Structural.Geometry.Restraint,Objects.Geometry.Vector,Objects.Geometry.Vector,Objects.Geometry.Plane,Objects.Structural.CSI.Properties.CSILinearSpring,System.Double[],Objects.Structural.CSI.Properties.DesignProcedure)", "Objects.Structural.CSI.Geometry.CSIElement1D");
          base.AddedToDocument(document);
        }
  }
}
