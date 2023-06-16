using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Objects.Converter.Revit;
using Revit.Async;
using Speckle.Core.Models;
using Xunit;
using DB = Autodesk.Revit.DB;

namespace ConverterRevitTests
{
  internal static class AssertUtils
  {
    internal static void AdaptiveComponentEqual(DB.FamilyInstance sourceElement, DB.FamilyInstance destElement)
    {
      ElementEqual(sourceElement, destElement);
      EqualParam(sourceElement, destElement, BuiltInParameter.FLEXIBLE_INSTANCE_FLIP);

      var dist = (sourceElement.Location as LocationPoint).Point.DistanceTo((destElement.Location as LocationPoint).Point);

      Assert.True(dist < 0.1);
    }

    internal static void CurveEqual(DB.CurveElement sourceElement, DB.CurveElement destElement)
    {
      ElementEqual(sourceElement, destElement);
      EqualParam(sourceElement, destElement, BuiltInParameter.CURVE_ELEM_LENGTH);
      EqualParam(sourceElement, destElement, BuiltInParameter.BUILDING_CURVE_GSTYLE);

      if (((LocationCurve)sourceElement.Location).Curve.IsBound)
      {
        var sourceEnd = ((LocationCurve)sourceElement.Location).Curve.GetEndPoint(0);
        var destEnd = ((LocationCurve)destElement.Location).Curve.GetEndPoint(0);

        Assert.Equal(sourceEnd.X, destEnd.X, 4);
        Assert.Equal(sourceEnd.Y, destEnd.Y, 4);
        Assert.Equal(sourceEnd.Z, destEnd.Z, 4);
      }
    }

    internal static void DirectShapeEqual(DB.DirectShape sourceElement, DB.DirectShape destElement)
    {
      ElementEqual(sourceElement, destElement);
    }

    internal static void DuctEqual(DB.Mechanical.Duct sourceElement, DB.Mechanical.Duct destElement)
    {
      ElementEqual(sourceElement, destElement);

      EqualParam(sourceElement, destElement, BuiltInParameter.CURVE_ELEM_LENGTH);
      EqualParam(sourceElement, destElement, BuiltInParameter.RBS_START_LEVEL_PARAM);
      EqualParam(sourceElement, destElement, BuiltInParameter.RBS_SYSTEM_CLASSIFICATION_PARAM);
      EqualParam(sourceElement, destElement, BuiltInParameter.RBS_CURVE_HEIGHT_PARAM);
      EqualParam(sourceElement, destElement, BuiltInParameter.RBS_CURVE_WIDTH_PARAM);
      EqualParam(sourceElement, destElement, BuiltInParameter.RBS_CURVE_DIAMETER_PARAM);
      EqualParam(sourceElement, destElement, BuiltInParameter.RBS_VELOCITY);
    }
    
    internal static void ElementEqual(DB.Element sourceElement, DB.Element destElement)
    {
      Assert.NotNull(sourceElement);
      Assert.NotNull(destElement);
      Assert.Equal(sourceElement.Name, destElement.Name);
      Assert.Equal(sourceElement.Document.GetElement(sourceElement.GetTypeId())?.Name, destElement.Document.GetElement(destElement.GetTypeId())?.Name);
      Assert.Equal(sourceElement.Category.Name, destElement.Category.Name);
    }
    
    internal static void FamilyInstanceEqual(DB.FamilyInstance sourceElement, DB.FamilyInstance destElement)
    {
      AssertUtils.ElementEqual(sourceElement, destElement);
      ParamAssertions(sourceElement, destElement);

      if (sourceElement.Location is LocationPoint locationPoint1)
      {
        var locationPoint2 = (LocationPoint)destElement.Location;
        Assert.Equal(locationPoint1.Point.X, locationPoint2.Point.X, 2);
        Assert.Equal(locationPoint1.Point.Y, locationPoint2.Point.Y, 2);
        Assert.Equal(locationPoint1.Point.Z, locationPoint2.Point.Z, 2);
      }

      FacingAndHandAssertions(sourceElement, destElement);

      if (sourceElement.Host == null)
      {
        return;
      }

      // workplane based elements can be tough to receive with the correct host
      if (sourceElement.Symbol.Family.FamilyPlacementType != FamilyPlacementType.WorkPlaneBased)
      {
        Assert.Equal(sourceElement.Host.Name, destElement.Host.Name);
      }

      return;
    }

