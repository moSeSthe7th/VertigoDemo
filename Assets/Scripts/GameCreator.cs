using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCreator : MonoBehaviour
{
    public int columns;
    public int rows;

    public GameObject hexagon;
    public GameObject bomb;

    private Transform gameMapTransform;

    public List<Vector3> hexPositions;

    private Quaternion hexRotation;

    private List<GameObject> hexList;
    

    private void Start()
    {
        
        gameMapTransform = GameObject.FindWithTag("GameMap").transform;
        hexRotation = Quaternion.Euler(0, 0, 30f);
        hexList = ObjectPooler.instance.PooltheObjects(hexagon, 100, gameMapTransform, hexRotation);
        CreateGrid();
    }

   
    Vector3 CalculateWorldPos(Vector2 gridPos)
    {
        float offset = 0;
        if(gridPos.y % 2 != 0)
        {
            offset = 0.5f;
        }

        float x = gridPos.x + offset;
        
        return (new Vector3(x, gridPos.y, 0));
    }

    void CreateGrid()
    {
        for(float x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector2 gridPlacer = new Vector2(x - (columns/2), y - rows/2);
                CreateHexInPosition(CalculateWorldPos(gridPlacer));
                /*GameObject hex = Instantiate(hexagon, CalculateWorldPos(gridPlacer), hexRotation);
                hex.transform.SetParent(gameMapTransform);*/
                hexPositions.Add(CalculateWorldPos(gridPlacer));
            }
        }
    }

    public void CreateHexInPosition(Vector3 hexPos)
    {
        GameObject hex = ObjectPooler.instance.GetPooledObject(hexList);
        hex.transform.position = hexPos;
        hex.SetActive(true);
    }

    public void CreateBombInPosition(Vector3 bombPos)
    {
        GameObject instantiatedBomb = Instantiate(bomb, bombPos, Quaternion.identity);
        instantiatedBomb.transform.SetParent(gameMapTransform);
    }
}
