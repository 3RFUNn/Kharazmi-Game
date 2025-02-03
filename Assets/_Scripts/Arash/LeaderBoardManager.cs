using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderBoardManager : SingletonBehaviour<LeaderBoardManager>
{
    [SerializeField] GameObject leaderboardObject;
    [SerializeField] GameObject leaderboardParent;
    [SerializeField] Button backBtn;
    LeaderBoards leaderboards;
    private async void Start()
    {
        backBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Menu");
        });
        await GetLeaderboard();
    }
    async Task SendScores()
    {
        var res = await APIManager.Instance.SendGameData(null, () =>
        {
            //PopupController.Instance.ShowPopup("Connection Error","Error","Ok");
        }
        ,0
        ,PlayerPrefs.GetInt("Level1",0)
        , PlayerPrefs.GetInt("Level2", 0)
        , PlayerPrefs.GetInt("Level3", 0)
        , PlayerPrefs.GetInt("Level4", 0));
    }
    async Task GetLeaderboard()
    {
        var res = await APIManager.Instance.GetLeaderboard(null, () =>
        {
            //PopupController.Instance.ShowPopup("Connection Error", "Error", "Ok");
        });
        Debug.Log(res);
        leaderboards = JsonUtility.FromJson<LeaderBoards>(res);
        Debug.Log(leaderboards.msg);
        MakeLeaderBoard(leaderboards.global_leaderboard);
    }
    public void MakeLeaderBoard(LeaderBoardData[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            var obj=Instantiate(leaderboardObject,parent: leaderboardParent.transform).GetComponent<LeaderboardObject>();
            obj.Setup(data[i].username, data[i].score_received);
        }        
    }

}
[System.Serializable]
public class LeaderBoards
{
    public LeaderBoardData[] class_leaderboard;
    public LeaderBoardData[] global_leaderboard;
    public string msg;
}
[System.Serializable]
public class LeaderBoardData{
    public string username;
    public int score_received;
}
