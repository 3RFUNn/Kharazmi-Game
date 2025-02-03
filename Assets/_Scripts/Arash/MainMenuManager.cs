using System;
using RTLTMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : SingletonBehaviour<MainMenuManager>
{
    [System.Serializable]
    public class HighScores
    {
        public int max_level1_score;
        public int max_level2_score;
        public int max_level3_score;
        public int max_level4_score;
        public string msg;
    }
    [System.Serializable]
    public class Ranks
    {
        public int class_rank;
        public int global_rank;
        public string msg;
    }
    [System.Serializable]
    public class DifficulityScores
    {
        public int max_easy;
        public int max_medium;
        public int max_hard;
        public int score_received;
        public string msg;
    }
    [System.Serializable]
    public class GameCount
    {
        public int games_count;
        public string msg;
    }
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject TutorialPanel;
    [SerializeField] GameObject SettingsPanel;
    [SerializeField] GameObject TutorialArea;
    [SerializeField] GameObject TutorialButtonParent;
    [SerializeField] GameObject ProfilePanel;

    [SerializeField] Button StartButton;
    [SerializeField] Button TutorialButton;
    [SerializeField] Button SettingsButton;
    [SerializeField] Button ProfileButton;

    [SerializeField] Button Level1TutorialButton;
    [SerializeField] Button Level2TutorialButton;
    [SerializeField] Button Level3TutorialButton;
    [SerializeField] Button Level4TutorialButton;

    [SerializeField] Button TutorialBackButton;
    [SerializeField] RTLTextMeshPro TutorialText;

    [SerializeField] Button SettingsBackButton;
    [SerializeField] Button ProfileBackButton;
    [SerializeField] ScrollRect ProfileScroll;
    [SerializeField] Image tutorialImage;
    [SerializeField] ProfileManager profileManager;
    void Start()
    {
        
        ProfilePanel.SetActive(false);
        MenuPanel.SetActive(true);
        TutorialPanel.SetActive(false);
        if (PlayerPrefs.GetString("PreviousScene") == "Leaderboard")
        {
            PlayerPrefs.DeleteKey("PreviousScene");
            MenuPanel.SetActive(false);
            ProfilePanel.SetActive(true);
        }
        TutorialButton.onClick.AddListener(TutorialButtonClicked);
        TutorialBackButton.onClick.AddListener(TutorialBackButtonClicked);
        Level1TutorialButton.onClick.AddListener(Level1TutorialButtonClicked);
        Level2TutorialButton.onClick.AddListener(Level2TutorialButtonClicked);
        Level3TutorialButton.onClick.AddListener(Level3TutorialButtonClicked);
        Level4TutorialButton.onClick.AddListener(Level4TutorialButtonClicked);
        SettingsButton.onClick.AddListener(SettingsButtonClicked);
        SettingsBackButton.onClick.AddListener(SettingsBackButtonClicked);
        ProfileButton.onClick.AddListener(ProfileButtonClicked);
        ProfileBackButton.onClick.AddListener(ProfileBackButtonClicked);
        //TODO Other Button Interactions
        StartButton.onClick.AddListener(StartGameClicked);
        GetHighScores();
    }

    private async void GetHighScores()
    {
        var res=await APIManager.Instance.GetHighScores(null,null);
        Debug.Log(res);
        var hs = JsonUtility.FromJson<HighScores>(res);
        profileManager.SetSize(1, hs.max_level1_score * 10);
        profileManager.SetSize(2, hs.max_level2_score * 100 / 3);
        profileManager.SetSize(3, hs.max_level3_score * 10);
        profileManager.SetSize(4, hs.max_level4_score * 50);
        var res2= await APIManager.Instance.GetGameCount(null,null);
        var res4 = await APIManager.Instance.GetMaxDifficulityScores(null, null);
        var res3 = await APIManager.Instance.GetRanks(null, null);
        var gameCount = JsonUtility.FromJson<GameCount>(res2);
        var ranks = JsonUtility.FromJson<Ranks>(res3);
        var difficulityScores = JsonUtility.FromJson<DifficulityScores>(res4);
        profileManager.globalRank.text = ranks.global_rank.ToString();
        profileManager.classRank.text = ranks.class_rank.ToString();
        profileManager.points.text= difficulityScores.score_received.ToString();
        profileManager.easyStat.Setup("آسان", difficulityScores.max_easy.ToString());
        profileManager.mediumStat.Setup("متوسط", difficulityScores.max_medium.ToString());
        profileManager.hardStat.Setup("سخت", difficulityScores.max_hard.ToString());
        profileManager.difficultyPointsText.text = "ایول تا اینجا " + gameCount.games_count + " دست بازی کردی";

        //TODO HighScores figure KAMRAN
    }

    private void ProfileBackButtonClicked()
    {
        ProfilePanel.SetActive(false);
        ProfileScroll.verticalNormalizedPosition = 1;
        MenuPanel.SetActive(true);
    }

    private void ProfileButtonClicked()
    {
        MenuPanel.SetActive(false);
        ProfileScroll.verticalNormalizedPosition = 1;
        ProfilePanel.SetActive(true);
    }

    private void StartGameClicked()
    {
        SceneManager.LoadScene("Level 2");
    }

    private void SettingsBackButtonClicked()
    {
        SettingsPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    private void SettingsButtonClicked()
    {
        SettingsPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }


    //TODO set Tutorial Texts
    private void Level4TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        tutorialImage.gameObject.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "";
    }

    private void Level3TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        tutorialImage.gameObject.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "مرحله‌ی سوم: بارش عبارت‌های جبری\r\nآسمان ریاضیات به روی تو باز شده و عبارت‌های جبری مثل بارون میبارن! تو باید با دقت قطرات بارون رو جمع‌آوری کنی و با چیدنشون کنار هم، عبارت‌ جبری خواسته شده رو بسازی. هر عبارت درستی که بسازی، یک نشان خوارزمی می گیری. حواست باشه که باید سریع عمل کنی، چون قطرات بارون به سرعت ناپدید میشن!\r\n";
    }

    private void Level2TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        tutorialImage.gameObject.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "مرحله‌ی دوم: نبرد با معادلات\r\nحالا نوبت به حل کردن معادلات جبری رسیده! با حل هر معادله، زمان بیشتری به دست میاری تا در مرحله‌ی بعدی ازش استفاده کنی. هر چه سریع‌تر و دقیق‌تر معادلات رو حل کنی، امتیاز بیشتری کسب می‌کنی. (به ازای هر جواب درست ۱۰ ثانیه دریافت میکنی)";
    }

    private void Level1TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        tutorialImage.gameObject.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "مرحله‌ی اول: شکار عبارت های جبری مار\r\nدر این مرحله، یک مار شیطون دنبال عبارت‌های جبری متشابه میگرده که بخوره! تو باید هدایتش کنی تا بیشترین عبارت جبری رو بخوره. هر عبارت جبری که مار بخوره، این بازی زمان بیشتری برای حل مساله های بعدی بهت میده. (به ازای هر عبارت متشابه درست ۵ ثانیه دریافت میکنی)";
    }

    private void TutorialBackButtonClicked()
    {
        if (TutorialArea.activeSelf)
        {
            TutorialPanel.SetActive(true);
            TutorialArea.SetActive(false);
            TutorialButtonParent.SetActive(true);
            tutorialImage.gameObject.SetActive(true);
            TutorialText.text= string.Empty;

        }
        else
        {
            TutorialPanel.SetActive(false);
            MenuPanel.SetActive(true);
        }
    }

    private void TutorialButtonClicked()
    {
        MenuPanel.SetActive(false);
        TutorialPanel.SetActive(true);
        TutorialButtonParent.SetActive(true);
        tutorialImage.gameObject.SetActive(true);
        TutorialArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
