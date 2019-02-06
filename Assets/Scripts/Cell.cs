using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{

    public Sprite cross, nought;
    private Image image;
    private Button button;

    public enum Status { Empty, Cross, Nought}
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
        value = controller.Player;
        //value = input;
        switch (value)
        {
            case Status.Cross:
                image.sprite = cross;
                break;
            case Status.Nought:
                image.sprite = nought;
                break;
        }
        var tempColor = image.color;
        tempColor.a = 1.0f;
        image.color = tempColor;
        button.interactable = false;
        if (CheckVictoryCondition(value))
        {
            controller.UpdateScores(value);
            controller.VictoryMessage.text = value.ToString() + " WIN!";
            controller.VictoryMessage.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            if (controller.Player == Status.Cross)
                controller.Player = Status.Nought;
            else
                controller.Player = Status.Cross;
        }
    }

    public void AiTurn()
    {
        controller.AiTurn();
    }

    public void Restart()
    {
        var tempColor = image.color;
        tempColor.a = 0.0f;
        image.color = tempColor;
        value = Status.Empty;
        button.interactable = true;
    }

}
