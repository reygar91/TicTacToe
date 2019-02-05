using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="AI/OppositeCorner")]
public class OppositeCorner : AIStrategy
{
    public override bool MakeMove(GameController controller)
    {
        bool result = false;
        Cell.Status enemy = controller.Player == Cell.Status.Cross ? Cell.Status.Noun : Cell.Status.Cross;

        for (int row = 0; row < 2; row++)
        {
            for (int col = 0; col < 2; col++)
            {
                if (controller.cells[2*row, 2*col].value == enemy)
                {
                    if(controller.cells[2 * ((row + 1) % 2), 2 * ((col + 1) % 2)].value == Cell.Status.Empty)
                    {
                        controller.cells[2 * ((row + 1) % 2), 2 * ((col + 1) % 2)].DrawASign();
                        result = true;
                        row = 2; break;
                    }                    
                }                    
            }
        }
        return result;
    }
}
