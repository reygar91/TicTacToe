using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{

    public Sprite cross, noun;
    private Image image;
    private Button button;

    public enum Status { Empty, Cross, Noun}
    public Status value;

    GameController myGameManager;

    public List<Line> lines = new List<Line>();

    void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        myGameManager = FindObjectOfType<GameController>();
    }
    private void Start()
    {
        foreach (Line line in myGameManager.lines)
        {
            foreach (Cell cell in line.cells)
            {
                if (cell == this)
                    lines.Add(line);
            }
        }
    }

    //public void Cross()
    //{
    //    image.sprite = cross;
    //    value = Status.Cross;
    //    button.interactable = false;
    //    if (CheckVictoryCondition(value))
    //    {
    //        Debug.Log(value.ToString() + " has WON!");
    //    } else
    //    {
    //        PassTurnTo(Status.Noun);
    //    }
    //}

    //public void Noun()
    //{
    //    image.sprite = noun;
    //    value = Status.Noun;
    //    button.interactable = false;
    //    if (CheckVictoryCondition(value))
    //    {
    //        Debug.Log(value.ToString() + " has WON!");
    //    }
    //    else
    //    {
    //        PassTurnTo(Status.Cross);
    //    }
    //}

    //public void PassTurnTo(Status side)
    //{
    //    myGameManager.AiTurn(side);
    //}

    private bool CheckVictoryCondition(Status value)
    {
        bool result = false;
        foreach(Line line in lines)
        {
            result = line.CheckVictoryCondition(value);
            if (result)
                break;
        }
        return result;
    }

    public void DrawASign ()
    {
        value = myGameManager.Player;
        //value = input;
        switch (value)
        {
            case Status.Cross:
                image.sprite = cross;
                break;
            case Status.Noun:
                image.sprite = noun;
                break;
        }
        button.interactable = false;
        if (CheckVictoryCondition(value))
        {
            Debug.Log(value.ToString() + " has WON!");
        }
        else
        {
            if (myGameManager.Player == Status.Cross)
                myGameManager.Player = Status.Noun;
            else
                myGameManager.Player = Status.Cross;
        }
    }

    public void AiTurn()
    {
        myGameManager.AiTurn();
    }

    
}
