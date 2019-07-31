using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCounterScript : MonoBehaviour
{
    private Text moveCounterText;

    private UIHandler uIHandler;

    private GameObject[] bombs;

    void Start()
    {
        uIHandler = FindObjectOfType(typeof(UIHandler)) as UIHandler;
        moveCounterText = GetComponent<Text>();
        SetMoveCounter();
    }

   
    public void SetMoveCounter()
    {
        bombs = GameObject.FindGameObjectsWithTag("Bomb");

        moveCounterText.text = DataScript.moveCount.ToString();
        if(DataScript.moveCount == 0)
        {
            uIHandler.GameOver();
        }
        else
        {
            foreach(GameObject bomb in bombs)
            {
                bomb.GetComponent<BombCounter>().DecreaseBombTimer();
            }
        }
    }
}
