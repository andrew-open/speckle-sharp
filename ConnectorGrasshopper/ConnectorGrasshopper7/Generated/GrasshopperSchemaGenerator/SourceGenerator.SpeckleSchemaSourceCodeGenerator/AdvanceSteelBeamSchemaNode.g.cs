﻿//<auto-generated/>
using System;
using System.Linq;
using Grasshopper.Kernel;
using ConnectorGrasshopperUtils;
using ConnectorGrasshopper;

namespace ConnectorGrasshopper.SchemaNodes.AutoGenerated {
    public class AdvanceSteelBeamSchemaComponent: CreateSchemaObjectBase {
        
        static AdvanceSteelBeamSchemaComponent() {
          SpeckleGHSettings.SettingsChanged += (_, args) =>
          {
            if (!args.Key.StartsWith("Speckle2:tabs.")) return;
            var proxy = Grasshopper.Instances.ComponentServer.ObjectProxies.FirstOrDefault(p => p.Guid == internalGuid);
            if (proxy == null) return;
            proxy.Exposure = internalExposure;
          };
        }
        
        public AdvanceSteelBeamSchemaComponent(): base("AdvanceSteelBeam", "AdvanceSteelBeam", "Creates a Advance Steel beam by curve.", "Advance Steel", "Structure"){}
        
        internal static string internalCategory => "Speckle 2 Autogenerated";
        internal static Guid internalGuid => new Guid("11e99e48-054b-5469-ba74-7cdb40cc5a4c");
        internal static GH_Exposure internalExposure => SpeckleGHSettings.GetTabVisibility(internalCategory)
          ? GH_Exposure.tertiary
          : GH_Exposure.hidden;

        public override GH_Exposure Exposure => internalExposure;
        public override Guid ComponentGuid => internalGuid;

        public override void AddedToDocument(GH_Document document){
          SelectedConstructor = CSOUtils.FindConstructor("Objects.BuiltElements.AdvanceSteel.AdvanceSteelBeam.ctor(Objects.ICurve,Objects.Structural.Properties.Profiles.SectionProfile,Objects.Structural.Materials.StructuralMaterial)", "Objects.BuiltElements.AdvanceSteel.AdvanceSteelBeam");
          base.AddedToDocument(document);
        }
  }
}
