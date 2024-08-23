using System;
using System.Collections;
using System.Collections.Generic;
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
    public List<string> correctAnswerText;
    public List<string> incorrectAnswerTexts;
}



