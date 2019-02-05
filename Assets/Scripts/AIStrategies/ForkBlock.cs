﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/ForkBlock")]
public class ForkBlock : AIStrategy
{
    private List<Line> potentialEnemyLines;
    private List<Cell> potentialEnemyCells;
    private List<Line> eligibleLines;
    private List<Cell> eligibleCells;

    public override bool MakeMove(GameController controller)
    {
        bool result = false;

        Cell.Status enemy;
        if (controller.Player == Cell.Status.Cross)
            enemy = Cell.Status.Noun;
        else enemy = Cell.Status.Cross;

        potentialEnemyLines = FindLinesWithOneOfType(enemy, controller.lines);
        FindPotentialEnemyMoves(controller.lines);

        if (potentialEnemyCells.Count > 0)
        {
            List<Cell> Move = new List<Cell>();
            Move = FindPotentiallyBetterMoves(controller);
            Move[Random.Range(0, Move.Count)].DrawASign();
            result = true;
        }
        return result;
    }

    private List<Line> FindLinesWithOneOfType(Cell.Status cellValue, Line[] lines)
    {
        List<Line> result = new List<Line>();
        //potentialEnemyLines = new List<Line>();
        foreach (Line line in lines)
        {
            if (!line.Draw)
            {
                //int counter = 0;
                foreach (Cell item in line.cells)
                {
                    if (item.value == cellValue)
                    {
                        result.Add(line);
                        break;
                    }
                }
            }

        }
        return result;
    }

    private void FindPotentialEnemyMoves(Line[] lines)
    {
        potentialEnemyCells = new List<Cell>();
        if (potentialEnemyLines.Count > 1)
        {
            for (int i = 0; i < potentialEnemyLines.Count - 1; i++)
            {
                foreach (Cell item in potentialEnemyLines[i].cells)
                {
                    if (item.value == Cell.Status.Empty)
                    {
                        for (int j = i + 1; j < potentialEnemyLines.Count; j++)
                        {
                            foreach (Cell cell in potentialEnemyLines[j].cells)
                            {
                                if (item == cell)
                                    potentialEnemyCells.Add(item);
                            }
                        }
                    }
                }
            }
        }
    }

    //private void FindPotentiallyBetterMoves(Cell.Status cellValue)
    //{
    //    eligibleCells = new List<Cell>();
    //    foreach (Cell cell in potentialEnemyCells)
    //    {
    //        eligibleLines = FindLinesWithOneOfType(cellValue, cell.lines.ToArray());
    //        if (eligibleLines.Count > 0)
    //            eligibleCells.Add(cell);
    //    }
    //}

    private List<Cell> FindPotentiallyBetterMoves(GameController controller)
    {
        List<Line> linesWith1ofType = FindLinesWithOneOfType(controller.Player, controller.lines);

        List<Cell> cellsToConsider = new List<Cell>();

        foreach (Cell item in controller.cellsRaw)
        {
            if (item.value == Cell.Status.Empty)
                cellsToConsider.Add(item);
        }

        foreach (Line line in linesWith1ofType)
        {
            foreach (Cell cell in line.cells)
            {
                if (potentialEnemyCells.Contains(cell))
                {
                    for (int i = 0; i < line.cells.Length; i++)
                    {
                        if (i != System.Array.IndexOf(line.cells, cell))
                        {
                            cellsToConsider.Remove(line.cells[i]);
                        }
                    }
                } 

            }
        }

        List<Cell> PrefferedMove = new List<Cell>();
        foreach (Cell cell in cellsToConsider)
        {
            foreach (Line line in cell.lines)
            {
                if (linesWith1ofType.Contains(line))
                    PrefferedMove.Add(cell);
            }
        }

        if (PrefferedMove.Count > 0)
            return PrefferedMove;
        else return cellsToConsider;

    }
}