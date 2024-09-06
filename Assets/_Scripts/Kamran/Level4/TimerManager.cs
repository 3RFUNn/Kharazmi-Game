using RTLTMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : SingletonBehaviour<TimerManager>
{
    public Action OnTimerUp;
    public int currentTime;
    [SerializeField] RTLTextMeshPro text;
    public void Init(int seconds)
    {
        StartTimer(seconds);
    }
    void StartTimer(int seconds)
    {
        currentTime = seconds;
        StartCoroutine(Countdown());
    }
    IEnumerator Countdown()
    {
        text.text = currentTime.ToString();
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;
            text.text=currentTime.ToString();
        }
        OnTimerUp?.Invoke();
    }
}
