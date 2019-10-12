using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousePlacer : MonoBehaviour
{
    #region Private Variables

    private bool _active = false;

    private Building _currentBuilding = null;

    [SerializeField]
    private GameObject _selectionMarker = null;

    [SerializeField]
    private Renderer _selectionRenderer = null;

    private Material _goodSelection = null;

    [SerializeField]
    private Material _badSelection = null;

    #endregion

    #region MonoBehaviour Functions

    private void Start()
    {
        _goodSelection = _selectionRenderer.material;
    }

    private void Update()
    {
        if (!_active)
        {
            _selectionMarker.SetActive(false);
            return;
        }

        MapTile tile = null;
        RaycastHit hit;
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out hit))
        {
            tile = hit.collider.GetComponentInParent<MapTile>();
        }

        _selectionMarker.SetActive(null != tile);
        if (null != tile)
        {
            Vector3 positionSizeAdjust = Quaternion.Euler(45f, 0f, 45f) * Vector3.up * (float)(_currentBuilding.Size.y - 1) / 2f;
            positionSizeAdjust += Quaternion.Euler(45f, 0f, 45f) * Vector3.right * (float)(_currentBuilding.Size.x - 1) / 2f;
            _selectionMarker.transform.position = hit.collider.transform.position + Quaternion.Euler(45f, 0f, 45f) * Vector3.back * 0.1f + positionSizeAdjust;
            _selectionRenderer.material = ((tile.Occupied) ? _badSelection : _goodSelection);
        }

        if ((null != tile) && (Input.GetMouseButtonDown(0)))
        {
            if (!tile.Occupied)
            {
                tile.SpawnHouse(_currentBuilding);
                _active = false;
            }
        }
        else if (Input.GetMouseButton(1))
        {
            _active = false;
        }
    }

    #endregion

    #region Public Functions

    public void StartPlacement(Building building)
    {
        _currentBuilding = building;
        _active = true;
        _selectionMarker.transform.localScale = new Vector3(_currentBuilding.Size.x - 0.2f, _currentBuilding.Size.y - 0.2f, 0.1f);
    }

    #endregion
}
