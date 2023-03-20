﻿//<auto-generated/>
using System;
using System.Linq;
using Grasshopper.Kernel;
using ConnectorGrasshopperUtils;
using ConnectorGrasshopper;

namespace ConnectorGrasshopper.SchemaNodes.AutoGenerated {
    public class TimberSchemaComponent: CreateSchemaObjectBase {
        
        static TimberSchemaComponent() {
          SpeckleGHSettings.SettingsChanged += (_, args) =>
          {
            if (!args.Key.StartsWith("Speckle2:tabs.")) return;
            var proxy = Grasshopper.Instances.ComponentServer.ObjectProxies.FirstOrDefault(p => p.Guid == internalGuid);
            if (proxy == null) return;
            proxy.Exposure = internalExposure;
          };
        }
        
        public TimberSchemaComponent(): base("Timber", "Timber", "Creates a Speckle structural material for timber (to be used in structural analysis models)", "Structural", "Materials"){}
        
        internal static string internalCategory => "Speckle 2 Autogenerated";
        internal static Guid internalGuid => new Guid("38920e11-30d6-5235-bda6-c082c442618d");
        internal static GH_Exposure internalExposure => SpeckleGHSettings.GetTabVisibility(internalCategory)
          ? GH_Exposure.tertiary
          : GH_Exposure.hidden;

        public override GH_Exposure Exposure => internalExposure;
        public override Guid ComponentGuid => internalGuid;

        public override void AddedToDocument(GH_Document document){
          SelectedConstructor = CSOUtils.FindConstructor("Objects.Structural.Materials.Timber.ctor(System.String,System.String,System.String,System.String,System.String,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double)", "Objects.Structural.Materials.Timber");
          base.AddedToDocument(document);
        }
  }
}
