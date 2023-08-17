using System;
using Speckle.Core.Kits;

namespace Speckle.ConnectorRevitDUI3.Utils;

public static class ConverterProvider
{
  public static readonly Type ConverterType = Core.Kits.KitManager.GetDefaultKit().LoadConverter("Revit2023").GetType();

  public static ISpeckleConverter GetConverterInstance()
  {
    return (ISpeckleConverter)Activator.CreateInstance(ConverterType); 
  }
}