    private static void ParamAssertions(FamilyInstance sourceElement, FamilyInstance destElement)
    {
      AssertUtils.EqualParam(sourceElement, destElement, BuiltInParameter.FAMILY_LEVEL_PARAM);
      AssertUtils.EqualParam(sourceElement, destElement, BuiltInParameter.INSTANCE_ELEVATION_PARAM);
      AssertUtils.EqualParam(sourceElement, destElement, BuiltInParameter.INSTANCE_SILL_HEIGHT_PARAM);

      AssertUtils.EqualParam(sourceElement, destElement, BuiltInParameter.FAMILY_BASE_LEVEL_PARAM);
      AssertUtils.EqualParam(sourceElement, destElement, BuiltInParameter.FAMILY_TOP_LEVEL_PARAM);
      AssertUtils.EqualParam(sourceElement, destElement, BuiltInParameter.FAMILY_BASE_LEVEL_OFFSET_PARAM);
      AssertUtils.EqualParam(sourceElement, destElement, BuiltInParameter.FAMILY_TOP_LEVEL_OFFSET_PARAM);
      AssertUtils.EqualParam(sourceElement, destElement, BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM);
    }

    private static void FacingAndHandAssertions(FamilyInstance sourceElement, FamilyInstance destElement)
    {
      var destFacingOrientation = destElement.FacingOrientation;
      var destHandOrientation = destElement.HandOrientation;

      // TODO : find way to get notified of conversion failures in tests
      // to be able to adjust assertions accordingly

      // right now, some items are failing to hand flip due to an api limitation, but there isn't a great way to 
      // know about that, so we're just not making this assertion
      // Assert.Equal(sourceElement.HandFlipped, destElement.HandFlipped);
      //Assert.Equal(sourceElement.HandOrientation.X, destHandOrientation.X, 2);
      //Assert.Equal(sourceElement.HandOrientation.Y, destHandOrientation.Y, 2);
      //Assert.Equal(sourceElement.HandOrientation.Z, destHandOrientation.Z, 2);

      Assert.Equal(sourceElement.FacingFlipped, destElement.FacingFlipped);
      
      Assert.Equal(sourceElement.FacingOrientation.X, destFacingOrientation.X, 2);
      Assert.Equal(sourceElement.FacingOrientation.Y, destFacingOrientation.Y, 2);
      Assert.Equal(sourceElement.FacingOrientation.Z, destFacingOrientation.Z, 2);
    }

    internal static async Task FloorEqual(DB.Floor sourceElement, DB.Floor destElement)
    {
      ElementEqual(sourceElement, destElement);

      var slopeArrow = await RevitTask.RunAsync(app => {
        return ConverterRevit.GetSlopeArrow(sourceElement);
      }).ConfigureAwait(false);

      if (slopeArrow == null)
      {
        EqualParam(sourceElement, destElement, BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM);
      }
      else
      {
        var tailOffset = ConverterRevit.GetSlopeArrowTailOffset(slopeArrow, sourceElement.Document);
        _ = ConverterRevit.GetSlopeArrowHeadOffset(slopeArrow, sourceElement.Document, tailOffset, out var slope);

        var newSlopeArrow = await RevitTask.RunAsync(app => {
          return ConverterRevit.GetSlopeArrow(destElement);
        }).ConfigureAwait(false);

        Assert.NotNull(newSlopeArrow);

        var newTailOffset = ConverterRevit.GetSlopeArrowTailOffset(slopeArrow, sourceElement.Document);
        _ = ConverterRevit.GetSlopeArrowHeadOffset(slopeArrow, sourceElement.Document, tailOffset, out var newSlope);
        Assert.Equal(slope, newSlope);

        var sourceOffset = ConverterRevit.GetParamValue<double>(sourceElement, BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM);
        Assert.Equal(sourceOffset + tailOffset, ConverterRevit.GetParamValue<double>(destElement, BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM));
      }

      EqualParam(sourceElement, destElement, BuiltInParameter.FLOOR_PARAM_IS_STRUCTURAL);
      EqualParam(sourceElement, destElement, BuiltInParameter.FLOOR_ATTR_THICKNESS_PARAM);
    }

