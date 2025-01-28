using System;
using RTLTMPro;
using UnityEngine;

public class StatObject : MonoBehaviour
{
    public RTLTextMeshPro difText;
    public RTLTextMeshPro scoreText;

    public void Setup(String difficulty, String score){
        difText.text=difficulty;
        scoreText.text=score;

    }
}
