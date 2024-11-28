// LoginManager.cs (Updated version)
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private string firstGameScene = "Level1"; // Set this in Inspector
    
    public static string CurrentPlayerName { get; private set; }
    
    private void Start()
    {
        loginButton.onClick.AddListener(HandleLogin);
        errorText.gameObject.SetActive(false);
    }
    
    private async void HandleLogin()
    {
        string playerName = nameInput.text.Trim();
        
        if (string.IsNullOrEmpty(playerName))
        {
            ShowError("Please enter your name");
            return;
        }
        
        PlayerData newPlayer = new PlayerData(playerName);
        bool success = await APIManager.Instance.SendPlayerData(newPlayer);
        
        if (success)
        {
            CurrentPlayerName = playerName;
            SceneManager.LoadScene(firstGameScene);
        }
        else
        {
            ShowError("Failed to connect to server. Please try again.");
        }
    }
    
    private void ShowError(string message)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(true);
    }
}

