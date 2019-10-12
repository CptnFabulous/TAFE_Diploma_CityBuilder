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

    [SerializeField]
    private BuildingObject _buildingObjectPrefab = null;

    private Vector2Int _coords = Vector2Int.zero;

    #endregion

    #region Public Properties

    public bool Occupied
    {
        get { return _occupied; }
    }

    public Vector2Int Coordinates
    {
        get { return _coords; }
        set { _coords = value; }
    }

    #endregion

    #region MonoBehaviour Functions

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    #endregion

    #region Public Functions

    public void SetAsAlt()
    {
        _renderer.material = _altMat;
    }

    public void SpawnHouse(Building building)
    {
        _occupied = true;

        GameObject buildingGameObj = Instantiate<GameObject>(_buildingObjectPrefab.gameObject, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity);
        BuildingObject buildingObj = buildingGameObj.GetComponent<BuildingObject>();
        buildingObj.Setup(building, _coords);
    }

    #endregion
}
