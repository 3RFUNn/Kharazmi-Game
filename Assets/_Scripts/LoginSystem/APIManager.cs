// APIManager.cs
// This class handles all REST API communications
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Threading.Tasks;

public class APIManager : MonoBehaviour
{
    private const string API_BASE_URL = "YOUR_API_ENDPOINT"; // Replace with your actual API endpoint
    
    public static APIManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public async Task<bool> SendPlayerData(PlayerData playerData)
    {
        string jsonData = JsonUtility.ToJson(playerData);
        
        using (UnityWebRequest request = new UnityWebRequest(API_BASE_URL + "/players", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            
            await request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Data sent successfully");
                return true;
            }
            else
            {
                Debug.LogError($"Error sending data: {request.error}");
                return false;
            }
        }
    }
    
    public async Task<PlayerData> GetPlayerData(string playerName)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(API_BASE_URL + "/players/" + playerName))
        {
            await request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                return JsonUtility.FromJson<PlayerData>(jsonResponse);
            }
            else
            {
                Debug.LogError($"Error getting data: {request.error}");
                return null;
            }
        }
    }
}