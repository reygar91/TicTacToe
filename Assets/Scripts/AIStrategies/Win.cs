using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="AI/Win")]
public class Win : AIStrategy
{
    public override bool MakeMove(GameController controller)
    {
        //cellValue = Cell.cellValue.Noun;
        bool result = false;
        foreach (Line line in controller.lines)
        {
            if (!line.Draw)
            {
                bool condition = CheckFor2InLine(line, controller.Player);
                if (condition)
                {
                    PutASign(line);
                    result= true;
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
            if (cell.value == cellValue)
                counter++;
        }
        if (counter == 2)
            return true;
        else
            return false;
    }

    private void PutASign(Line line)
    {
        foreach (Cell cell in line.cells)
        {
            if (cell.value == Cell.Status.Empty)
                cell.MakeMove();
        }
    }
}
