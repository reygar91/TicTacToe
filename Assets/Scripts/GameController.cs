using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Cell[,] cells = new Cell[3, 3];
    public List<Cell> emptyCells = new List<Cell>();

    public Line[] lines;

    public AIDifficulty[] AIDifficulties;
    public AIDifficulty Difficulty;

    public Cell.Status Player;

    public Toggle[] toggles;
    public Text EndGame, CrossesTxt, NoughtsTxt;

    private int CrossesScore, NoughtsScore;

    private void Awake()
    {
        toggles[0].onValueChanged.AddListener(FirstMove);

        Cell[] cellsTemp = GetComponentsInChildren<Cell>();

        for (int row=0; row<3; row++)
        {
            for (int col=0; col<3; col++)
            {
                cells[row, col] = cellsTemp[row*3+col];
                emptyCells.Add(cellsTemp[row * 3 + col]);
            }
        }

        lines = GetComponentsInChildren<Line>();
    }

    private void FirstMove(bool value)
    {
        if (!value)
        {
            StartCoroutine(AiTurn());
            toggles[0].onValueChanged.RemoveAllListeners();
            toggles[0].interactable = false;
            toggles[1].interactable = false;
        }

    }

    public void SetDifficulty(Dropdown dropdown) //Attached to Dropdown in Editor
    {
        Difficulty = AIDifficulties[dropdown.value];
    }

    public void RestartGame() //Attached to Button in Editor
    {
        emptyCells = new List<Cell>();
        foreach (Cell cell in cells)
        {
            cell.Restart();
            emptyCells.Add(cell);
        }
        foreach (Line line in lines)
        {
            line.Draw = false;
        }
        Player = Cell.Status.Crosses;
        toggles[0].isOn = true;
        toggles[0].onValueChanged.RemoveAllListeners();
        toggles[0].onValueChanged.AddListener(FirstMove);
        toggles[0].interactable = true;
        toggles[1].interactable = true;
        EndGame.transform.parent.gameObject.SetActive(false);
    }

    public void CloseApp() //Attached to Button in Editor
    {
        Application.Quit();
    }

    public IEnumerator AiTurn() //Called from Cell in AiTurn()
    {
        yield return new WaitForSeconds(0.5f);
        bool result = false;
        for (int i = 0; i < Difficulty.Moves.Length; i++)
        {
            result = Difficulty.Moves[i].MakeMove(this);
            if (result)
                break;
        }
    }

    public void UpdateScores(Cell.Status value) //Called From Cell in CheckEndGameConditions()
    {
        switch (value)
        {
            case Cell.Status.Crosses:
                CrossesScore++;
                CrossesTxt.text = CrossesScore.ToString();
                break;
            case Cell.Status.Noughts:
                NoughtsScore++;
                NoughtsTxt.text = NoughtsScore.ToString();
                break;
        }
    }

}
