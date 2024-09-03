using RTLTMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Manager : SingletonBehaviour<Level4Manager>
{
    public RainingCollectibleManager rainManager;
    public ClickedThingsManager thingsManager;
    public System.Action<RainingCollectible> ClickedRain;
    public System.Action FinishedEquation;
    [SerializeField] RTLTextMeshPro EquationKeyText;
    [SerializeField] RTLTextMeshPro EquationSignText;
    [SerializeField] RTLTextMeshPro EquationSuffixText;
    public int EquationKeyNumber;
    public int EquationSufNumber;
    int totalSumKey;
    int totalSumSuf;
    void Start()
    {
        totalSumKey = 0;
        totalSumSuf = 0;
        rainManager.Init();
        thingsManager.Init();
        GetRandomEquation();
    }

    public void GetRandomEquation()
    {
        var random = DatabaseHolder4.Instance.EquationPrefix[Random.Range(0, DatabaseHolder4.Instance.EquationPrefix.Count - 1)];
        var sign = "";
        var rand = Random.Range(0, 2);
        if (rand == 0)
        {
            sign = "-";
        }
        EquationKeyNumber = int.Parse(random);
        if (sign.Equals("-"))
        {
            EquationKeyNumber *= -1;
        }
        if (random.Equals("1"))
        {
            EquationKeyText.transform.position += new Vector3(0, 0.05f, 0);
            random = "";
        }
        EquationKeyText.text = random+ sign;
        EquationKeyText.text += " x";


        var newText = "+";
        rand = Random.Range(0, 2);
        if (rand == 0)
        {
            newText = "-";
        }
        EquationSignText.text = newText;

        random = DatabaseHolder4.Instance.EquationSuffix[Random.Range(0, DatabaseHolder4.Instance.EquationSuffix.Count - 1)];
        EquationSuffixText.text = random;
        EquationSufNumber = int.Parse(random);
        if (newText.Equals("-"))
        {
            EquationSufNumber *= -1;
        }
        Debug.Log("random equation is " + EquationKeyNumber + " x "+ EquationSignText.text + " "+ EquationSufNumber);
    }

    public void ClickedOnRain(RainingCollectible rain)
    {
        bool isNegetive = false;
        var txt =string.Copy(rain.KeyText);
        Debug.Log("doing logic for " + txt);
        if (txt.Contains("-"))
        {
            isNegetive = true;
            txt=txt.Replace("-","");
            Debug.Log("removing negetive : " + txt);
        }
        if (txt.Contains("x"))
        {
            txt = txt.Replace("x","");
            if (txt.Equals(""))
            {
                txt = "۱";
            }
            Debug.Log("converting key " + txt);
            var num = GetEnglishNumber(txt);
            if (isNegetive) num *= -1;
            totalSumKey += num;
        }
        else
        {
            Debug.Log("converting constant " + txt);
            if (txt.Equals(""))
            {
                txt = "۱";
            }
            var num = GetEnglishNumber(txt);
            if (isNegetive) num *= -1;
            totalSumSuf += num;
        }
        Debug.Log("THE TOTAL SUMS ARE : " + totalSumKey + " & " + totalSumSuf);
        ClickedRain?.Invoke(rain);
        if (totalSumSuf == EquationSufNumber && totalSumKey == EquationKeyNumber)
        {
            EquationSolved();
        }
    }
    void EquationSolved()
    {

        Debug.Log("YOU WIN THE EQUATION");
        totalSumSuf = 0;
        totalSumKey = 0;
        GetRandomEquation();
        FinishedEquation?.Invoke();
    }
    int GetEnglishNumber(string text)
    {
        switch (text)
        {
            case "۱": return 1;
            case "۲": return 2;
            case "۳": return 3;
            case "۴": return 4;
            case "۵": return 5;
            case "۶": return 6;
            case "۷": return 7;
            case "۸": return 8;
            case "۹": return 9;
            case "۰۱": return 10;
            default: return 0;
        }
    }
}
