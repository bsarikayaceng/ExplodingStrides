using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour
{
    public void OnMouseDown()
    {
       // Debug.Log("OnClick Down");
        Tile clickedTile = GetComponent<Tile>();
        if (clickedTile != null)
        {
            print("on mouse down");
            clickedTile.OnTileClick();

        }
    }

    public void OnMouseUp()
    {
      //  Debug.Log("OnClick Up");
    }
}
