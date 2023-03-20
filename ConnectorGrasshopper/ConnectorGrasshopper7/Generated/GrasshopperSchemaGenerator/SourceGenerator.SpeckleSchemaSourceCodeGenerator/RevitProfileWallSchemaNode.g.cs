﻿//<auto-generated/>
using System;
using System.Linq;
using Grasshopper.Kernel;
using ConnectorGrasshopperUtils;
using ConnectorGrasshopper;

namespace ConnectorGrasshopper.SchemaNodes.AutoGenerated {
    public class RevitProfileWallSchemaComponent: CreateSchemaObjectBase {
        
        static RevitProfileWallSchemaComponent() {
          SpeckleGHSettings.SettingsChanged += (_, args) =>
          {
            if (!args.Key.StartsWith("Speckle2:tabs.")) return;
            var proxy = Grasshopper.Instances.ComponentServer.ObjectProxies.FirstOrDefault(p => p.Guid == internalGuid);
            if (proxy == null) return;
            proxy.Exposure = internalExposure;
          };
        }
        
        public RevitProfileWallSchemaComponent(): base("RevitWall by profile", "RevitWall by profile", "Creates a Revit wall from a profile.", "Revit", "Architecture"){}
        
        internal static string internalCategory => "Speckle 2 Autogenerated";
        internal static Guid internalGuid => new Guid("93b2f9af-ed87-1cbf-b1b0-57f780028851");
        internal static GH_Exposure internalExposure => SpeckleGHSettings.GetTabVisibility(internalCategory)
          ? GH_Exposure.tertiary
          : GH_Exposure.hidden;

        public override GH_Exposure Exposure => internalExposure;
        public override Guid ComponentGuid => internalGuid;

        public override void AddedToDocument(GH_Document document){
          SelectedConstructor = CSOUtils.FindConstructor("Objects.BuiltElements.Revit.RevitProfileWall.ctor(System.String,System.String,Objects.Geometry.Polycurve,Objects.BuiltElements.Level,Objects.BuiltElements.Revit.LocationLine,System.Boolean,System.Collections.Generic.List`1[Speckle.Core.Models.Base],System.Collections.Generic.List`1[Objects.BuiltElements.Revit.Parameter])", "Objects.BuiltElements.Revit.RevitProfileWall");
          base.AddedToDocument(document);
        }
  }
}
