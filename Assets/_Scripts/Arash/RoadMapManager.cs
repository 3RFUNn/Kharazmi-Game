using System;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoadMapManager : SingletonBehaviour<RoadMapManager>
{
    public Action onPauseGame;
    public Action onResumeGame;
    [SerializeField] GameObject MapPanel;
    [SerializeField] GameObject RoadMapPanel;
    [SerializeField] GameObject DescriptionPanel;
    [SerializeField] Button MapButton;
    [SerializeField] Button DescrptionButton;
    [SerializeField] Button MapBackButton;
    [SerializeField] Button PanelBackButton;
    [SerializeField] Button DescriptionBackButton;

    [SerializeField] Sprite level0sprite;
    [SerializeField] Sprite level1sprite;
    [SerializeField] Sprite level2sprite;
    [SerializeField] Sprite level3sprite;
    [SerializeField] Sprite level4sprite;
    [SerializeField] Image mapImage;
    
    [SerializeField] RTLTextMeshPro descriptionText;
    int mapLevel;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        MapPanel.SetActive(false);
        RoadMapPanel.SetActive(false);
        MapBackButton.onClick.AddListener(MapBackButtonClicked);
        MapButton.onClick.AddListener(MapButtonClicked);
        PanelBackButton.onClick.AddListener(CloseRoadMap);
        DescrptionButton.onClick.AddListener(DescriptionButtonClicked);
        DescriptionBackButton.onClick.AddListener(DescriptionBackButtonClicked);
        gameObject.SetActive(false);
    }
    public void OpenRoadMap(int openType=0,int level=0)
    {
        mapLevel = level;
        onPauseGame?.Invoke();
        RoadMapPanel.SetActive(true);
        gameObject.SetActive(true);
        if (openType == 1)
        {
            MapButtonClicked();
        }
        if (openType == 2)
        {
            DescriptionButtonClicked();
        }
    }
    public void CloseRoadMap()
    {
        onResumeGame?.Invoke();
        RoadMapPanel.SetActive(false);
        gameObject.SetActive(false);
    }
    private void DescriptionBackButtonClicked()
    {
        DescriptionPanel.SetActive(false);
        RoadMapPanel.SetActive(true);
    }

    private void DescriptionButtonClicked()
    {
        DescriptionPanel.SetActive(true);
        switch (mapLevel)
        {
            case 0:
                descriptionText.text = "سلام دوست ریاضی‌دان من!\r\n\r\nتصور کن تو، یک ریاضی‌دان باهوش و کنجکاو هستی که عاشق حل کردن مساله‌های ریاضیاتی که میخوای نشان خوارزمی بگیری! تو باید با استفاده از دانش و مهارت خودت در مورد عبارت‌های جبری، مساله های مختلف رو حل کنی و بیشترین تعداد نشان خوارزمی رو دریافت کنی.";
                break;
            case 1:
                descriptionText.text = "مرحله‌ی اول: شکار عبارت های جبری مار\r\nدر این مرحله، یک مار شیطون دنبال عبارت‌های جبری متشابه میگرده که بخوره! تو باید هدایتش کنی تا بیشترین عبارت جبری رو بخوره. هر عبارت جبری که مار بخوره، این بازی زمان بیشتری برای حل مساله های بعدی بهت میده. (به ازای هر عبارت متشابه درست ۵ ثانیه دریافت میکنی)";
                break;
            case 2:
                descriptionText.text = "مرحله‌ی دوم: نبرد با معادلات\r\nحالا نوبت به حل کردن معادلات جبری رسیده! با حل هر معادله، زمان بیشتری به دست میاری تا در مرحله‌ی بعدی ازش استفاده کنی. هر چه سریع‌تر و دقیق‌تر معادلات رو حل کنی، امتیاز بیشتری کسب می‌کنی. (به ازای هر جواب درست ۱۰ ثانیه دریافت میکنی)";
                break;
            case 3:
                descriptionText.text = "مرحله‌ی سوم: بارش عبارت‌های جبری\r\nآسمان ریاضیات به روی تو باز شده و عبارت‌های جبری مثل بارون میبارن! تو باید با دقت قطرات بارون رو جمع‌آوری کنی و با چیدنشون کنار هم، عبارت‌ جبری خواسته شده رو بسازی. هر عبارت درستی که بسازی، یک نشان خوارزمی می گیری. حواست باشه که باید سریع عمل کنی، چون قطرات بارون به سرعت ناپدید میشن!";
                break;
            case 4:
                descriptionText.text = "";
                break;
        }
        RoadMapPanel.SetActive(false);
    }

    private void MapButtonClicked()
    {
        MapPanel.SetActive(true);
        switch (mapLevel)
        {
            case 0:
                mapImage.sprite = level0sprite;
                break;
            case 1:
                mapImage.sprite = level1sprite;
                break;
            case 2:
                mapImage.sprite = level2sprite;
                break;
            case 3:
                mapImage.sprite = level3sprite;
                break;
            case 4:
                mapImage.sprite = level4sprite;
                break;
        }
        RoadMapPanel.SetActive(false);
    }

    private void MapBackButtonClicked()
    {
        RoadMapPanel.SetActive(true);
        MapPanel.SetActive(false);
        DescriptionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
