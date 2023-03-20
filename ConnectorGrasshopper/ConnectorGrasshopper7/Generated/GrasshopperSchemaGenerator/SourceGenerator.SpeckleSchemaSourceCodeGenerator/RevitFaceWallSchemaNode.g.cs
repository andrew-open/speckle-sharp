﻿//<auto-generated/>
using System;
using System.Linq;
using Grasshopper.Kernel;
using ConnectorGrasshopperUtils;
using ConnectorGrasshopper;

namespace ConnectorGrasshopper.SchemaNodes.AutoGenerated {
    public class RevitFaceWallSchemaComponent: CreateSchemaObjectBase {
        
        static RevitFaceWallSchemaComponent() {
          SpeckleGHSettings.SettingsChanged += (_, args) =>
          {
            if (!args.Key.StartsWith("Speckle2:tabs.")) return;
            var proxy = Grasshopper.Instances.ComponentServer.ObjectProxies.FirstOrDefault(p => p.Guid == internalGuid);
            if (proxy == null) return;
            proxy.Exposure = internalExposure;
          };
        }
        
        public RevitFaceWallSchemaComponent(): base("RevitWall by face", "RevitWall by face", "Creates a Revit wall from a surface.", "Revit", "Architecture"){}
        
        internal static string internalCategory => "Speckle 2 Autogenerated";
        internal static Guid internalGuid => new Guid("f6a554b0-71c4-b772-8f3d-8850a02e0db5");
        internal static GH_Exposure internalExposure => SpeckleGHSettings.GetTabVisibility(internalCategory)
          ? GH_Exposure.tertiary
          : GH_Exposure.hidden;

        public override GH_Exposure Exposure => internalExposure;
        public override Guid ComponentGuid => internalGuid;

        public override void AddedToDocument(GH_Document document){
          SelectedConstructor = CSOUtils.FindConstructor("Objects.BuiltElements.Revit.RevitFaceWall.ctor(System.String,System.String,Objects.Geometry.Brep,Objects.BuiltElements.Level,Objects.BuiltElements.Revit.LocationLine,System.Collections.Generic.List`1[Speckle.Core.Models.Base],System.Collections.Generic.List`1[Objects.BuiltElements.Revit.Parameter])", "Objects.BuiltElements.Revit.RevitFaceWall");
          base.AddedToDocument(document);
        }
  }
}
