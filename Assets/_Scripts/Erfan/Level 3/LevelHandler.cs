using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private GameObject quiz;
    [SerializeField] private GameObject firstQuestion;
    [SerializeField] private GameObject secondQuestion;
    [SerializeField] private GameObject rightAnswer;
    [SerializeField] private GameObject wrongAnswer;

    [SerializeField] private QuizManager manager;
    //[SerializeField] private GameObject statistics;


    private void Awake()
    {
        quiz.SetActive(false);
        firstQuestion.SetActive(true);
        secondQuestion.SetActive(false);
        rightAnswer.SetActive(false);
        wrongAnswer.SetActive(false);
        //statistics.SetActive(false);
    }


    public void NextLevel()
    {
        SceneManager.LoadScene("");
    }

    public void QuizLoader()
    {
        firstQuestion.SetActive(false);
        quiz.SetActive(true);
        
        manager.StartQuiz();
        
    }
    
    
    


    


}
