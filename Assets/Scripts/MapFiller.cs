using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFiller : MonoBehaviour
{
    private GameCreator gameCreator;

    private List<Vector3> hexList;
    private UIHandler uIHandler;

    Vector2 sizeOfOverlapBox;

    private void Start()
    {
        uIHandler = FindObjectOfType(typeof(UIHandler)) as UIHandler;
        gameCreator = FindObjectOfType(typeof(GameCreator)) as GameCreator;
        hexList = gameCreator.hexPositions;
        sizeOfOverlapBox = new Vector2(0.3f, 0.3f);
    }

    public void FillTheMap()
    {
       foreach(Vector3 posVec in hexList)
        {
            
            Vector2 posOfOverlapBox = new Vector2(posVec.x,posVec.y);
            //Collider[] colliders = Physics.OverlapSphere(posVec, 1f);
            Collider2D collider = Physics2D.OverlapBox(posOfOverlapBox, sizeOfOverlapBox, 0);

            if (collider == null)
            {
                uIHandler.AddPoints();
                if (DataScript.point % 1000 == 0)
                    gameCreator.CreateBombInPosition(posVec);
                else
                    gameCreator.CreateHexInPosition(posVec);
            }
        }
    }
}
