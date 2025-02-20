using System.Collections.Generic;
using Speckle.Core.Kits;
using Speckle.Core.Models;

namespace Objects.Geometry;

public class Extrusion : Base, IHasVolume, IHasArea, IHasBoundingBox
{
  public double? length;

  public Extrusion() { }

  public Extrusion(Base profile, double length, bool capped, string units = Units.Meters, string applicationId = null)
  {
    this.profile = profile;
    this.length = length;
    this.capped = capped;
    this.applicationId = applicationId;
    this.units = units;
  }

  public bool? capped { get; set; }
  public Base profile { get; set; }
  public Point pathStart { get; set; }
  public Point pathEnd { get; set; }
  public Base pathCurve { get; set; }
  public Base pathTangent { get; set; }
  public List<Base> profiles { get; set; }

  public string units { get; set; }

  public double area { get; set; }

  public Box bbox { get; set; }
  public double volume { get; set; }
}
