using System;
using System.Collections;
using System.Collections.Generic;
using RTLTMPro;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "EquationBank", menuName = "Create/Equation Bank")]
public class EquationBank : ScriptableObject
{
    public List<Equation> easyEquations;
    public List<Equation> mediumEquations;
    public List<Equation> hardEquations;
}

[System.Serializable]
public struct Equation
{
    public string equationText;
    public List<GameObject> correctAnswerText;
    public List<GameObject> incorrectAnswerTexts;
    
   
}



