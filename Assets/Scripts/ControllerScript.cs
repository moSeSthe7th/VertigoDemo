using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public GameObject selectedTrio;
    public GameObject gameMap;

    private bool rotateLock;
    private bool selectionLock = true;


    GameObject[] allSelectedHexagons;

    private MapFiller mapFiller;
    private MoveCounterScript moveCounterScript;

    Vector2 startingTouchPos;
    

    bool isAnyHexDestroyed = false;

    //private HexagonMatchChecker hexagonMatchChecker;

    private void Start()
    {
        mapFiller = FindObjectOfType(typeof(MapFiller)) as MapFiller;
        moveCounterScript = FindObjectOfType(typeof(MoveCounterScript)) as MoveCounterScript;
    }

    void Update()
    {

        if (Input.touchCount > 0 )
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began && selectionLock)
            {
                startingTouchPos = touch.position;
                selectionLock = false;
                if (selectedTrio.GetComponentsInChildren<Transform>().Length != 1)
                {
                    Transform[] hexSToDeselect;
                    hexSToDeselect = selectedTrio.GetComponentsInDirectChildren<Transform>();
                    Debug.Log("Deselect Hex Length = " + hexSToDeselect.Length);
                    for (int i = 0; i < hexSToDeselect.Length; i++)
                    {
                        float a;
                        float b;

                        hexSToDeselect[i].parent = gameMap.transform;
                        a = RoundToHalves(hexSToDeselect[i].position.x);
                        b = RoundToHalves(hexSToDeselect[i].position.y); ;
                        hexSToDeselect[i].position = new Vector3(a, b, 0);
                    }
                }

                GameObject[] selectedHexagons;
                Vector3[] positionVectors = new Vector3[3];
                Vector3 clickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                selectedHexagons = FindHexToRotate(clickedPos);

                float x = 0;
                float y = 0;


                for (int i = 0; i < selectedHexagons.Length; i++)
                {
                    positionVectors[i] = selectedHexagons[i].transform.position;
                    x += selectedHexagons[i].transform.position.x;
                    y += selectedHexagons[i].transform.position.y;

                    //selectedHexagons[i].GetComponent<SpriteRenderer>().color = Color.black;
                }

                selectedTrio.transform.position = new Vector3(x / 3, y / 3, 0);

                for (int i = 0; i < selectedHexagons.Length; i++)
                {
                    selectedHexagons[i].transform.parent = selectedTrio.transform;
                    selectedHexagons[i].transform.position = positionVectors[i];
                }

                selectionLock = true;
                rotateLock = true;
                allSelectedHexagons = selectedHexagons;
            }

            else if (touch.phase == TouchPhase.Moved && rotateLock)
            {
                float moveDirection = touch.position.x - startingTouchPos.x;
                if (moveDirection > 0)
                {
                    selectionLock = false;
                    StartCoroutine(RotateTrio(selectedTrio, true, allSelectedHexagons));
                    rotateLock = false;
                }
                else
                {
                    selectionLock = false;
                    StartCoroutine(RotateTrio(selectedTrio, false, allSelectedHexagons));
                    rotateLock = false;
                }
                
            }
        }

        

        #region Mouse Controls
        if (Input.GetMouseButtonDown(0) && selectionLock)
        {
            selectionLock = false;
            if(selectedTrio.GetComponentsInChildren<Transform>().Length != 1)
            {
                Transform[] hexSToDeselect;
                hexSToDeselect = selectedTrio.GetComponentsInDirectChildren<Transform>();
                Debug.Log("Deselect Hex Length = " + hexSToDeselect.Length);
                for (int i = 0; i < hexSToDeselect.Length; i++)
                {
                    float a;
                    float b;

                    hexSToDeselect[i].parent = gameMap.transform;
                    a = RoundToHalves(hexSToDeselect[i].position.x);
                    b = RoundToHalves(hexSToDeselect[i].position.y); ;
                    hexSToDeselect[i].position = new Vector3(a, b, 0);
                }
            }

            GameObject[] selectedHexagons;
            Vector3[] positionVectors = new Vector3[3];
            Vector3 clickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedHexagons = FindHexToRotate(clickedPos);

            float x = 0;
            float y = 0;

            
            for (int i = 0; i < selectedHexagons.Length; i++)
            {
                positionVectors[i] = selectedHexagons[i].transform.position;
                x += selectedHexagons[i].transform.position.x;
                y += selectedHexagons[i].transform.position.y;
                
                //selectedHexagons[i].GetComponent<SpriteRenderer>().color = Color.black;
            }

            selectedTrio.transform.position = new Vector3(x / 3, y / 3, 0);

            for (int i = 0; i < selectedHexagons.Length; i++)
            {
                selectedHexagons[i].transform.parent = selectedTrio.transform;
                selectedHexagons[i].transform.position = positionVectors[i];
            }

            selectionLock = true;
            rotateLock = true;
            allSelectedHexagons = selectedHexagons;
        }

        else if (Input.GetMouseButtonDown(1) && rotateLock)
        {
            selectionLock = false;
            StartCoroutine(RotateTrio(selectedTrio, true, allSelectedHexagons));
            
            rotateLock = false;
        }

        else if (Input.GetMouseButtonDown(2) && rotateLock)
        {
            selectionLock = false;
            StartCoroutine(RotateTrio(selectedTrio, false, allSelectedHexagons));
           
            rotateLock = false;
            
        }
        #endregion
    }

    GameObject[] FindHexToRotate(Vector3 touchedPoint)
    {
        GameObject[] closestHexagons = new GameObject[3];
        List<GameObject> hexagons = new List<GameObject>();
        hexagons.AddRange(GameObject.FindGameObjectsWithTag("Hexagon"));
        hexagons.AddRange(GameObject.FindGameObjectsWithTag("Bomb"));
        
        for(int i = 0;i <2; i++)
        {
            float distance = Mathf.Infinity;
            GameObject closestHexagon = null;

            foreach (GameObject hex in hexagons)
            {
                Vector3 diff = hex.transform.position - touchedPoint;
                float curDistance = diff.sqrMagnitude;

                if (curDistance < distance)
                {
                    closestHexagon = hex;
                    distance = curDistance;
                }

            }
            closestHexagons[i] = closestHexagon;
            hexagons.Remove(closestHexagon);
            
        }


        float x = (10000*closestHexagons[0].transform.position.x + 10000*closestHexagons[1].transform.position.x + touchedPoint.x/10000) / 20001;
        float y = (10000*closestHexagons[0].transform.position.y + 10000*closestHexagons[1].transform.position.y + touchedPoint.y/10000) / 20001;

        float dist = Mathf.Infinity;
        GameObject thirdHex = null;

        foreach (GameObject hex in hexagons)
        {
            Vector3 diff = hex.transform.position - new Vector3(x,y,0);
            float curDistance = diff.sqrMagnitude;

            if (curDistance < dist)
            {
                thirdHex = hex;
                dist = curDistance;
            }

        }
        closestHexagons[2] = thirdHex;

        return closestHexagons;
    }

    float RoundToHalves(float numToRound)
    {
        float roundedNum;
        numToRound = numToRound * 2;
        numToRound = Mathf.Round(numToRound);
        numToRound = numToRound / 2;

        return numToRound;
    }

    IEnumerator RotateTrio(GameObject trio, bool isRotatingRight, GameObject[] hexSInTrio)
    {
        isAnyHexDestroyed = false;

        if (isRotatingRight)
        {
            while (trio.transform.eulerAngles.z < 120f)
            {
                trio.transform.Rotate(0, 0, 30f);
                yield return new WaitForSecondsRealtime(0.1f);
            }
            trio.transform.eulerAngles = new Vector3(0, 0, 120f);
        }

        else
        {
            trio.transform.Rotate(0, 0, -10f);

            while (trio.transform.eulerAngles.z > 240f)
            {
                trio.transform.Rotate(0, 0, -30f);
                yield return new WaitForSecondsRealtime(0.1f);
            }
            trio.transform.eulerAngles = new Vector3(0, 0, 240f);
        }

        for (int i = 0; i < hexSInTrio.Length; i++)
        {
            float a;
            float b;

            hexSInTrio[i].transform.parent = gameMap.transform;
            a = RoundToHalves(hexSInTrio[i].transform.position.x);
            b = RoundToHalves(hexSInTrio[i].transform.position.y); ;
            hexSInTrio[i].transform.position = new Vector3(a, b, 0);
        }

        
        trio.transform.eulerAngles = new Vector3(0, 0, 0);

        
        for (int i = 0; i < 3; i++)
        {
            bool hexDestroyed = hexSInTrio[i].gameObject.GetComponent<HexagonMatchChecker>().checkMatches();
            if (hexDestroyed)
            {
                isAnyHexDestroyed = true;
                hexSInTrio[i].SetActive(false);
                
            }
                
        }

        yield return new WaitForSecondsRealtime(0.2f);

        selectionLock = true;

        if (isAnyHexDestroyed)
        {
            
            mapFiller.FillTheMap();
            DataScript.moveCount -= 1;
            moveCounterScript.SetMoveCounter();
        }
        else
        {
            for(int i = 0; i < 3; i++)
            {
                hexSInTrio[i].transform.parent = trio.transform;
            }
            
            StartCoroutine(ReverseTrio(trio, !isRotatingRight, hexSInTrio));
        }
       

        
    }

    IEnumerator ReverseTrio(GameObject trio, bool isRotatingRight, GameObject[] hexSInTrio)
    {
        
        selectionLock = false;
        
        if (isRotatingRight)
        {
            while (trio.transform.eulerAngles.z < 120f)
            {
                trio.transform.Rotate(0, 0, 30f);
                yield return new WaitForSecondsRealtime(0.1f);
            }
            trio.transform.eulerAngles = new Vector3(0, 0, 120f);
        }

        else
        {
            trio.transform.Rotate(0, 0, -10f);

            while (trio.transform.eulerAngles.z > 240f)
            {
                trio.transform.Rotate(0, 0, -30f);
                yield return new WaitForSecondsRealtime(0.1f);
            }
            trio.transform.eulerAngles = new Vector3(0, 0, 240f);
        }

        for (int i = 0; i < hexSInTrio.Length; i++)
        {
            float a;
            float b;

            hexSInTrio[i].transform.parent = gameMap.transform;
            a = RoundToHalves(hexSInTrio[i].transform.position.x);
            b = RoundToHalves(hexSInTrio[i].transform.position.y); ;
            hexSInTrio[i].transform.position = new Vector3(a, b, 0);
        }


        trio.transform.eulerAngles = new Vector3(0, 0, 0);

        DataScript.moveCount -= 1;
        moveCounterScript.SetMoveCounter();
        selectionLock = true;
    }
}
