using RTLTMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level2Manager : MonoBehaviour
{
    [SerializeField] SpawnCollectibles spawnCollectibles;
    [SerializeField] PlayerManager player;
    [SerializeField] GameObject GameObj;
    [SerializeField] GameObject Canvas;
    [SerializeField] List<Button> Buttons;
    private void Start()
    {
        GameObj.SetActive(false);
        Canvas.SetActive(true);
        foreach(var b in Buttons)
        {
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(() =>
            {
                StartGame(b.transform.GetChild(0).GetComponent<RTLTextMeshPro>().text);
            });
        }
    }
    void StartGame(string key)
    {
        player.Init(key);
        spawnCollectibles.SpawnCollectible();
        Canvas.SetActive(false);
        GameObj.SetActive(true);
    }
    public static List<int> GetUniqueRandomNumbers(int count, int min, int max)
    {
        if (count > (max - min + 1))
        {
            Debug.LogError("Count is greater than the range of numbers available.");
            return null;
        }

        List<int> numbers = new();
        System.Random rand = new();

        while (numbers.Count < count)
        {
            int num = rand.Next(min, max + 1);
            if (!numbers.Contains(num))
            {
                numbers.Add(num);
            }
        }

        return numbers;
    }
}
