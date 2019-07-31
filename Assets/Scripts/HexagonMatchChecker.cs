using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonMatchChecker : MonoBehaviour
{
    public LayerMask layerMask;

    Vector2 southEastVec;
    Vector2 northEastVec;
    Vector2 southWestVec;
    Vector2 northWestVec;

    GameObject rightNeighbour;
    GameObject leftNeighbour;
    GameObject southEastNeighbour;
    GameObject southWestNeighbour;
    GameObject northEastNeighbour;
    GameObject northWestNeighbour;

    Color rightNeighbourColor;
    Color leftNeighbourColor;
    Color southEastNeighbourColor;
    Color southWestNeighbourColor;
    Color northEastNeighbourColor;
    Color northWestNeighbourColor;

    private MapFiller mapFiller;

    void Start()
    {
        southEastVec = new Vector2(1f, -1f);
        northEastVec = new Vector2(1f, 1f);
        southWestVec = new Vector2(-1f, -1f);
        northWestVec = new Vector2(-1f, 1f);

        mapFiller = FindObjectOfType(typeof(MapFiller)) as MapFiller;
    }


    public bool checkMatches()
    {
        Color thisHexColor;
        rightNeighbour = null;
        leftNeighbour = null;
        southEastNeighbour = null;
        southWestNeighbour = null;
        northEastNeighbour = null;
        northWestNeighbour = null;

        rightNeighbourColor = new Color(0, 0, 0, 0);
        leftNeighbourColor = new Color(0, 0, 0, 0);
        southEastNeighbourColor = new Color(0, 0, 0, 0);
        southWestNeighbourColor = new Color(0, 0, 0, 0);
        northEastNeighbourColor = new Color(0, 0, 0, 0);
        northWestNeighbourColor = new Color(0, 0, 0, 0);

        thisHexColor = gameObject.GetComponent<SpriteRenderer>().color;
        GetColorsOfHexS();

        if(thisHexColor == southWestNeighbourColor && thisHexColor == leftNeighbourColor)
        {
            leftNeighbour.SetActive(false);
            southWestNeighbour.SetActive(false);


            return true;
        }
        else if(thisHexColor == leftNeighbourColor && thisHexColor == northWestNeighbourColor)
        {
            leftNeighbour.SetActive(false);
            northWestNeighbour.SetActive(false);


            return true;
        }
        else if(thisHexColor == northWestNeighbourColor && thisHexColor == northEastNeighbourColor)
        {
            northWestNeighbour.SetActive(false);
            northEastNeighbour.SetActive(false);


            return true;
        }
        else if (thisHexColor == northEastNeighbourColor && thisHexColor == rightNeighbourColor)
        {
            northEastNeighbour.SetActive(false);
            rightNeighbour.SetActive(false);


            return true;
        }
        else if (thisHexColor == rightNeighbourColor && thisHexColor == southEastNeighbourColor)
        {
            rightNeighbour.SetActive(false);
            southEastNeighbour.SetActive(false);


            return true;
        }
        else if (thisHexColor == southEastNeighbourColor && thisHexColor == southWestNeighbourColor)
        {
            southEastNeighbour.SetActive(false);
            southWestNeighbour.SetActive(false);


            return true;
        }

        /*Debug.Log("This Hex: " + thisHexColor);
        Debug.Log("This Hex: " + thisHexColor + " southWestNeighbourColor: " + southWestNeighbourColor);
        Debug.Log("This Hex: " + thisHexColor + " leftNeighbourColor :" + leftNeighbourColor);
        Debug.Log("This Hex: " + thisHexColor + " northWestNeighbourColor: " + northWestNeighbourColor);
        Debug.Log("This Hex: " + thisHexColor + " northEastNeighbourColor: " + northEastNeighbourColor);
        Debug.Log("This Hex: " + thisHexColor + " rightNeighbourColor: " + rightNeighbourColor);
        Debug.Log("This Hex: " + thisHexColor + " southEastNeighbourColor: " + southEastNeighbourColor);*/

        return false;
    }

    public void GetColorsOfHexS()
    {
        RaycastHit2D raycastSouthWest = Physics2D.Raycast(transform.position + new Vector3(-0.2f, -0.85f, 0), southWestVec, 0.1f, layerMask);
        RaycastHit2D raycastSouthEast = Physics2D.Raycast(transform.position + new Vector3(0.2f, -0.85f, 0), southEastVec, 0.1f, layerMask);
        RaycastHit2D raycastNorthWest = Physics2D.Raycast(transform.position + new Vector3(-0.2f, 0.85f, 0), northWestVec, 0.1f, layerMask);
        RaycastHit2D raycastNorthEast = Physics2D.Raycast(transform.position + new Vector3(0.2f, 0.85f, 0), northEastVec, 0.1f, layerMask);
        RaycastHit2D raycastRight = Physics2D.Raycast(transform.position + new Vector3(1f, 0, 0), Vector2.right, 0.1f, layerMask);
        RaycastHit2D raycastLeft = Physics2D.Raycast(transform.position + new Vector3(-1f, 0, 0), Vector2.left, 0.1f, layerMask);

        if (raycastSouthWest.collider != null && (raycastSouthWest.collider.gameObject.tag == "Hexagon" || raycastSouthWest.collider.gameObject.tag == "Bomb"))
        {
            southWestNeighbour = raycastSouthWest.collider.gameObject;
            southWestNeighbourColor = southWestNeighbour.GetComponent<SpriteRenderer>().color;
        }
        if (raycastSouthEast.collider != null && (raycastSouthEast.collider.gameObject.tag == "Hexagon" || raycastSouthEast.collider.gameObject.tag == "Bomb"))
        {
            southEastNeighbour = raycastSouthEast.collider.gameObject;
            southEastNeighbourColor = southEastNeighbour.GetComponent<SpriteRenderer>().color;
        }
        if (raycastNorthWest.collider != null && (raycastNorthWest.collider.gameObject.tag == "Hexagon" || raycastNorthWest.collider.gameObject.tag == "Bomb"))
        {
            northWestNeighbour = raycastNorthWest.collider.gameObject;
            northWestNeighbourColor = northWestNeighbour.GetComponent<SpriteRenderer>().color;
        }
        if (raycastNorthEast.collider != null && (raycastNorthEast.collider.gameObject.tag == "Hexagon" || raycastNorthEast.collider.gameObject.tag == "Bomb"))
        {
            northEastNeighbour = raycastNorthEast.collider.gameObject;
            northEastNeighbourColor = northEastNeighbour.GetComponent<SpriteRenderer>().color;
        }
        if (raycastLeft.collider != null && (raycastLeft.collider.gameObject.tag == "Hexagon" || raycastLeft.collider.gameObject.tag == "Bomb"))
        {
            leftNeighbour = raycastLeft.collider.gameObject;
            leftNeighbourColor = leftNeighbour.GetComponent<SpriteRenderer>().color;
        }
        if (raycastRight.collider != null && (raycastRight.collider.gameObject.tag == "Hexagon" || raycastRight.collider.gameObject.tag == "Bomb"))
        {
            rightNeighbour = raycastRight.collider.gameObject;
            rightNeighbourColor = rightNeighbour.GetComponent<SpriteRenderer>().color;
        }

        
    }
}
