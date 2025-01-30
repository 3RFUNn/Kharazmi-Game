using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System;

[Serializable]
public class RequestData
{
    public string username;
    public string password;

    public RequestData(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
}

[Serializable]
public class ResponseData
{
    public string message;
    public int status;
}

public class APIManager : SingletonBehaviour<APIManager>
{
    private const string baseUrl = "localhost:8080"; // Change to your actual API URL
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SendRequest(string endpoint, string method, RequestData requestData = null)
    {
        string url = $"{baseUrl}/{endpoint}";

        if (method.ToUpper() == "GET" && requestData != null)
        {
            url += $"?username={UnityWebRequest.EscapeURL(requestData.username)}&password={UnityWebRequest.EscapeURL(requestData.password)}";
            StartCoroutine(SendWebRequest(url, "GET", null));
        }
        else
        {
            StartCoroutine(SendWebRequest(url, method, requestData));
        }
    }

    private IEnumerator SendWebRequest(string url, string method, RequestData requestData)
    {
        UnityWebRequest request;
        if (method.ToUpper() == "POST")
        {
            string jsonData = requestData != null ? JsonUtility.ToJson(requestData) : "{}";
            byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);
            request = new UnityWebRequest(url, "POST");
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.SetRequestHeader("Content-Type", "application/json");
        }
        else // GET Request
        {
            request = UnityWebRequest.Get(url);
        }

        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;
            ResponseData response = JsonUtility.FromJson<ResponseData>(responseText);
            Debug.Log($"Success: {response.message}, Status: {response.status}");
        }
        else
        {
            Debug.LogError($"Error: {request.error}, Response: {request.downloadHandler.text}");
        }
    }
}
