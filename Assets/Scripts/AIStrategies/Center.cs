using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="AI/Center")]
public class Center : AIStrategy
{
    public override bool MakeMove(GameController controller)
    {
        bool result = false;
        if (controller.cells[1, 1].value == Cell.Status.Empty)
        {
            controller.cells[1, 1].MakeMove();
            result = true;
        }
        return result;
    }
}
