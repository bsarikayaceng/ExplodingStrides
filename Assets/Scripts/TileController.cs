using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public int neighborMineCount;
    public static void PrintNeighbourCoordinates()
    {
        List<Vector2Int> neighbourCoordinates = new();

        foreach (var minePosition in GridManager.Instance.MinePositions)
        {
            AddNeighbourCoordinates(neighbourCoordinates, minePosition);
        }

        foreach (var coordinate in neighbourCoordinates)
        {
            Debug.Log("Komþu Koordinatlarý X: " + coordinate.x + ", Y: " + coordinate.y);
        }
    }

    private static void AddNeighbourCoordinates(List<Vector2Int> neighbourCoordinates, Vector2Int minePosition)
    {
        int startX = Mathf.Max(minePosition.x - 1, 0);
        int startY = Mathf.Max(minePosition.y - 1, 0);
        int endX = Mathf.Min(minePosition.x + 1, GridManager.Instance.GetGridSizeX - 1);
        int endY = Mathf.Min(minePosition.y + 1, GridManager.Instance.GetGridSizeY - 1);

        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                if (x == minePosition.x && y == minePosition.y)
                    continue; // Kendi pozisyonunu atlamak için

                Vector2Int neighbourPosition = new Vector2Int(x, y);

                if (!neighbourCoordinates.Contains(neighbourPosition) && !GridManager.Instance.MinePositions.Contains(neighbourPosition))
                {
                    neighbourCoordinates.Add(neighbourPosition);
                }
            }
        }
    }

    public int CalculateNeighborMineCount(int x, int y)
    {
        int startX = Mathf.Max(x - 1, 0);
        int startY = Mathf.Max(y - 1, 0);
        int endX = Mathf.Min(x + 1, GridManager.Instance.GetGridSizeX - 1);
        int endY = Mathf.Min(y + 1, GridManager.Instance.GetGridSizeY - 1);
        neighborMineCount = 0;

        for (int i = startX; i <= endX; i++)
        {
            for (int j = startY; j <= endY; j++)
            {
                if (i == x && j == y)
                    continue; // Kendi pozisyonunu atlamak için

                Tile neighborTile = GridManager.Instance.GetTileAtPosition(i, j);

                if (neighborTile.UnitState == UnitState.Mine)
                {
                    neighborMineCount++;
                }
            }
        }

        return neighborMineCount;
    }

}
