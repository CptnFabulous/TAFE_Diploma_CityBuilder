using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObject : MonoBehaviour
{
    #region Private Variables

    private Building _buildingData = null;

    [SerializeField]
    private SpriteRenderer _spriteRenderer = null;

    [SerializeField]
    private Vector2Int _coords = Vector2Int.zero;

    private MapTile _bottomTile = null;

    [SerializeField]
    private int _idx = 0;

    #endregion

    #region Public Properties

    public Building BuildingData
    {
        get { return _buildingData; }
    }

    public MapTile BottomTile
    {
        get { return _bottomTile; }
    }

    #endregion

    #region MonoBehaviour Functions

    #endregion

    #region Public Funcitons

    public void Setup(Building buildingData, MapTile tile)
    {
        _buildingData = buildingData;
        _coords = tile.Coordinates;
        _bottomTile = tile;

        _spriteRenderer.sprite = _buildingData.BuildingSprite;

        Rect spriteRect = _buildingData.BuildingSprite.rect;
        float pxPerUnit = _buildingData.BuildingSprite.pixelsPerUnit;

        float scaleAdjust = ((((buildingData.Size.x + buildingData.Size.y) / 2f) * Mathf.Sqrt(2f)) - 0.2f) / (spriteRect.width / pxPerUnit);
        _spriteRenderer.transform.localScale = new Vector3(scaleAdjust, scaleAdjust, 1f);

        int sideDifference = _buildingData.Size.x - _buildingData.Size.y;
        float sideAdjust = sideDifference * (Mathf.Sqrt(2f) / 4f);

        float heightAdjust = Mathf.Cos(Quaternion.Angle(Quaternion.Euler(45f, 0f, 45f), Quaternion.identity) * Mathf.Deg2Rad) / Mathf.Sqrt(2f);
        _spriteRenderer.transform.position += new Vector3(sideAdjust, (spriteRect.height / (2f * pxPerUnit)) * scaleAdjust - heightAdjust, 0f);

        ReorderSprites();
    }

    public void Remove()
    {
        Destroy(gameObject);
    }

    #endregion

    #region Private Static Functions

    private static void ReorderSprites()
    {
        List<BuildingObject> buildings = new List<BuildingObject>(FindObjectsOfType<BuildingObject>());

        List<int> order = BuildDAGOrder(buildings);

        for (int i = 0; i < buildings.Count; ++i)
        {
            buildings[order[i]]._spriteRenderer.sortingOrder = i;
            buildings[i]._idx = i;
        }
    }

    private static List<int> BuildDAGOrder(List<BuildingObject> buildings)
    {
        List<List<int>> dag = new List<List<int>>();

        for (int i = 0; i < buildings.Count; ++i)
        {
            dag.Add(new List<int>());
        }

        for (int i = 0; i < buildings.Count - 1; ++i)
        {
            for (int j = i + 1; j < buildings.Count; ++j)
            {
                int coverstate = BuildingCoverState(buildings[i], buildings[j]);
                
                if (1 == coverstate)
                {
                    dag[i].Add(j);
                }
                else if (-1 == coverstate)
                {
                    dag[j].Add(i);
                }
            }
        }

        List<int> order = new List<int>();
        List<bool> done = new List<bool>();

        for (int i = 0; i < buildings.Count; ++i)
        {
            done.Add(false);
        }

        for (int i = 0; i < buildings.Count; ++i)
        {
            RecursiveVisit(dag, i, order, done);
        }

        return order;
    }

    private static void RecursiveVisit(List<List<int>> dag, int idx, List<int> order, List<bool> done)
    {
        if (!done[idx])
        {
            for (int i = 0; i < dag[idx].Count; ++i)
            {
                if (!done[dag[idx][i]])
                {
                    RecursiveVisit(dag, dag[idx][i], order, done);
                }
            }

            done[idx] = true;
            order.Add(idx);
        }
    }

    private static int BuildingCoverState(BuildingObject lhs, BuildingObject rhs)
    {
        int buildingConverState = 0;

        for(int li = 0; li < lhs._buildingData.Size.x; ++li)
        {
            for (int lj = 0; lj < lhs._buildingData.Size.y; ++lj)
            {
                for (int ri = 0; ri < rhs._buildingData.Size.x; ++ri)
                {
                    for (int rj = 0; rj < rhs._buildingData.Size.y; ++rj)
                    {
                        buildingConverState += SquareCoverState(lhs._coords + new Vector2Int(li, lj), rhs._coords + new Vector2Int(ri, rj));
                    }
                }
            }
        }

        buildingConverState = ((buildingConverState > 0) ? 1 : ((buildingConverState < 0) ? -1 : 0));

        return buildingConverState;
    }

    private static int SquareCoverState(Vector2Int lhs, Vector2Int rhs)
    {
        int squareCoverState = 0;

        int lhsColumn = lhs.x - lhs.y;
        int rhsColumn = rhs.x - rhs.y;

        if (Mathf.Abs(lhsColumn - rhsColumn) <= 1)
        {
            if ((lhs.x + lhs.y) < (rhs.x + rhs.y))
            {
                squareCoverState = 1;
            }
            else if ((lhs.x + lhs.y) > (rhs.x + rhs.y))
            {
                squareCoverState = -1;
            }
        }

        return squareCoverState;
    }

    #endregion
}
