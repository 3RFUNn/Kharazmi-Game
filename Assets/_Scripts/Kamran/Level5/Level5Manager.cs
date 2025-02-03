using RTLTMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level5Manager : SingletonBehaviour<Level5Manager>
{
    int score = 0;
    int cnt = 0;
    [SerializeField] RTLTextMeshPro question;
    [SerializeField] Image ans1;
    [SerializeField] Image ans2;
    [SerializeField] GameObject firstTime;
    [SerializeField] TimerManager timerManager;
    private void Start()
    {
        
    }
    public void StartTheGame()
    {
        firstTime.SetActive(false);
        timerManager.Init(300);
    }
    public void ShowQuestion(int num)
    {
        var level = Dataholder5.Instance.Levels[num];
        cnt = num;
        question.text = level.Question;
        ans1.sprite = level.Ans1;
        ans2.sprite = level.Ans2;
    }
    public void ClickedOnAnswer(int answer)
    {
        if (answer == Dataholder5.Instance.Levels[cnt].AnswerNum)
        {
            score++;
        }
        if (cnt == Dataholder5.Instance.Levels.Length - 1)
        {
            PlayerPrefs.SetInt("Level4", score);
            SceneManager.LoadScene("Leaderboard");
            return;
        }
        else
        {
            ShowQuestion(cnt + 1);
            return;
        }
    }
}
