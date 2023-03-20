﻿//<auto-generated/>
using System;
using System.Linq;
using Grasshopper.Kernel;
using ConnectorGrasshopperUtils;
using ConnectorGrasshopper;

namespace ConnectorGrasshopper.SchemaNodes.AutoGenerated {
    public class DeckFilledSchemaComponent: CreateSchemaObjectBase {
        
        static DeckFilledSchemaComponent() {
          SpeckleGHSettings.SettingsChanged += (_, args) =>
          {
            if (!args.Key.StartsWith("Speckle2:tabs.")) return;
            var proxy = Grasshopper.Instances.ComponentServer.ObjectProxies.FirstOrDefault(p => p.Guid == internalGuid);
            if (proxy == null) return;
            proxy.Exposure = internalExposure;
          };
        }
        
        public DeckFilledSchemaComponent(): base("DeckFilled", "DeckFilled", "Create an CSI Filled Deck", "CSI", "Properties"){}
        
        internal static string internalCategory => "Speckle 2 Autogenerated";
        internal static Guid internalGuid => new Guid("6af1c57f-4bb5-ff55-4c93-3a71f3706836");
        internal static GH_Exposure internalExposure => SpeckleGHSettings.GetTabVisibility(internalCategory)
          ? GH_Exposure.tertiary
          : GH_Exposure.hidden;

        public override GH_Exposure Exposure => internalExposure;
        public override Guid ComponentGuid => internalGuid;

        public override void AddedToDocument(GH_Document document){
          SelectedConstructor = CSOUtils.FindConstructor("Objects.Structural.CSI.Properties.CSIProperty2D+DeckFilled.ctor(System.String,Objects.Structural.CSI.Analysis.ShellType,Objects.Structural.Materials.StructuralMaterial,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double,System.Double)", "Objects.Structural.CSI.Properties.CSIProperty2D+DeckFilled");
          base.AddedToDocument(document);
        }
  }
}
