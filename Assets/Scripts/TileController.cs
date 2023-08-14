using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TileController : MonoBehaviour
{
    public int neighborMineCount;
    public static void PrintNeighbourCoordinates()
    {
        List<Vector2Int> neighbourCoordinates = new List<Vector2Int>();

        foreach (var minePosition in GridManager.minePositions)
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
        int endX = Mathf.Min(minePosition.x + 1, GridManager.gridSizeX - 1);
        int endY = Mathf.Min(minePosition.y + 1, GridManager.gridSizeY - 1);

        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                if (x == minePosition.x && y == minePosition.y)
                    continue; // Kendi pozisyonunu atlamak için

                Vector2Int neighbourPosition = new Vector2Int(x, y);

                if (!neighbourCoordinates.Contains(neighbourPosition)&& !GridManager.minePositions.Contains(neighbourPosition))
                {
                    neighbourCoordinates.Add(neighbourPosition);
                }
            }
        }
    }

    public void CalculateMineNeighborMineCounts()
    {
        foreach (var minePosition in GridManager.minePositions)
        {
            CalculateMineNeighborMineCount(minePosition.x, minePosition.y);
        }
    }

    private void CalculateMineNeighborMineCount(int x, int y)
    {
        Tile mineTile = GridManager.Instance.GetTileAtPosition(x,y);
        int startX = Mathf.Max(x - 1, 0);
        int startY = Mathf.Max(y - 1, 0);
        int endX = Mathf.Min(x + 1, GridManager.gridSizeX - 1);
        int endY = Mathf.Min(y + 1, GridManager.gridSizeY - 1);
        Debug.Log($"x, y: {x}, {y}");
        Debug.Log($"StartX: {startX}, StartY {startY}, EndX {endX},EndY {endY}");

        int neighborMineCount = 0;

        for (int i = startX; i <= endX; i++)
        {
            for (int j = startY; j <= endY; j++)
            {
                if (i == x && j == y)
                    continue; // Kendi pozisyonunu atlamak için

                Tile neighborTile = GridManager.Instance.GetTileAtPosition(x, y);
                Debug.Log($"X: {neighborTile.x},Y {neighborTile.y}");

                if (neighborTile.UnitState == UnitState.Mine)
                {
                    neighborMineCount++;
                }
            }
        }

        Debug.Log($"Mayýn ({x}, {y})'in komþuluk ettiði mayýn sayýsý: {neighborMineCount}");
    }

    public void PrintNeighborMineCounts(int x, int y)
    {
        int neighborMineCount = CalculateNeighborMineCount(x,y);

        Debug.Log($"Týklanan tile'ýn komþu olduðu mayýn sayýsý: {neighborMineCount}");
    }

    public int CalculateNeighborMineCount(int x, int y)
    {
        int startX = Mathf.Max(x - 1, 0);
        int startY = Mathf.Max(y - 1, 0);
        int endX = Mathf.Min(x + 1, GridManager.gridSizeX - 1);
        int endY = Mathf.Min(y + 1, GridManager.gridSizeY - 1);
        neighborMineCount = 0;

        for (int i = startX; i <= endX; i++)
        {
            for (int j = startY; j <= endY; j++)
            {
                if (i == x && j ==y)
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
public enum TileState
{
    Empty,
    Mine
}
