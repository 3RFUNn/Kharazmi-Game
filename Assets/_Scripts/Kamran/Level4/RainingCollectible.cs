using DG.Tweening;
using RTLTMPro;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RainingCollectible : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] RTLTextMeshPro3D keyText;
    public string KeyText => keyText.text;
    public void Init(float endY)
    {
        SetText();
        transform.DOMoveY(endY, speed+Random.Range(-0.2f,0.2f)).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(() =>
        {
            RainingCollectibleManager.Instance.RemoveRain(this);
        });
    }

    private void SetText()
    {
        var rand = Random.Range(0, 2);
        if (rand == 0)
        {
            keyText.text = DatabaseHolder4.Instance.Constants[Random.Range(0, DatabaseHolder4.Instance.Constants.Count - 1)];
            return;
        }
        keyText.text = DatabaseHolder4.Instance.Prefixes[Random.Range(0, DatabaseHolder4.Instance.Prefixes.Count - 1)];
        keyText.text += "x";

    }
    private void OnMouseDown()
    {
        Debug.Log("clicked on : " +keyText.text);
        RainingCollectibleManager.Instance.ClickedRain(this);
    }
}
