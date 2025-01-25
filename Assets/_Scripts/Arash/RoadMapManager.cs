using System;
using UnityEngine;
using UnityEngine.UI;

public class RoadMapManager : SingletonBehaviour<RoadMapManager>
{
    [SerializeField] GameObject MapPanel;
    [SerializeField] GameObject RoadMapPanel;
    [SerializeField] GameObject DescriptionPanel;
    [SerializeField] Button MapButton;
    [SerializeField] Button DescrptionButton;
    [SerializeField] Button MapBackButton;
    [SerializeField] Button PanelBackButton;
    [SerializeField] Button DescriptionBackButton;
    void Start()
    {
        MapPanel.SetActive(false);
        RoadMapPanel.SetActive(true);
        MapBackButton.onClick.AddListener(MapBackButtonClicked);
        MapButton.onClick.AddListener(MapButtonClicked);
        DescrptionButton.onClick.AddListener(DescriptionButtonClicked);
        DescriptionBackButton.onClick.AddListener(DescriptionBackButtonClicked);
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
