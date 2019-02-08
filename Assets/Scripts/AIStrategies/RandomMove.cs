using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="AI/Random")]
public class RandomMove : AIStrategy
{
    public override bool MakeMove(GameController controller)
    {
        bool result = false;
        List<Cell> cellsToConsider = new List<Cell>();

        foreach (Cell cell in controller.cells)
        {
            if (cell.value == Cell.Status.Empty)
                cellsToConsider.Add(cell);
        }

        if (cellsToConsider.Count != 0)
        {
            cellsToConsider[Random.Range(0, cellsToConsider.Count)].MakeMove();
            result = true;
        }

        return result;
    }
}
