using UnityEngine;

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
