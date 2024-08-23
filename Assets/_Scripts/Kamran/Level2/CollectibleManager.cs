using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    [HideInInspector]
    public string KeyString;
    [Header("Dotween Properties")]
    [SerializeField] float positionAmount = 2f;
    [SerializeField] float duration = 1f;
    [SerializeField] int vibrato = 1;
    [SerializeField] float rotationAmount = 90;
    [SerializeField] int elasticity = 1;
    [SerializeField] int delayMiliseconds = 1;

    public void Init(string _keyString = "nothing")
    {
        SetRandomText(_keyString);
        Wiggle();
    }
    public void SetText(string _text,string _keyString)
    {
        text.text= _text;
        KeyString = _keyString;
    }
    void SetRandomText(string _keyString="nothing")
    {
        int rand = Random.Range(0, 5);
        if (rand == 0 && _keyString.Equals("nothing"))
        {
            rand = Random.Range(0, DatabaseHolder.Instance.Constants.Count);
            var constant=DatabaseHolder.Instance.Constants[rand];
            SetText(constant, constant);
            return;
        }
        rand = Random.Range(0,DatabaseHolder.Instance.KeyStrings.Count);
        var keyString=DatabaseHolder.Instance.KeyStrings[rand];
        if (!_keyString.Equals("nothing"))
        {
            keyString = _keyString;
        }
        rand=Random.Range(0,DatabaseHolder.Instance.KeyPrefixes.Count);
        var prefix=DatabaseHolder.Instance.KeyPrefixes[rand];
        SetText(prefix+" "+keyString, keyString);
    }
    public string GetText()
    {
        return text.text;
    }
    async void Wiggle()
    {
        var delayAmountRand = Random.Range(0, delayMiliseconds);
        await Task.Delay(delayAmountRand);
        var positionAmountRand = Random.Range(1f, positionAmount);
        var rotationAmountRand = Random.Range(5f, rotationAmount);

        transform.DOShakePosition(duration, positionAmountRand, vibrato, elasticity, false, true, ShakeRandomnessMode.Harmonic)
            .SetLoops(-1);
        Vector3 rotationStrength = new Vector3(0f, 0f, rotationAmountRand);
        transform.DOShakeRotation(duration, rotationStrength, vibrato, elasticity, true, ShakeRandomnessMode.Harmonic)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
