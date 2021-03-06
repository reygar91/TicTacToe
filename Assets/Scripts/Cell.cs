﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Sprite cross, nought;
    private Image image;
    private Button button;

    public enum Status { Empty, Crosses, Noughts}
    public Status value;

    GameController controller;

    public List<Line> lines = new List<Line>();

    void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        controller = FindObjectOfType<GameController>();
    }
    private void Start()
    {
        foreach (Line line in controller.lines)
        {
            foreach (Cell cell in line.cells)
            {
                if (cell == this)
                    lines.Add(line);
            }
        }
    }

    public void MakeMove() //Attached to button in Editor
    {
        DrawASign();
        CheckEndGameConditions();
    }

    private void DrawASign ()
    {
        value = controller.Player;
        switch (value)
        {
            case Status.Crosses:
                image.sprite = cross;
                break;
            case Status.Noughts:
                image.sprite = nought;
                break;
        }
        var tempColor = image.color;
        tempColor.a = 1.0f;
        image.color = tempColor;
        button.interactable = false;
        controller.emptyCells.Remove(this);
    }

    private void CheckEndGameConditions()
    {
        if (CheckVictoryCondition(value))
        {
            controller.UpdateScores(value);
            controller.EndGame.text = value.ToString() + " WINS!";
            controller.EndGame.transform.parent.gameObject.SetActive(true);
        }
        else if (controller.emptyCells.Count == 0)
        {
            controller.EndGame.text = "DRAW";
            controller.EndGame.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            PassTurnToNextPlayer();
        }
    }

    private bool CheckVictoryCondition(Status value)
    {
        bool result = false;
        foreach (Line line in lines)
        {
            result = line.CheckVictoryCondition(value);
            if (result)
                break;
        }
        return result;
    }

    private void PassTurnToNextPlayer()
    {
        if (controller.Player == Status.Crosses)
        {
            controller.Player = Status.Noughts;
            controller.toggles[1].isOn = true;
        }
        else
        {
            controller.Player = Status.Crosses;
            controller.toggles[0].isOn = true;
        }
    }

    public void AiTurn() //Attached to button in Editor
    { 
        if (controller.EndGame.transform.parent.gameObject.activeSelf == false)
            controller.StartCoroutine("AiTurn");
    }

    public void Restart() //Called from GameController
    {
        var tempColor = image.color;
        tempColor.a = 0.0f;
        image.color = tempColor;
        value = Status.Empty;
        button.interactable = true;
    }

    public void DisablePlayerSwitch() //Attached to button in Editor
    {
        controller.toggles[0].onValueChanged.RemoveAllListeners();
        controller.toggles[0].interactable = false;
        controller.toggles[1].interactable = false;
    }
    
}
