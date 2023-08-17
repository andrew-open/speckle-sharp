using System;
using Speckle.Core.Kits;

namespace ConnectorRhinoWebUI.Utils;

public static class ConverterProvider
{
  public static readonly Type ConverterType = Speckle.Core.Kits.KitManager.GetDefaultKit().LoadConverter("Rhino7").GetType();

  public static ISpeckleConverter GetConverterInstance()
  {
    return (ISpeckleConverter)Activator.CreateInstance(ConverterType); 
  }
}
