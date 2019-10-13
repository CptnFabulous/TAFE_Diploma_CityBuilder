using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour
{
    #region Private Variables

    private enum BuildingTypes
    {
        Housing,
        Power,
        Shop,
        Entertainment,
    };

    [SerializeField]
    private BuildingTypes _buidingType = BuildingTypes.Housing;

    [SerializeField]
    private BuildButton[] _buttons = new BuildButton[6];

    private List<Building> _filteredTypes = new List<Building>();

    [SerializeField]
    private GameObject _nextButton = null;

    [SerializeField]
    private GameObject _prevButton = null;

    [SerializeField]
    private GameObject _pageDisplay = null;

    [SerializeField]
    private Text _pageCounterText = null;

    private int _page = 1;
    private int _totalPages = 0;

    [SerializeField]
    private HousePlacer _housePlacer = null;

    [SerializeField]
    private GameObject _buildButtonOn = null;

    [SerializeField]
    private GameObject _buildButtonOff = null;

    #endregion

    #region MonoBehaviour Functions

    private void Start()
    {
        List<Object> buildingResources = new List<Object>(Resources.LoadAll("Buildings", typeof(Building)));

        foreach (var buildingResource in buildingResources)
        {
            FilterObject(buildingResource);
        }

        if (_filteredTypes.Count < 7)
        {
            _pageDisplay.SetActive(false);
        }

        _totalPages = (_filteredTypes.Count / 6) + 1;

        SetupButtons();
    }

    #endregion

    #region Public Functions

    private void FilterObject(Object obj)
    {
        switch (_buidingType)
        {
            case (BuildingTypes.Housing):
                if (obj.GetType() == typeof(Housing))
                {
                    Housing building = (Housing)(obj);
                    _filteredTypes.Add(building);
                }
                break;
            case (BuildingTypes.Power):
                if (obj.GetType() == typeof(Power))
                {
                    Power building = (Power)(obj);
                    _filteredTypes.Add(building);
                }
                break;
            case (BuildingTypes.Shop):
                if (obj.GetType() == typeof(Shop))
                {
                    Shop building = (Shop)(obj);
                    _filteredTypes.Add(building);
                }
                break;
            case (BuildingTypes.Entertainment):
                if (obj.GetType() == typeof(Entertainment))
                {
                    Entertainment building = (Entertainment)(obj);
                    _filteredTypes.Add(building);
                }
                break;
        }
    }

    private void SetupButtons()
    {
        _prevButton.SetActive(_page > 1);
        _nextButton.SetActive(_page < _totalPages);

        _pageCounterText.text = _page.ToString() + "/" + _totalPages.ToString();

        int i = 0;
        for (i = 0; (i < 6) && ((i + (_page - 1) * 6) < _filteredTypes.Count); ++i)
        {
            _buttons[i].Setup(_filteredTypes[(i + (_page - 1) * 6)]);
            _buttons[i].gameObject.SetActive(true);
        }

        for (; i < 6; ++i)
        {
            _buttons[i].gameObject.SetActive(false);
        }
    }

    public void BuildButtonPressed(int idx)
    {
        _housePlacer.StartPlacement(_filteredTypes[(idx + (_page - 1) * 6)]);

        _buildButtonOff.SetActive(false);
        _buildButtonOn.SetActive(true);
        gameObject.SetActive(false);
    }

    public void NextPage()
    {
        ++_page;
        SetupButtons();
    }

    public void PrevPage()
    {
        --_page;
        SetupButtons();
    }

    #endregion
}
