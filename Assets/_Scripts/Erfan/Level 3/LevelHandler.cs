using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private GameObject quiz;
    [SerializeField] private GameObject firstQuestion;
    [SerializeField] private GameObject secondQuestion;
    [SerializeField] private GameObject rightAnswer;
    [SerializeField] private GameObject wrongAnswer;
    
    public GameObject[] Answers;

    [SerializeField] private QuizManager manager;
    //[SerializeField] private GameObject statistics;


    private void Start()
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

    public void NextQuestion()
    {
        secondQuestion.SetActive(false);



    }
    
    
    public void EndQuiz()
    {
        manager.IsQuizActive = false;
        quiz.SetActive(false);
        

        
    }



    public async void AnswerChecker(GameObject checkedAnswer)
    {

        manager.IsQuizActive = false;
        
        if (manager.AnswerIndex.ToString().Equals(checkedAnswer.tag))
        {
            if (manager.CurrentLevel >= 3)
            {
                rightAnswer.SetActive(true);
                await Task.Delay(2000);
                rightAnswer.SetActive(false);
            }
            else
            {
                rightAnswer.SetActive(true);
                await Task.Delay(2000);
                rightAnswer.SetActive(false);
                secondQuestion.SetActive(true);
            }

            manager.CheckAnswer(true);
            

        }
        else
        {
            if (manager.CurrentLevel >= 3)
            {
                wrongAnswer.SetActive(true);
                await Task.Delay(2000);
                wrongAnswer.SetActive(false);
            }
            else
            {
                wrongAnswer.SetActive(true);
                await Task.Delay(2000);
                wrongAnswer.SetActive(false);
                secondQuestion.SetActive(true);
            }

            manager.CheckAnswer(false);

            
            
        }
        
        manager.IsQuizActive = true;
        

    }

}
