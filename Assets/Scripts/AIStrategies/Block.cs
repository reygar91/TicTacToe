using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/Block")]
public class Block : AIStrategy
{
    public override bool MakeMove(GameController controller)
    {
        bool result = false;
        foreach (Line line in controller.lines)
        {
            if (!line.Draw)
            {
                bool condition = CheckFor2InLine(line, controller.Player);
                if (condition)
                {
                    PutASign(line, controller.Player);
                    result = true;
                    break;
                }
            }
        }
        return result;
    }

    private bool CheckFor2InLine(Line line, Cell.Status cellValue)
    {
        int counter = 0;
        foreach (Cell cell in line.cells)
        {
            if (cell.value != cellValue && cell.value != Cell.Status.Empty)
                counter++;
        }
        if (counter >= 2)
            return true;
        else
            return false;
    }

    private void PutASign(Line line, Cell.Status cellValue)
    {
        foreach (Cell cell in line.cells)
        {
            if (cell.value == Cell.Status.Empty)
                cell.DrawASign();
        }
    }
}
