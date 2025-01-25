using System;
using System.Collections.Generic;
using NUnit.Framework;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : SingletonBehaviour<PanelManager>
{
    [SerializeField] GameObject StartPanel;
    [SerializeField] Button StartButton;
    [SerializeField] GameObject LoginPanel;
    [SerializeField] GameObject SignUpPanel;
    [SerializeField] GameObject LoginAndSignUpPanel;
    [SerializeField] Button LoginButton;
    [SerializeField] RTLTextMeshPro LoginUsernameText;
    [SerializeField] RTLTextMeshPro LoginPasswordText;
    [SerializeField] Button SignUpButton;
    [SerializeField] RTLTextMeshPro SignUpUsernameText;
    [SerializeField] RTLTextMeshPro SignUpPasswordText;
    [SerializeField] RTLTextMeshPro SignUpEmailText;
    [SerializeField] Button LoginBackButton;
    [SerializeField] Button SignUpBackButton;
    [SerializeField] Button LoginConfirmButton;
    [SerializeField] Button SignUpConfirmButton;


    void Start()
    {
        StartPanel.gameObject.SetActive(true);
        LoginPanel.gameObject.SetActive(false);
        SignUpPanel.gameObject.SetActive(false);
        LoginAndSignUpPanel.gameObject.SetActive(false);
        StartButton.onClick.AddListener(StartButtonClicked);
        LoginButton.onClick.AddListener(LoginButtonClicked);
        SignUpButton.onClick.AddListener(SignUpButtonClicked);
        LoginBackButton.onClick.AddListener(LoginBackButtonClicked);
        SignUpBackButton.onClick.AddListener(SignUpBackButtonClicked);

    }

    private void SignUpBackButtonClicked()
    {
        SignUpUsernameText.text = String.Empty;
        SignUpPasswordText.text = String.Empty;
        SignUpEmailText.text = String.Empty;
        SignUpPanel.SetActive(false);
        LoginAndSignUpPanel.SetActive(true);
    }

    private void LoginBackButtonClicked()
    {
        LoginUsernameText.text = String.Empty;
        LoginPasswordText.text = String.Empty;
        LoginPanel.SetActive(false);
        LoginAndSignUpPanel.SetActive(true);
    }

    private void SignUpButtonClicked()
    {
        LoginAndSignUpPanel.SetActive(false);
        SignUpPanel.SetActive(true) ;
    }

    private void LoginButtonClicked()
    {
        LoginAndSignUpPanel.SetActive(false);
        LoginPanel.SetActive(true) ;
    }

    private void StartButtonClicked()
    {
        StartPanel.gameObject.SetActive(false);
        LoginAndSignUpPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
