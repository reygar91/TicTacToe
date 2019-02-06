using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public bool Draw;
    public Cell[] cells;


    public bool CheckVictoryCondition(Cell.Status value)
    {
        bool result = false;
        if (!Draw)
        {
            int counter = 0;
            foreach (Cell cell in cells)
            {
                if (cell.value == value)
                {
                    counter++;
                }
                else if (cell.value != Cell.Status.Empty)
                {
                    Draw = true;
                    break;
                }                    
            }
            if (counter == 3)
                result = true;
        }
        return result;
    }

}
