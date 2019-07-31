using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombCounter : MonoBehaviour
{
    private int bombCounter;
    private Text bombCounterText;

    private UIHandler uIHandler;

    public bool isBombNewlyCreated = true;
    
    void Start()
    {
        uIHandler = FindObjectOfType(typeof(UIHandler)) as UIHandler;
        bombCounterText = GetComponentInChildren<Text>();
        bombCounter = Random.Range(10, 20);
        SetBombCounterText();
    }

  
    void SetBombCounterText()
    {
        bombCounterText.text = bombCounter.ToString();
    }

    public void DecreaseBombTimer()
    {
        if (!isBombNewlyCreated)
        {
            bombCounter -= 1;
            SetBombCounterText();

            if (bombCounter == 0)
                uIHandler.GameOver();
        }
        else
            isBombNewlyCreated = false;
       
    }
}
