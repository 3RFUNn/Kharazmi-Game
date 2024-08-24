using UnityEngine;
using RTLTMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public EquationBank equationBank;

    public RTLTextMeshPro equationText;
    // public RTLTextMeshPro answer1Text;
    // public RTLTextMeshPro answer2Text;
    // public RTLTextMeshPro answer3Text;
    // public RTLTextMeshPro answer4Text;


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
        StartQuiz();
    }

    public void StartQuiz()
    {
        
        currentLevel = 1;
        currentQuestion = 0;
        timeRemaining = 40f;
        isQuizActive = true;

        // Show the quiz panel
        quizPanel.SetActive(true);
        nextSceneButton.SetActive(false);

        LoadNextQuestion();
        StartCoroutine(Timer());
    }
     
    public static void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            int k = Random.Range(0, n--);
            (list[k], list[n]) = (list[n], list[k]);
        }
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
        
        // Randomly select a correct answer from the list
        int randomCorrectAnswerIndex = Random.Range(0, equation.correctAnswerText.Count);
        GameObject correctAnswer = equation.correctAnswerText[randomCorrectAnswerIndex];

        // Randomly select 3 incorrect answers from the list
        List<GameObject> incorrectAnswers = new List<GameObject>();
        while (incorrectAnswers.Count < 3)
        {
            int randomIncorrectAnswerIndex = Random.Range(0, equation.incorrectAnswerTexts.Count);
            GameObject incorrectAnswer = equation.incorrectAnswerTexts[randomIncorrectAnswerIndex];
            if (!incorrectAnswers.Contains(incorrectAnswer))
            {
                incorrectAnswers.Add(incorrectAnswer);
            }
        }
      
        // Create a list of all answers (correct and incorrect)
        List<GameObject> allAnswers = new List<GameObject>();
        allAnswers.Add(correctAnswer);
        allAnswers.AddRange(incorrectAnswers);
        
        

        // Shuffle the list of all answers
        Shuffle(allAnswers);

        // Assign shuffled answers to UI elements
    //     answer1Text.text = allAnswers[0];
    //     answer2Text.text = allAnswers[1];
    //     answer3Text.text = allAnswers[2];
    //     answer4Text.text = allAnswers[3];
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
        while (isQuizActive)
        {
            timeRemaining -= Time.deltaTime;

            // Update timer UI
            timerText.text = timeRemaining.ToString("F0");
            timerImage.fillAmount = timeRemaining / 40f;

            if (timeRemaining <= 0)
            {
                // Time's up
                EndQuiz();
            }

            yield return null;
        }
    }
}
    
   