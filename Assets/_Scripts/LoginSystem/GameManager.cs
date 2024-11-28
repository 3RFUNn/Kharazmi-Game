// GameManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private PlayerData currentPlayerData;
    private float saveInterval = 60f; // Save every minute
    private float playTimeCounter = 0f;
    private Coroutine autoSaveCoroutine;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializePlayerData();
        StartAutoSave();
    }

    private void InitializePlayerData()
    {
        if (currentPlayerData == null && !string.IsNullOrEmpty(LoginManager.CurrentPlayerName))
        {
            currentPlayerData = new PlayerData(LoginManager.CurrentPlayerName);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Don't process for login scene
        if (scene.name == "LoginScene") return;

        Debug.Log($"Scene loaded: {scene.name}");
        InitializePlayerData();
    }

    public void StartAutoSave()
    {
        if (autoSaveCoroutine != null)
        {
            StopCoroutine(autoSaveCoroutine);
        }
        autoSaveCoroutine = StartCoroutine(AutoSaveRoutine());
    }

    public void StopAutoSave()
    {
        if (autoSaveCoroutine != null)
        {
            StopCoroutine(autoSaveCoroutine);
            autoSaveCoroutine = null;
        }
    }
    
    private void Update()
    {
        if (currentPlayerData != null)
        {
            playTimeCounter += Time.deltaTime;
            currentPlayerData.playTime = playTimeCounter;
        }
    }
    
    public void UpdateScore(int newScore)
    {
        if (currentPlayerData != null)
        {
            currentPlayerData.score = newScore;
        }
    }

    public void UpdateSpecificSceneData(string sceneName, object sceneSpecificData)
    {
        // You can implement scene-specific data storage here
        // For example, storing level completion status, achievements, etc.
    }
    
    private IEnumerator AutoSaveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(saveInterval);
            SaveGameData();
        }
    }
    
    public async void SaveGameData()
    {
        if (currentPlayerData != null)
        {
            currentPlayerData.lastPlayed = System.DateTime.Now;
            bool success = await APIManager.Instance.SendPlayerData(currentPlayerData);
            
            if (success)
            {
                Debug.Log("Game data saved successfully");
            }
            else
            {
                Debug.LogWarning("Failed to save game data");
            }
        }
    }

    public void LoadNextGameScene(string sceneName)
    {
        SaveGameData(); // Save before loading new scene
        SceneManager.LoadScene(sceneName);
    }
    
    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

// SceneManager extension for easy scene transitions
public static class SceneManagerExtension
{
    public static void LoadGameScene(string sceneName)
    {
        GameManager.Instance?.SaveGameData();
        SceneManager.LoadScene(sceneName);
    }
}
