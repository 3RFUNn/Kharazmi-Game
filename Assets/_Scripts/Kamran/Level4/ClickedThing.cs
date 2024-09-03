using RTLTMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedThing : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro text;
    public void Repaint(string text)
    {
        text = ReverseString(text);
        this.text.text = text;
    }

    static string ReverseString(string input)
    {
        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}