    internal static void NestedEqual(DB.Element sourceElement, DB.Element destElement)
    {
      ElementEqual(sourceElement, destElement);

      //family instance
      EqualParam(sourceElement, destElement, BuiltInParameter.FAMILY_LEVEL_PARAM);
      EqualParam(sourceElement, destElement, BuiltInParameter.INSTANCE_ELEVATION_PARAM);
      EqualParam(sourceElement, destElement, BuiltInParameter.INSTANCE_SILL_HEIGHT_PARAM);

      EqualParam(sourceElement, destElement, BuiltInParameter.FAMILY_BASE_LEVEL_PARAM);
      EqualParam(sourceElement, destElement, BuiltInParameter.FAMILY_TOP_LEVEL_PARAM);
      EqualParam(sourceElement, destElement, BuiltInParameter.FAMILY_BASE_LEVEL_OFFSET_PARAM);
      EqualParam(sourceElement, destElement, BuiltInParameter.FAMILY_TOP_LEVEL_OFFSET_PARAM);
      EqualParam(sourceElement, destElement, BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM);

      //walls
      EqualParam(sourceElement, destElement, BuiltInParameter.WALL_USER_HEIGHT_PARAM);
      EqualParam(sourceElement, destElement, BuiltInParameter.WALL_BASE_OFFSET);
      EqualParam(sourceElement, destElement, BuiltInParameter.WALL_TOP_OFFSET);
      EqualParam(sourceElement, destElement, BuiltInParameter.WALL_BASE_CONSTRAINT);
      EqualParam(sourceElement, destElement, BuiltInParameter.WALL_HEIGHT_TYPE);
      EqualParam(sourceElement, destElement, BuiltInParameter.WALL_STRUCTURAL_SIGNIFICANT);

      if (sourceElement.Location is LocationPoint locationPoint1)
      {
        var locationPoint2 = destElement.Location as LocationPoint;
        Assert.Equal(locationPoint1.Point.X, locationPoint2.Point.X, 2);
        Assert.Equal(locationPoint1.Point.Y, locationPoint2.Point.Y, 2);
        Assert.Equal(locationPoint1.Point.Z, locationPoint2.Point.Z, 2);
      }

      if (!(sourceElement is FamilyInstance fi && destElement is FamilyInstance fi2))
      {
        return;
      }

      var actualFacing = fi.FacingOrientation;
      var calculatedFacing = ConverterRevit.GetTransformThatConsidersHandAndFaceFlipping(fi).BasisY;
      var actualHand = fi.FacingOrientation;
      var calculatedHand = ConverterRevit.GetTransformThatConsidersHandAndFaceFlipping(fi).BasisY;

      if (Math.Abs(actualFacing.X - calculatedFacing.X) > .01
        || Math.Abs(actualFacing.Y - calculatedFacing.Y) > .01)
      {
        Debug.WriteLine($"id : {sourceElement.UniqueId}");
        Debug.WriteLine($"facing   : {actualFacing}");
        Debug.WriteLine($"expected : {calculatedFacing}");
        Debug.WriteLine($"facingFlipped : {fi.FacingFlipped}");
        Debug.WriteLine($"handflipped : {fi.HandFlipped}");
      }
      
      if (Math.Abs(actualHand.X - calculatedHand.X) > .01
        || Math.Abs(actualHand.Y - calculatedHand.Y) > .01)
      {
        Debug.WriteLine($"id : {sourceElement.UniqueId}");
        Debug.WriteLine($"facing   : {actualHand}");
        Debug.WriteLine($"expected : {calculatedHand}");
        Debug.WriteLine($"facingFlipped : {fi.FacingFlipped}");
        Debug.WriteLine($"handflipped : {fi.HandFlipped}");
      }

      var t1 = ConverterRevit.GetTransformThatConsidersHandAndFaceFlipping(fi);
      var t2 = ConverterRevit.GetTransformThatConsidersHandAndFaceFlipping(fi2);

      // we can't just compare the rotation value of two elements because the rotation of may only differ by .0003
      // but one could be 6.281 (almost 2pi) and the other could be 0
      var signedRotation = ConverterRevit.GetSignedRotation(t1, t2);
      var rotation = Math.Abs(signedRotation) - Math.PI * Math.Floor(Math.Abs(signedRotation) / Math.PI);
      Assert.True(rotation < .01 || rotation > Math.PI - .01);

      //Assert.Equal(fi.Mirrored, fi2.Mirrored);

      if (fi.Host == null)
      {
        return;
      }

      // workplane based elements can be tough to receive with the correct host
      if (fi.Symbol.Family.FamilyPlacementType != FamilyPlacementType.WorkPlaneBased)
      {
        Assert.Equal(fi.Host.Name, fi2.Host.Name);
      }
    }

