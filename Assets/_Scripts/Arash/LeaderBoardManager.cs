using System;
using UnityEngine;

public class LeaderBoardManager : SingletonBehaviour<LeaderBoardManager>
{
    [SerializeField] GameObject leaderboardObject;
    [SerializeField] GameObject leaderboardParent;

    public void MakeLeaderBoard(LeaderBoardData[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            var obj=Instantiate(leaderboardObject,parent: leaderboardParent.transform).GetComponent<LeaderboardObject>();
            obj.Setup(data[i].name,data[i].rank,data[i].rankNumber,data[i].sprite);
        }        
    }

}
public class LeaderBoardData{
    public string name;
    public string rank;
    public string rankNumber;
    public Sprite sprite;
}
