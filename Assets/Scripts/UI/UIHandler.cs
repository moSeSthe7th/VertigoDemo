using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject scoreboard;
    public GameObject gameOverPanel;
    public GameObject moveCountPanel;
    public Text pointCounter;
   
    void Start()
    {
        gameOverPanel.SetActive(false);
        pointCounter.text = DataScript.point.ToString();
    }

    
    void Update()
    {
        
    }

    public void GameOver()
    {
        moveCountPanel.SetActive(false);
        scoreboard.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void AddPoints()
    {
        DataScript.point += 5;
        pointCounter.text = DataScript.point.ToString();
    }
}
