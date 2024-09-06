using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ClickedThingsManager : SingletonBehaviour<ClickedThingsManager>
{
    [SerializeField] ClickedThing prefab;
    [SerializeField] ScrollRect scrollbar;
    [SerializeField] Transform parent;
    public void Init()
    {
        ResetRains();
        Level4Manager.Instance.ClickedRain += AddRain;
        Level4Manager.Instance.FinishedEquation += ResetRains;
    }

    private void ResetRains()
    {
        foreach (Transform t in parent)
        {
            Destroy(t.gameObject);
        }
    }

    private async void AddRain(RainingCollectible collectible,int value,bool isKey)
    {
        var thing = Instantiate(prefab, parent);
        thing.Repaint(collectible.KeyText,value,isKey);
        await Task.Delay(100);
        scrollbar.verticalNormalizedPosition = 0f;
    }
}
