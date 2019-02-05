using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/EmptySide")]
public class EmptySide : AIStrategy
{
    public override bool MakeMove(GameController controller)
    {
        bool result = false;

        List<Cell> cellsToConsider = new List<Cell>();
        foreach (Cell cell in controller.cellsRaw)
        {
            cellsToConsider.Add(cell);
        }

        for (int row = 0; row < 2; row++)
        {
            for (int col = 0; col < 2; col++)
            {
                if (controller.cells[2 * row, 2 * col].value == Cell.Status.Empty)
                {
                    cellsToConsider.Remove(controller.cells[2 * row, 2 * col]);
                }
            }
        }

        cellsToConsider.Remove(controller.cells[1, 1]);

        foreach (Cell cell in cellsToConsider.ToArray())
        {
            if (cell.value != Cell.Status.Empty)
                cellsToConsider.Remove(cell);
        }

        if (cellsToConsider.Count != 0)
        {
            cellsToConsider[Random.Range(0, cellsToConsider.Count)].DrawASign();
            result = true;
        }

        return result;
    }
}
