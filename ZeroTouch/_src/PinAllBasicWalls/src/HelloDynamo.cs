using System.Collections.Generic;
using Autodesk.Revit.DB;
using Revit.Elements;
using RevitServices.Persistence;
using RevitServices.Transactions;
using Wall = Revit.Elements.Wall;

namespace PinAllBasicWalls
{
    /// <summary>
    /// Hello World basic sample
    /// </summary>
    public static class HelloDynamo
    {
        /// <summary>
        /// Prints a friendly greeting
        /// </summary>
        /// <returns>A greeting</returns>
        public static List<Revit.Elements.Wall> PinAllBasicWalls()
        {
            var doc = DocumentManager.Instance.CurrentDBDocument;

            List<Revit.Elements.Wall> basicWalls = new List<Wall>();

            var allWalls = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls)
                .WhereElementIsNotElementType().ToElements();

            TransactionManager.Instance.EnsureInTransaction(doc);

            foreach (Autodesk.Revit.DB.Wall wall in allWalls)
            {
                if (wall.WallType.Kind == WallKind.Basic)
                {
                    wall.Pinned = true;

                    Revit.Elements.Wall dynamoWall = (Revit.Elements.Wall)wall.ToDSType(true);

                    basicWalls.Add(dynamoWall); 
                }
                
            }

            TransactionManager.Instance.TransactionTaskDone();

            return basicWalls;
        }
    }
}
