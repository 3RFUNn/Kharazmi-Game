using UnityEngine;
using UnityEngine.UI;

public class HeaderHandler : MonoBehaviour
{
    [SerializeField] Button MapBtn;
    [SerializeField] Button infoBtn;
    [SerializeField] GameObject timer;
    private void Start()
    {
        infoBtn.onClick.AddListener(()=>RoadMapManager.Instance.OpenRoadMap(2));
        MapBtn.onClick.AddListener(() => RoadMapManager.Instance.OpenRoadMap(1));
    }
    public void SetTimeObjectEnable(bool val)
    {
        timer.SetActive(val);
    }
}
