using System.Collections;
using NUnit.Framework;
using Objects.BuiltElements;
using Objects.BuiltElements.Archicad;
using Speckle.Core.Api;
using Speckle.Core.Models;
using Speckle.Core.Kits;
using Speckle.Newtonsoft.Json;
using ObjectsBeam = Objects.BuiltElements.Archicad.ArhcicadBeam;
using ArchicadBeam = Speckle.ConnectorArchicad.ArhcicadBeam;

namespace Objects.Tests.Geometry
{
  public class DeserializationOverrideTest
  {
    public static IEnumerable testSource()
    {
      yield return 1.0d;
      yield return 4.241d;
      yield return new Level("myLevel", 4.321);
      yield return new ArchicadLevel("myLevel", 4.34, 1);
    }

    public static void Test()
    {
      new DeserializationOverrideTest().Test(5.6);
    }

    [Test]
    [TestCaseSource(nameof(testSource))]
    public void Test(object level)
    {
      //Setup old objects
      ObjectsBeam old = new();
      old.level = level;

      //Emulate a receive
      string json = Operations.Serialize(old);
      var value = (ObjectsBeam)Operations.Deserialize(json);

      //Test the receive was good
      ArchicadBeam myRealBeam = (ArchicadBeam)value;
      Assert.That(myRealBeam.level is not null);
      Assert.That(myRealBeam.level.elevation is not 0);

      //Emulate a send
      json = Operations.Serialize(myRealBeam);
      value = (ObjectsBeam)Operations.Deserialize(json);

      //Test the receive was good
      myRealBeam = (ArchicadBeam)value;
      Assert.That(myRealBeam.level is not null);
      Assert.That(myRealBeam.level.elevation is not 0);
    }
  }
}

namespace Objects.BuiltElements.Archicad
{
  public class ArhcicadBeam : Base
  {
    public string name { get; set; }
    public object level { get; set; }
    
    public static explicit operator Speckle.ConnectorArchicad.ArhcicadBeam(ArhcicadBeam d)
    {
      var ret = new Speckle.ConnectorArchicad.ArhcicadBeam();
      ret.name = d.name;
      ret.applicationId = d.applicationId;

      if (d.level is double v)
      {
        ret.level = new ArchicadLevel("", v, 0);
      }

      if (d.level is Level l)
      {
        ret.level = new ArchicadLevel(l.name, l.elevation, 0);
      }

      if (d.level is ArchicadLevel al)
      {
        ret.level = al;
      }

      return ret;
    }
  }
}

namespace Speckle.ConnectorArchicad
{
  public class ArhcicadBeam : Base
  {
    public override string speckle_type => new ArhcicadBeam().speckle_type;
    public string name { get; set; }

    public ArchicadLevel level { get; set; }
  }
}

namespace Objects.BuiltElements
{
  public class Level : Base
  {
    //public List<Base> elements { get; set; }

    public Level() { }

    /// <summary>
    /// SchemaBuilder constructor for a Speckle level
    /// </summary>
    /// <param name="name"></param>
    /// <param name="elevation"></param>
    /// <remarks>Assign units when using this constructor due to <paramref name="elevation"/> param</remarks>
    [SchemaInfo("Level", "Creates a Speckle level", "BIM", "Architecture")]
    public Level(string name, double elevation)
    {
      this.name = name;
      this.elevation = elevation;
    }

    public string name { get; set; }
    public double elevation { get; set; }

    public string units { get; set; }
  }
}

namespace Objects.BuiltElements.Archicad
{
  /*
  For further informations about given the variables, visit:
  https://archicadapi.graphisoft.com/documentation/api_storytype
  */
  public class ArchicadLevel : Level
  {
    public short index { get; set; }

    public ArchicadLevel() { }

    public ArchicadLevel(string name, double elevation, short index)
    {
      this.name = name;
      this.elevation = elevation;
      this.index = index;
    }

    public ArchicadLevel(string name)
    {
      this.name = name;
    }
  }
}
