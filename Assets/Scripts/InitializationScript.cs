using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationScript : MonoBehaviour
{
    public int moves;
    public Color[] hexColors;

    private void Awake()
    {
        DataScript.point = 0;
        DataScript.moveCount = moves;
        DataScript.hexColors = hexColors;
    }
}
