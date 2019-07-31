using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScore : MonoBehaviour
{
    private Text gameOverScoreText;
    

    private void OnEnable()
    {
        gameOverScoreText = GetComponent<Text>();
        gameOverScoreText.text = DataScript.point.ToString();
    }
}
