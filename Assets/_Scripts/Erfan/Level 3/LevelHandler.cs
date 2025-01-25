using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFXSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private GameObject quiz;
    [SerializeField] private GameObject firstQuestion;
    [SerializeField] private GameObject secondQuestion;
    [SerializeField] private GameObject rightAnswer;
    [SerializeField] private GameObject wrongAnswer;
    [SerializeField] private GameObject EquationsParent;

    [SerializeField] private GameObject equationFinished;
    [SerializeField] private GameObject timeFinished;
    [SerializeField] private QuizManager manager;
    //[SerializeField] private GameObject statistics;


    private void Start()
    {
        quiz.SetActive(false);
        firstQuestion.SetActive(true);
        secondQuestion.SetActive(false);
        EquationsParent.SetActive(true);
        rightAnswer.SetActive(false);
        wrongAnswer.SetActive(false);
        equationFinished.SetActive(false);
        timeFinished.SetActive(false);
        SoundSystemManager.Instance.ChangeBGM("Music2");
        SoundSystemManager.Instance.PlayBGM();
        //statistics.SetActive(false);
    }


    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FirstLevel()
    {
        SceneManager.LoadScene("Menu");
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
        EquationsParent.SetActive(true);



    }
    
    
    public void EndQuiz()
    {
        manager.IsQuizActive = false;
        quiz.SetActive(false);
        equationFinished.SetActive(true);
        
        

        
    }
    
    public void EndQuiz_Time()
    {
        manager.IsQuizActive = false;
        quiz.SetActive(false);
        timeFinished.SetActive(true);
        

        
    }



    public async void AnswerChecker(GameObject checkedAnswer)
    {

        manager.IsQuizActive = false;
        
        if (manager.AnswerIndex.ToString().Equals(checkedAnswer.tag))
        {
            if (manager.CurrentLevel >= 3)
            {
                //rightAnswer.SetActive(true);
                await Task.Delay(2000);
               // rightAnswer.SetActive(false);
            }
            else
            {
                //rightAnswer.SetActive(true);
                await Task.Delay(2000);
                //rightAnswer.SetActive(false);
                secondQuestion.SetActive(true);
                EquationsParent.SetActive(false);
            }

            manager.CheckAnswer(true);
            

        }
        else
        {
            if (manager.CurrentLevel >= 3)
            {
                //wrongAnswer.SetActive(true);
                await Task.Delay(2000);
                //wrongAnswer.SetActive(false);
            }
            else
            {
                ///wrongAnswer.SetActive(true);
                await Task.Delay(2000);
                //wrongAnswer.SetActive(false);
                secondQuestion.SetActive(true);
                EquationsParent.SetActive(false);
            }

            manager.CheckAnswer(false);

            
            
        }
        
        manager.IsQuizActive = true;
        

    }

}
