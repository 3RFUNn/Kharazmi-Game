using System;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : SingletonBehaviour<MainMenuManager>
{
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject TutorialPanel;
    [SerializeField] GameObject TutorialArea;
    [SerializeField] GameObject TutorialButtonParent;

    [SerializeField] Button StartButton;
    [SerializeField] Button TutorialButton;
    [SerializeField] Button SettingsButton;
    [SerializeField] Button ProfileButton;

    [SerializeField] Button Level1TutorialButton;
    [SerializeField] Button Level2TutorialButton;
    [SerializeField] Button Level3TutorialButton;
    [SerializeField] Button Level4TutorialButton;

    [SerializeField] Button TutorialBackButton;
    [SerializeField] RTLTextMeshPro TutorialText;
    void Start()
    {
        MenuPanel.SetActive(true);
        TutorialPanel.SetActive(false);
        TutorialButton.onClick.AddListener(TutorialButtonClicked);
        TutorialBackButton.onClick.AddListener(TutorialBackButtonClicked);
        Level1TutorialButton.onClick.AddListener(Level1TutorialButtonClicked);
        Level2TutorialButton.onClick.AddListener(Level2TutorialButtonClicked);
        Level3TutorialButton.onClick.AddListener(Level3TutorialButtonClicked);
        Level4TutorialButton.onClick.AddListener(Level4TutorialButtonClicked);
        //TODO Other Button Interactions


    }
    //TODO set Tutorial Texts
    private void Level4TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "";
    }

    private void Level3TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "";
    }

    private void Level2TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "";
    }

    private void Level1TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "";
    }

    private void TutorialBackButtonClicked()
    {
        if (TutorialArea.activeSelf)
        {
            TutorialPanel.SetActive(true);
            TutorialArea.SetActive(false);
            TutorialButtonParent.SetActive(true);
            TutorialText.text= string.Empty;

        }
        else
        {
            TutorialPanel.SetActive(false);
            MenuPanel.SetActive(true);
        }
    }

    private void TutorialButtonClicked()
    {
        MenuPanel.SetActive(false);
        TutorialPanel.SetActive(true);
        TutorialButtonParent.SetActive(true);
        TutorialArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