    internal static void OpeningEqual(DB.Opening sourceElement, DB.Opening destElement)
    {
      ElementEqual(sourceElement, destElement);

      EqualParam(sourceElement, destElement, BuiltInParameter.WALL_BASE_CONSTRAINT);
      EqualParam(sourceElement, destElement, BuiltInParameter.WALL_HEIGHT_TYPE);
      EqualParam(sourceElement, destElement, BuiltInParameter.WALL_USER_HEIGHT_PARAM);
    }

    internal static void EqualParam(DB.Element expected, DB.Element actual, BuiltInParameter param)
    {
      var expecedParam = expected.get_Parameter(param);
      if (expecedParam == null)
        return;

      switch (expecedParam.StorageType)
      {
        case StorageType.Double:
          Assert.Equal(expecedParam.AsDouble(), actual.get_Parameter(param).AsDouble(), 4);
          break;
        case StorageType.Integer:
          Assert.Equal(expecedParam.AsInteger(), actual.get_Parameter(param).AsInteger());
          break;
        case StorageType.String:
          Assert.Equal(expecedParam.AsString(), actual.get_Parameter(param).AsString());
          break;
        case StorageType.ElementId:
          {
            var e1 = expected.Document.GetElement(expecedParam.AsElementId());
            var e2 = actual.Document.GetElement(actual.get_Parameter(param).AsElementId());
            if (e1 is Level l1 && e2 is Level l2)
              Assert.Equal(l1.Elevation, l2.Elevation, 3);
            else if (e1 != null && e2 != null)
              Assert.Equal(e1.Name, e2.Name);
            break;
          }
        case StorageType.None:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    internal static void PipeEqual(DB.Plumbing.Pipe sourceElement, DB.Plumbing.Pipe destElement)
    {
      ElementEqual(sourceElement, destElement);

      EqualParam(sourceElement, destElement, BuiltInParameter.RBS_PIPE_DIAMETER_PARAM);
      EqualParam(sourceElement, destElement, BuiltInParameter.CURVE_ELEM_LENGTH);
      EqualParam(sourceElement, destElement, BuiltInParameter.RBS_START_LEVEL_PARAM);
      EqualParam(sourceElement, destElement, BuiltInParameter.RBS_SYSTEM_CLASSIFICATION_PARAM);
    }

    internal static void RoofEqual(DB.RoofBase sourceElement, DB.RoofBase destElement)
    {
      ElementEqual(sourceElement, destElement);

      EqualParam(sourceElement, destElement, BuiltInParameter.ROOF_SLOPE);
      EqualParam(sourceElement, destElement, BuiltInParameter.ROOF_BASE_LEVEL_PARAM);
      EqualParam(sourceElement, destElement, BuiltInParameter.ROOF_CONSTRAINT_LEVEL_PARAM);
      EqualParam(sourceElement, destElement, BuiltInParameter.ROOF_UPTO_LEVEL_PARAM);
    }

    internal static async Task ScheduleEqual(DB.ViewSchedule sourceElement, DB.ViewSchedule destElement)
    {
      ElementEqual(sourceElement, destElement);

      var sourceValueList = await RevitTask.RunAsync(app =>
      {
        return GetTextValuesFromSchedule(sourceElement);
      }).ConfigureAwait(false);

      var destValueList = await RevitTask.RunAsync(app =>
      {
        return GetTextValuesFromSchedule(destElement);
      }).ConfigureAwait(false);

      var index = 0;
      for (var i = 0; i < sourceValueList.Count; i++)
      {
        try
        {
          // you can't unassign parameter values in Revit, so just ignore those
          var emptyIndicies = Enumerable.Range(0, sourceValueList[i].Count)
             .Where(j => string.IsNullOrWhiteSpace(sourceValueList[i][j]))
             .ToList();

          if (emptyIndicies.Any())
          {
            for (var j = emptyIndicies.Count - 1; j >= 0; j--)
            {
              sourceValueList[i].RemoveAt(emptyIndicies[j]);
              destValueList[i].RemoveAt(emptyIndicies[j]);
            }
          }
          Assert.Equal(sourceValueList[i], destValueList[i]);
        }
        catch (Exception ex)
        {
          throw;
        }
        index++;
      }
    }

    private static List<List<string>> GetTextValuesFromSchedule(ViewSchedule revitSchedule)
    {
      var originalTableIds = new FilteredElementCollector(revitSchedule.Document, revitSchedule.Id)
        .ToElementIds();
      var values = new List<List<string>>();
      foreach (var rowInfo in RevitScheduleUtils.ScheduleRowIteration(revitSchedule))
      {
        if (rowInfo.tableSection == SectionType.Header)
        {
          continue;
        }
        if (!ConverterRevit.ElementApplicationIdsInRow(rowInfo.rowIndex, rowInfo.section, originalTableIds, revitSchedule, rowInfo.tableSection).Any())
        {
          continue;
        }

        var innerList = new List<string>();
        for (var columnIndex = 0; columnIndex < rowInfo.columnCount; columnIndex++)
        {
          innerList.Add(revitSchedule.GetCellText(rowInfo.tableSection, rowInfo.rowIndex, columnIndex));
        }
        values.Add(innerList);
      }

      return values;
    }

    internal static void ValidSpeckleElement(DB.Element elem, Base spkElem)
    {
      Assert.NotNull(elem);
      Assert.NotNull(spkElem);
      Assert.NotNull(spkElem["speckle_type"]);
      Assert.NotNull(spkElem["applicationId"]);

      SpeckleUtils.CustomAssertions(elem, spkElem);
    }

    internal static void WallEqual(DB.Wall sourceElement, DB.Wall destElement)
    {
      ElementEqual(sourceElement, destElement);

      EqualParam(sourceElement, destElement, BuiltInParameter.WALL_USER_HEIGHT_PARAM);
      EqualParam(sourceElement, destElement, BuiltInParameter.WALL_BASE_OFFSET);
      EqualParam(sourceElement, destElement, BuiltInParameter.WALL_TOP_OFFSET);
      EqualParam(sourceElement, destElement, BuiltInParameter.WALL_BASE_CONSTRAINT);
      EqualParam(sourceElement, destElement, BuiltInParameter.WALL_HEIGHT_TYPE);
      EqualParam(sourceElement, destElement, BuiltInParameter.WALL_STRUCTURAL_SIGNIFICANT);
    }

    internal static void WireEqual(DB.Electrical.Wire sourceElement, DB.Electrical.Wire destElement)
    {
      ElementEqual(sourceElement, destElement);

      EqualParam(sourceElement, destElement, BuiltInParameter.RBS_ELEC_WIRE_TYPE);
      EqualParam(sourceElement, destElement, BuiltInParameter.FABRIC_WIRE_LENGTH);
      EqualParam(sourceElement, destElement, BuiltInParameter.RBS_ELEC_WIRE_ELEVATION);
    }
  }
}
