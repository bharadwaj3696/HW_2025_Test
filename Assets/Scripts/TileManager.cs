using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int width, heigth;
    private float cellSize;
    private Vector2Int origin;
    private bool[,] occupied;

    public void Initialize(int w, int h, float cellSize)
    {
        width = Mathf.Max(1, w);
        heigth = Mathf.Max(1, h);
        this.cellSize = Mathf.Max(0.001f, cellSize);
        occupied = new bool[width, heigth];
        origin = new Vector2Int(-(width / 2), -(heigth / 2));
    }

    public bool IsInside(Vector2Int g)
    {
        var idx = GridToIndex(g);
        return idx.x >= 0 && idx.x < width && idx.y >= 0 && idx.y < heigth;
    }

    public bool IsOccupied(Vector2Int g)
    {
        if (!IsInside(g))
            return true;
        var idx = GridToIndex(g);
        return occupied[idx.x, idx.y];
    }

    public void SetOccupied(Vector2Int g, bool val)
    {
        if (!IsInside(g))
            return;
        var idx = GridToIndex(g);
        occupied[idx.x, idx.y] = val;
    }

    Vector2Int GridToIndex(Vector2Int g) => new Vector2Int(g.x - origin.x, g.y - origin.y);

    public Vector3 GridToWorld(Vector2Int g) => new Vector3(g.x * cellSize, 0f, g.y * cellSize);

    public IEnumerable<Vector2Int> Get4Neighbors(Vector2Int g)
    {
        yield return g + Vector2Int.up;
        yield return g + Vector2Int.down;
        yield return g + Vector2Int.right;
        yield return g + Vector2Int.left;
    }
}
