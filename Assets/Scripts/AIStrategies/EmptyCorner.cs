using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/EmptyCorner")]
public class EmptyCorner : AIStrategy
{
    public override bool MakeMove(GameController controller)
    {
        bool result = false;

        List<Cell> cellsToConsider = new List<Cell>();

        for (int row = 0; row < 2; row++)
        {
            for (int col = 0; col < 2; col++)
            {
                if (controller.cells[2 * row, 2 * col].value == Cell.Status.Empty)
                {
                    cellsToConsider.Add(controller.cells[2 * row, 2 * col]);
                }
            }
        }

        if (cellsToConsider.Count != 0)
        {
            cellsToConsider[Random.Range(0, cellsToConsider.Count)].DrawASign();
            result = true;
        }

        return result;
    }
}
