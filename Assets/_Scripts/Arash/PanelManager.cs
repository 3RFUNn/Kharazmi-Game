using System;
using System.Collections.Generic;
using NUnit.Framework;
using RTLTMPro;
using TMPro;
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
    [SerializeField] TMP_InputField LoginPasswordField;
    [SerializeField] Button SignUpButton;
    [SerializeField] RTLTextMeshPro SignUpUsernameText;
    [SerializeField] RTLTextMeshPro SignUpPasswordText;
    [SerializeField] RTLTextMeshPro SignUpEmailText;
    [SerializeField] TMP_InputField SignUpPasswordField;
    [SerializeField] Button LoginBackButton;
    [SerializeField] Button SignUpBackButton;
    [SerializeField] Button LoginConfirmButton;
    [SerializeField] Button SignUpConfirmButton;
    [SerializeField] Button ShowPasswordButtonLogin;
    [SerializeField] Button ShowPasswordButtonSignup;
    [SerializeField] RTLTextMeshPro Label;


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
        ShowPasswordButtonLogin.onClick.AddListener(ShowPasswordLogin);
        ShowPasswordButtonSignup.onClick.AddListener(ShowPasswordSignup);
    }

    private void ShowPasswordLogin()
    {
        if(LoginPasswordField.contentType==TMP_InputField.ContentType.Password)
            LoginPasswordField.contentType=TMP_InputField.ContentType.Password;
        else
            LoginPasswordField.contentType=TMP_InputField.ContentType.Standard;
    }
    private void ShowPasswordSignup(){
        if(SignUpPasswordField.contentType==TMP_InputField.ContentType.Password)
            SignUpPasswordField.contentType=TMP_InputField.ContentType.Password;
        else
            SignUpPasswordField.contentType=TMP_InputField.ContentType.Standard;
    }

    private void SignUpBackButtonClicked()
    {
        SignUpUsernameText.text = String.Empty;
        SignUpPasswordText.text = String.Empty;
        SignUpEmailText.text = String.Empty;
        SignUpPasswordField.contentType=TMP_InputField.ContentType.Password;
        SignUpPanel.SetActive(false);
        LoginAndSignUpPanel.SetActive(true);
    }

    private void LoginBackButtonClicked()
    {
        LoginUsernameText.text = String.Empty;
        LoginPasswordText.text = String.Empty;
        LoginPanel.SetActive(false);
        LoginPasswordField.contentType=TMP_InputField.ContentType.Password;
        LoginAndSignUpPanel.SetActive(true);
    }

    private void SignUpButtonClicked()
    {
        LoginAndSignUpPanel.SetActive(false);
        SignUpPasswordField.contentType=TMP_InputField.ContentType.Password;
        SignUpPanel.SetActive(true) ;
        Label.text="نام کلاس";
    }

    private void LoginButtonClicked()
    {
        LoginAndSignUpPanel.SetActive(false);
        LoginPasswordField.contentType=TMP_InputField.ContentType.Password;
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
