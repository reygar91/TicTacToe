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

    public Toggle[] toggles;
    public Text VictoryMessage, CrossesTxt, NoughtsTxt;

    private int CrossesScore, NoughtsScore;

    private void Awake()
    {
        toggles[0].onValueChanged.AddListener(FirstMove);

        cellsRaw = GetComponentsInChildren<Cell>();

        for (int row=0; row<3; row++)
        {
            for (int col=0; col<3; col++)
            {
                cells[row, col] = cellsRaw[row*3+col];
                
                RectTransform cellRectT = cells[row, col].GetComponent<RectTransform>();
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

    public void RestartGame()
    {
        foreach (Cell cell in cellsRaw)
        {
            cell.Restart();
        }
        foreach (Line line in lines)
        {
            line.Draw = false;
        }
        Player = Cell.Status.Cross;
        toggles[0].isOn = true;
        toggles[0].onValueChanged.AddListener(FirstMove);
        toggles[0].interactable = true;
        toggles[1].interactable = true;
        VictoryMessage.transform.parent.gameObject.SetActive(false);
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void FirstMove(bool value)
    {
        if (!value)
        {
            AiTurn();
            toggles[0].onValueChanged.RemoveAllListeners();
            toggles[0].interactable = false;
            toggles[1].interactable = false;
        }
        
    }

    public void UpdateScores(Cell.Status value)
    {
        switch (value)
        {
            case Cell.Status.Cross:
                CrossesScore++;
                CrossesTxt.text = CrossesScore.ToString();
                break;
            case Cell.Status.Nought:
                NoughtsScore++;
                NoughtsTxt.text = NoughtsScore.ToString();
                break;
        }
    }
    
}
