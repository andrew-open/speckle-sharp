﻿//<auto-generated/>
using System;
using System.Linq;
using Grasshopper.Kernel;
using ConnectorGrasshopperUtils;
using ConnectorGrasshopper;

namespace ConnectorGrasshopper.SchemaNodes.AutoGenerated {
    public class CSILinearSpringSchemaComponent: CreateSchemaObjectBase {
        
        static CSILinearSpringSchemaComponent() {
          SpeckleGHSettings.SettingsChanged += (_, args) =>
          {
            if (!args.Key.StartsWith("Speckle2:tabs.")) return;
            var proxy = Grasshopper.Instances.ComponentServer.ObjectProxies.FirstOrDefault(p => p.Guid == internalGuid);
            if (proxy == null) return;
            proxy.Exposure = internalExposure;
          };
        }
        
        public CSILinearSpringSchemaComponent(): base("LinearSpring", "LinearSpring", "Create an CSI LinearSpring", "CSI", "Properties"){}
        
        internal static string internalCategory => "Speckle 2 Autogenerated";
        internal static Guid internalGuid => new Guid("37440810-2853-9e74-2336-6b56d6d0c2a3");
        internal static GH_Exposure internalExposure => SpeckleGHSettings.GetTabVisibility(internalCategory)
          ? GH_Exposure.tertiary
          : GH_Exposure.hidden;

        public override GH_Exposure Exposure => internalExposure;
        public override Guid ComponentGuid => internalGuid;

        public override void AddedToDocument(GH_Document document){
          SelectedConstructor = CSOUtils.FindConstructor("Objects.Structural.CSI.Properties.CSILinearSpring.ctor(System.String,System.Double,System.Double,System.Double,System.Double,Objects.Structural.CSI.Properties.NonLinearOptions,Objects.Structural.CSI.Properties.NonLinearOptions,System.String)", "Objects.Structural.CSI.Properties.CSILinearSpring");
          base.AddedToDocument(document);
        }
  }
}
