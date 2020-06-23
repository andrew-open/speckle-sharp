﻿using Speckle.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Speckle.Elements.Geometry
{
  public class Block : Geometry
  {
    public string description { get; set; }
    public List<Base> objects { get; set; }
    public Block()
    {

    }
  }
}
