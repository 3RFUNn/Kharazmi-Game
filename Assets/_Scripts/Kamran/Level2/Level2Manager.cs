using RTLTMPro;
using SFXSystem;
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
    [SerializeField] GameObject SelectionPanel;
    [SerializeField] Button EndGameButton;
    [SerializeField] List<Button> Buttons;
    private void Start()
    {
        Application.targetFrameRate = 120;
        SoundSystemManager.Instance.ChangeBGM("Music");
        SoundSystemManager.Instance.PlayBGM();
        player.joystick.gameObject.SetActive(false);
        GameObj.SetActive(false);
        EndGameButton.gameObject.SetActive(false);
        SelectionPanel.SetActive(true);
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
        player.joystick.gameObject.SetActive(true);
        player.Init(key);
        spawnCollectibles.SpawnCollectible();
        SelectionPanel.SetActive(false);
        GameObj.SetActive(true);
        EndGameButton.gameObject.SetActive(true);
        EndGameButton.onClick.RemoveAllListeners();
        EndGameButton.onClick.AddListener(() =>
        {
            PlayerManager.Instance.GameOver(true);
        });
    }
    public static List<int> GetUniqueRandomNumbers(int count,int initialOveralMax)
    {
        List<int> numbers = new();
        System.Random rand = new();
        int min = 0;
        while (numbers.Count < count)
        {
            int tmp = rand.Next(0, 4);
            if (tmp != 0)
            {
                min = (initialOveralMax + numbers.Count+1) / 2;
            }
            int num = rand.Next(min, initialOveralMax + numbers.Count+1);
            min = 0;
            if (!numbers.Contains(num))
            {
                numbers.Add(num);
                numbers.Sort();
            }
        }

        return numbers;
    }
}
