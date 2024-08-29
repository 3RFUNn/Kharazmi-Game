using System;
using UnityEngine;
using RTLTMPro;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuizManager : MonoBehaviour
{
    public EquationBank equationBank;

    public RTLTextMeshPro equationText;

    public GameObject[] Answer;

    public RTLTextMeshPro[] Answer1Text;
    public RTLTextMeshPro[] Answer2Text;
    public RTLTextMeshPro[] Answer3Text;
    public RTLTextMeshPro[] Answer4Text;

    [SerializeField] private LevelHandler handler;

    
    


    public Image timerImage;

    public RTLTextMeshPro timerText;

    public GameObject quizPanel;
    public GameObject nextSceneButton;

    
    

    private int currentLevel;
    private int currentQuestion;
    private float timeRemaining;
    private bool isQuizActive;

    private void Start()
    {
        //StartQuiz();
        
    }

    public void StartQuiz()
    {

        currentLevel = 1;
        currentQuestion = 0;
        timeRemaining = 60f;
        isQuizActive = true;

        // Show the quiz panel
        quizPanel.SetActive(true);
        nextSceneButton.SetActive(false);

        LoadNextQuestion();
        
        
        StartCoroutine(Timer());
    }
     
    
    private void LoadNextQuestion()
    {
        List<Equation> equations;

        switch (currentLevel)
        {
            case 1:
                equations = equationBank.easyEquations;
                break;
            case 2:
                equations = equationBank.mediumEquations;
                break;
            case 3:
                equations = equationBank.hardEquations;
                break;
            default:
                equations = new List<Equation>();
                break;
        }

        // Randomly select an equation
        int randomIndex = Random.Range(0, equations.Count);
        Equation equation = equations[randomIndex];
        
        // Set the equation and answers
        equationText.text = equation.equationText;
        
        List<RTLTextMeshPro[]> Answerlist = new List<RTLTextMeshPro[]>();
        
        // adding the answers to a list
        Answerlist.Add(Answer1Text);
        Answerlist.Add(Answer2Text);
        Answerlist.Add(Answer3Text);
        Answerlist.Add(Answer4Text);
        
        // create 4 shuffled numbers from 0 to 3
        int[] shuffledArray = ShuffleFourNumbers();
        
        int[] ShuffleFourNumbers()
        {
            int[] numbers = { 0, 1, 2, 3 };
            for (int i = 0; i < numbers.Length; i++)
            {
                int randomIndex = Random.Range(0, numbers.Length);
                // Swap the numbers
                (numbers[i], numbers[randomIndex]) = (numbers[randomIndex], numbers[i]);
            }
            return numbers;
        }




        int answerIndex = shuffledArray[0];
        

        for (int i = 0; i < 4 ; i++)
        {
            Answerlist[answerIndex][i].text = equation.correctAnswer[i];
            Answerlist[(answerIndex + 1) % 4][i].text = equation.incorrectAnswer1[i];
            Answerlist[(answerIndex + 2) % 4][i].text = equation.incorrectAnswer2[i];
            Answerlist[(answerIndex + 3) % 4][i].text = equation.incorrectAnswer3[i];
            
            Debug.Log(shuffledArray[i] + 1);

        }
        

        
        
    }

    public void CheckAnswer(string answer)
    {
        if (isQuizActive)
        {
            if (true)
            {
                // Correct answer
                currentQuestion++;

                if (currentQuestion >= 3)
                {
                    // End of quiz
                    EndQuiz();
                }
                else
                {
                    LoadNextQuestion();
                }
            }
            else
            {
                // Incorrect answer
                // Handle incorrect answer (e.g., show a message, deduct points)
            }
        }
    }

    private void EndQuiz()
    {
        isQuizActive = false;

        // Show the next scene button
        quizPanel.SetActive(false);
        nextSceneButton.SetActive(true);
    }

    private IEnumerator Timer()
    {

        yield return new WaitForSeconds(1);
        
        while (isQuizActive)
        {
            timeRemaining -= Time.deltaTime;

            // Update timer UI
            timerText.text = timeRemaining.ToString("F0");
            timerImage.fillAmount = timeRemaining / 60f;

            if (timeRemaining <= 0)
            {
                // Time's up
                EndQuiz();
            }

            yield return null;
        }
    }
}
    
   