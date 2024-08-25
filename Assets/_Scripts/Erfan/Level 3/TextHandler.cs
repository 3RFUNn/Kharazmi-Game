using System.Collections;
using System.Collections.Generic;
using RTLTMPro;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class TextHandler : MonoBehaviour
{
    // Reference to RTLTextMeshPro components
    public RTLTextMeshPro rtlText1;
    public RTLTextMeshPro rtlText2;
    public RTLTextMeshPro rtlText3;
    public RTLTextMeshPro rtlText4;

    // String fields to manually input text
    [TextArea(3, 10)]
    public string text1;
    [TextArea(3, 10)]
    public string text2;
    [TextArea(3, 10)]
    public string text3;
    [TextArea(3, 10)]
    public string text4;

    // This function will be called whenever the script is updated in the editor
    void OnValidate()
    {
        // Check if all text components are assigned and set the corresponding text
        if (rtlText1 != null)
        {
            rtlText1.text = text1;
        }
        if (rtlText2 != null)
        {
            rtlText2.text = text2;
        }
        if (rtlText3 != null)
        {
            rtlText3.text = text3;
        }
        if (rtlText4 != null)
        {
            rtlText4.text = text4;
        }
    }
}
