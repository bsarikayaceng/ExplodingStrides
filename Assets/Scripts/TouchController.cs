using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour
{
    public void OnMouseDown()
    {
        Tile clickedTile = GetComponent<Tile>();
        if (clickedTile != null)
        {
            print("on mouse down");
            clickedTile.OnTileClick();

        }
    }
}
