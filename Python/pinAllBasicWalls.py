import clr
clr.AddReference("RevitAPI")

from Autodesk.Revit.DB import *

clr.AddReference('RevitServices')
import RevitServices
from RevitServices.Persistence import DocumentManager
from RevitServices.Transactions import TransactionManager

doc = DocumentManager.Instance.CurrentDBDocument

basicWalls = []

allWalls = FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType().ToElements()

TransactionManager.Instance.EnsureInTransaction(doc)

for wall in allWalls:

	if wall.WallType.Kind == WallKind.Basic:
		wall.Pinned = True
		basicWalls.append(wall)
		
TransactionManager.Instance.TransactionTaskDone()

OUT = basicWalls