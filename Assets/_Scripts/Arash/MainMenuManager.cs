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
    [SerializeField] SettingsManager SettingsPanel;
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
        SettingsPanel.LoadSettings();
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
        SettingsPanel.gameObject.SetActive(false);
        MenuPanel.SetActive(true);
    }

    private void SettingsButtonClicked()
    {
        SettingsPanel.gameObject.SetActive(true);
        MenuPanel.SetActive(false);
    }


    //TODO set Tutorial Texts
    private void Level4TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        tutorialImage.gameObject.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "ماز \r\nرسیدی به مرحله آخر، یعنی چالش تصویری معماهای! تو وارد یک ماز (هزارتوی پیچیده) می‌شوی که پر از معما و شکل‌های مختلف است.\r\nاینجا دیگر خبری از معادله‌های جبری نیست! در این چالش جذاب، باید مسائل را به صورت تصویری حل کنی و گزینه صحیح را انتخاب کنی.\r\n\r\n📐 مثال:\r\nتصور کن از تو می‌خواهند مساحت یک مستطیل را به دست بیاوری، یا زاویه‌ درست یک شکل را تشخیص دهی. سؤال‌های ساده و جذاب که باید با تمرکز حلشان کنی.\r\n\r\n🕒 پاداش:\r\nبه ازای هر جواب درست، امتیاز بیشتری به دست می‌آوری.\r\n\r\n👑 ویژه:\r\nدر این مرحله برنده شدن به دقت و شجاعت ریاضی تو بستگی دارد.\r\n«نشان‌های خوارزمی» آخرین فرصتت برای تصاحب امتیاز ویژه است!\r\n";
    }

    private void Level3TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        tutorialImage.gameObject.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "بارش عبارت‌های جبری، چیدمان کامل!\r\nحالا وقت آن رسیده که مهارت‌هایت در ساختن عبارت‌های جبری نشان داده شود. آسمان ریاضیات به روی تو باز شده و عبارت‌های کوچک جبری مثل دانه‌های باران از آسمان می‌ریزند.\r\nمأموریت تو چیست؟ تو باید با دقت و هوش، این عبارت‌ها را جمع‌آوری کنی و کنار هم بچینی تا یک عبارت جبری کامل و خواسته‌شده بسازی.\r\n\r\n🔑 نکته طلایی:\r\nکمی سرعت بیشتری به خودت بده! قطرات بارانی که نخ جمع کنی و عبارت را ناتمام بگذاری، به سرعت ناپدید می‌شوند. اگر سرعت عمل نداشته باشی، شانس برنده شدن در این مرحله را از دست خواهی داد.\r\n\r\n🎯 هدف این مرحله:\r\nعبارت‌های درست و تکمیل شده می‌توانند تو را به جایگاه یک ریاضي‌دان حرفه‌ای برسانند. هر عبارت درستی که تکمیل کنی، یک نشان خوارزمی دریافت می‌کنی!";
    }

    private void Level2TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        tutorialImage.gameObject.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "ترازو، چالش معادلات!\r\nتبریک! تو از مرحله اول با موفقیت عبور کردی و حالا باید وارد یکی از جدی‌ترین چالش‌ها شوی: معادلات جبری!\r\nدر این مرحله، مقابلت یک ترازو ظاهر می‌شود که نشان‌دهنده یک معادله جبری است. تو باید با حل درست معادلات، ترازو را به تعادل برسانی. هرچه سرعت و دقت بیشتری داشته باشی، زمان بیشتری برنده می‌شوی و نشان خوارزمی بیشتری بدست می‌آوری.\r\n\r\n🔑 نکته طلایی:\r\nیادت باشد که هم دقت مهم است و هم سرعت! اگر جوابت درست نباشد، نشان از دست می‌دهی. پس معادله‌ها را خوب تحلیل کن و با منطق پیش برو.\r\n\r\n🕒 پاداش:\r\nبه ازای هر معادله‌ای که درست حل می‌کنی، X ثانیه زمان اضافه دریافت می‌کنی.\r\nهر جواب درست یک نشان خوارزمی دارد!";
    }

    private void Level1TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        tutorialImage.gameObject.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "مرحله‌ی اول: شکار عبارت های جبری مار پرواز با کایت رؤیایی\r\nاولین چالش این بازی، پرواز هیجان‌انگیز با کایت شگفت‌انگیزت است. تو باید کایتت را هدایت کنی و عبارت‌های جبری مشابه را پیدا کنی تا دم کایتت طولانی‌تر و زیباتر شود. هرچه بتوانی عبارت‌های مشابه بیشتری را جمع کنی، زمان بیشتری برای حل چالش‌های بعدی بدست می‌آوری!\r\n\r\n🔑 نکته طلایی:\r\nتمرکز کن و به سرعت عمل کن! کایت زمانی زیبا دیده می‌شود که تو بتوانی عبارت‌های درست و متناسب را شناسایی و جمع‌آوری کنی.\r\n\r\n🕒 پاداش:\r\nبه ازای هر عبارت متشابه که به درستی پیدا می‌کنی، X ثانیه زمان اضافه دریافت می‌کنی که در مراحل بعد به دردت می‌خورد!";
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
