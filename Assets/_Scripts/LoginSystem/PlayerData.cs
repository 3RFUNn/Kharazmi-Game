using System;
[Serializable]
public class PlayerData
{
    public string playerName;
    public int score;
    public float playTime;
    public DateTime lastPlayed;
    
    public PlayerData(string name)
    {
        playerName = name;
        score = 0;
        playTime = 0f;
        lastPlayed = DateTime.Now;
    }
}

