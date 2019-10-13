using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    #region Private Variables

    [SerializeField]
    private int _size = 15;

    [SerializeField]
    private MapTile _tile = null;

    #endregion

    #region MonoBehaviour Functions

    private void Start()
    {
        if ((null != _tile) && (_size > 0))
        {
            GenerateMap();
        }
    }

    #endregion

    #region Private Functions

    private void GenerateMap()
    {
        GameObject mapRoot = new GameObject("MapRoot");
        mapRoot.transform.SetParent(transform);

        MapTile[,] tiles = new MapTile[_size, _size];

        for (int i = 0; i < _size; ++i)
        {
            for (int j = 0; j < _size; ++j)
            {
                Vector3 pos = new Vector3((float)(i) - (_size * 0.5f), (float)(j) - (_size * 0.5f), 0f);
                GameObject tileObj = Instantiate<GameObject>(_tile.gameObject, pos, Quaternion.identity);
                tileObj.transform.SetParent(mapRoot.transform);

                MapTile tile = tileObj.GetComponent<MapTile>();
                tiles[i, j] = tile;

                if (((i + j) % 2) == 0)
                {
                    tile.SetAsAlt();
                }

                tile.Coordinates = new Vector2Int(i, j);
            }
        }

        for (int i = 0; i < _size; ++i)
        {
            for (int j = 0; j < _size; ++j)
            {
                if (i != _size - 1)
                {
                    tiles[i, j].XNeighbour = tiles[i + 1, j];
                }

                if (j != _size - 1)
                {
                    tiles[i, j].YNeighbour = tiles[i, j + 1];
                }
            }
        }

        mapRoot.transform.rotation = Quaternion.Euler(45f, 0f, 45f);
        mapRoot.transform.position = mapRoot.transform.position + Vector3.forward * _size;
    }

    #endregion
}
