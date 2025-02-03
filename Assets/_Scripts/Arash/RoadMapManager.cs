using System;
using UnityEngine;
using UnityEngine.UI;

public class RoadMapManager : SingletonBehaviour<RoadMapManager>
{
    public Action onPauseGame;
    public Action onResumeGame;
    [SerializeField] GameObject MapPanel;
    [SerializeField] GameObject RoadMapPanel;
    [SerializeField] GameObject DescriptionPanel;
    [SerializeField] Button MapButton;
    [SerializeField] Button DescrptionButton;
    [SerializeField] Button MapBackButton;
    [SerializeField] Button PanelBackButton;
    [SerializeField] Button DescriptionBackButton;

    [SerializeField] Sprite level0sprite;
    [SerializeField] Sprite level1sprite;
    [SerializeField] Sprite level2sprite;
    [SerializeField] Sprite level3sprite;
    [SerializeField] Sprite level4sprite;
    [SerializeField] Image mapImage;
    int mapLevel;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        MapPanel.SetActive(false);
        RoadMapPanel.SetActive(false);
        MapBackButton.onClick.AddListener(MapBackButtonClicked);
        MapButton.onClick.AddListener(MapButtonClicked);
        PanelBackButton.onClick.AddListener(CloseRoadMap);
        DescrptionButton.onClick.AddListener(DescriptionButtonClicked);
        DescriptionBackButton.onClick.AddListener(DescriptionBackButtonClicked);
    }
    public void OpenRoadMap(int openType=0,int level=0)
    {
        mapLevel = level;
        onPauseGame?.Invoke();
        RoadMapPanel.SetActive(true);
        if (openType == 1)
        {
            MapButtonClicked();
        }
        if (openType == 2)
        {
            DescriptionButtonClicked();
        }
    }
    public void CloseRoadMap()
    {
        onResumeGame?.Invoke();
        RoadMapPanel.SetActive(false);
    }
    private void DescriptionBackButtonClicked()
    {
        DescriptionPanel.SetActive(false);
        RoadMapPanel.SetActive(true);
    }

    private void DescriptionButtonClicked()
    {
        DescriptionPanel.SetActive(true);
        RoadMapPanel.SetActive(false);
    }

    private void MapButtonClicked()
    {
        MapPanel.SetActive(true);
        switch (mapLevel)
        {
            case 0:
                mapImage.sprite = level0sprite;
                break;
            case 1:
                mapImage.sprite = level1sprite;
                break;
            case 2:
                mapImage.sprite = level2sprite;
                break;
            case 3:
                mapImage.sprite = level3sprite;
                break;
            case 4:
                mapImage.sprite = level4sprite;
                break;
        }
        RoadMapPanel.SetActive(false);
    }

    private void MapBackButtonClicked()
    {
        RoadMapPanel.SetActive(true);
        MapPanel.SetActive(false);
        DescriptionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
