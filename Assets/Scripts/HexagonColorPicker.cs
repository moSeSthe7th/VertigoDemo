using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonColorPicker : MonoBehaviour
{
    private Color[] hexColors;

    void Start()
    {
        hexColors = DataScript.hexColors;
        if(hexColors!= null)
        {
            int i = hexColors.Length;
            int colorSelector = Random.Range(0, i);
            gameObject.GetComponent<SpriteRenderer>().color = hexColors[colorSelector];
        }
       
    }

}
