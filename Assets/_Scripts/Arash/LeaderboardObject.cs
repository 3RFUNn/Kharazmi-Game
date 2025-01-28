using System;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardObject : MonoBehaviour
{
    public Image Profile;
    public RTLTextMeshPro Name;
    public RTLTextMeshPro Rank;
    public RTLTextMeshPro RankNumber;

    public void Setup(String name,String rank,String number,Sprite sprite){
        Profile.sprite=sprite;
        Name.text=name;
        Rank.text=rank;
        RankNumber.text=number;

    }
}
