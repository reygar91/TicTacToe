using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/Fork")]
public class Fork : AIStrategy
{
    private List<Line> eligibleLines;
    private List<Cell> eligibleCells;

    public override bool MakeMove(GameController controller)
    {
        bool result = false;
        FindForkableLines(controller.Player, controller.lines);
        FindPotentialMoves(controller.lines);
        
        if (eligibleCells.Count > 0)
        {
            eligibleCells[Random.Range(0, eligibleCells.Count)].MakeMove();
            result = true;
        }
        return result;
    }

    private void FindForkableLines(Cell.Status cellValue, Line[] lines)
    {
        eligibleLines = new List<Line>();
        foreach (Line line in lines)
        {
            if (!line.Draw)
            {
                foreach (Cell item in line.cells)
                {
                    if (item.value == cellValue)
                    {
                        eligibleLines.Add(line);
                        break;
                    }
                }
            }

        }
    }

    private void FindPotentialMoves(Line[] lines)
    {
        eligibleCells = new List<Cell>();
        if (eligibleLines.Count > 1)
        {
            for (int i = 0; i < eligibleLines.Count - 1; i++)
            {
                foreach (Cell item in eligibleLines[i].cells)
                {
                    if (item.value == Cell.Status.Empty)
                    {
                        for (int j = i + 1; j < eligibleLines.Count; j++)
                        {
                            foreach (Cell cell in eligibleLines[j].cells)
                            {
                                if (item == cell)
                                    eligibleCells.Add(item);
                            }
                        }
                    }
                }
            }
        }
    }

}