using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    #region Private Variables

    [SerializeField]
    private Renderer _renderer = null;

    [SerializeField]
    private Material _altMat = null;

    private bool _occupied = false;

    private BuildingObject _occupyingBuilding = null;

    [SerializeField]
    private BuildingObject _buildingObjectPrefab = null;

    private Vector2Int _coords = Vector2Int.zero;

    private MapTile _xNeighbour = null;
    private MapTile _yNeighbour = null;

    #endregion

    #region Public Properties

    public Vector2Int Coordinates
    {
        get { return _coords; }
        set { _coords = value; }
    }

    public MapTile XNeighbour
    {
        set { _xNeighbour = value; }
    }

    public MapTile YNeighbour
    {
        set { _yNeighbour = value; }
    }

    public BuildingObject OccupyingBuilding
    {
        get { return _occupyingBuilding; }
    }

    #endregion

    #region MonoBehaviour Functions

    #endregion

    #region Public Functions

    public void SetAsAlt()
    {
        _renderer.material = _altMat;
    }

    public void SpawnHouse(Building building)
    {
        GameObject buildingGameObj = Instantiate<GameObject>(_buildingObjectPrefab.gameObject, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity);
        BuildingObject buildingObj = buildingGameObj.GetComponent<BuildingObject>();
        buildingObj.Setup(building, this);

        SetOccupied(building.Size.x, building.Size.y, buildingObj);
    }

    public void SetOccupied(int x, int y, BuildingObject building)
    {
        if ((x > 0) && (y > 0))
        {
            _occupied = true;
            _occupyingBuilding = building;

            if ((x > 1) && (null != _xNeighbour))
            {
                _xNeighbour.SetOccupied(x - 1, y, building);
            }

            if ((y > 1) && (null != _yNeighbour))
            {
                _yNeighbour.SetOccupied(1, y - 1, building);
            }
        }
    }

    public bool IsOccupied(Building building)
    {
        return IsOccupied(building.Size.x, building.Size.y);
    }

    public bool IsOccupied(int x, int y)
    {
        bool occupied = false;

        if ((x > 0) && (y > 0))
        {
            occupied = occupied || _occupied;

            if ((x > 1) && (null != _xNeighbour))
            {
                occupied = occupied || _xNeighbour.IsOccupied(x - 1, y);
            }
            else if ((x > 1) && (null == _xNeighbour))
            {
                occupied = true;
            }

            if ((y > 1) && (null != _yNeighbour))
            {
                occupied = occupied || _yNeighbour.IsOccupied(1, y - 1);
            }
            else if ((y > 1) && (null == _yNeighbour))
            {
                occupied = true;
            }
        }

        return occupied;
    }

    public void RemoveBuilding()
    {
        if (null != _occupyingBuilding)
        {
            _occupyingBuilding.BottomTile.RemoveBuildingFromBottom();
        }
    }

    #endregion

    #region Private Functions

    private void RemoveBuildingFromBottom()
    {
        BuildingObject buildingToRemove = _occupyingBuilding;
        SetUnoccupied(_occupyingBuilding.BuildingData.Size.x, _occupyingBuilding.BuildingData.Size.y);
        buildingToRemove.Remove();
    }

    private void SetUnoccupied(int x, int y)
    {
        if ((x > 0) && (y > 0))
        {
            _occupied = false;
            _occupyingBuilding = null;

            if ((x > 1) && (null != _xNeighbour))
            {
                _xNeighbour.SetUnoccupied(x - 1, y);
            }

            if ((y > 1) && (null != _yNeighbour))
            {
                _yNeighbour.SetUnoccupied(1, y - 1);
            }
        }
    }

    #endregion
}
