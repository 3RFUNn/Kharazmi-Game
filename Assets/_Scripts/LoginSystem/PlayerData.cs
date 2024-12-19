using System;

[Serializable]
public class PlayerData
{
    public string playerName;
    public int score;
    public float playTime;
    public DateTime lastPlayed;
    public string currentScene;
    
    public PlayerData(string name)
    {
        playerName = name;
        score = 0;
        playTime = 0f;
        lastPlayed = DateTime.Now;
        currentScene = string.Empty;
    }
    
    // Parameterless constructor for JSON deserialization
    public PlayerData() { }
}