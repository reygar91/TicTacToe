using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Cell[] cellsRaw;
    public Cell[,] cells = new Cell[3, 3];

    public Line[] lines;

    public AIStrategy[] AIStrategies;

    public Cell.Status Player;

    private void Awake()
    {
        RectTransform rectT = GetComponent<RectTransform>();
        Debug.Log("height " + rectT.rect.height);
        Debug.Log("width " + rectT.rect.width);

        float newSize = Mathf.Min(rectT.rect.height, rectT.rect.width)/4;

        cellsRaw = GetComponentsInChildren<Cell>();

        for (int row=0; row<3; row++)
        {
            for (int col=0; col<3; col++)
            {
                cells[row, col] = cellsRaw[row*3+col];
                //Cell newCell = Instantiate(cellPrefub, transform);
                
                RectTransform cellRectT = cells[row, col].GetComponent<RectTransform>();
                cellRectT.sizeDelta = new Vector2(newSize, newSize);
                cellRectT.localPosition = new Vector3((col-1) * newSize, (row-1) * newSize, 0f);
            }
        }

        lines = GetComponentsInChildren<Line>();
    }

    public void AiTurn()
    {
        bool result = false;
        for (int i = 0; i< AIStrategies.Length; i++)
        {
            result = AIStrategies[i].MakeMove(this);
            if (result)
                break;
        }        
    }
    
}
