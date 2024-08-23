using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public EquationBank equationBank;

    public TextMeshProUGUI equationText;
    public TextMeshProUGUI answer1Text;
    public TextMeshProUGUI answer2Text;
    public TextMeshProUGUI answer3Text;
    public TextMeshProUGUI answer4Text;

    public
        Image timerImage;

    public TextMeshProUGUI timerText;

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
            int k = Random.Range(0,
                n--);
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
        answer1Text.text = equation.correctAnswerText;
        List<string> incorrectAnswers = equation.incorrectAnswerTexts;
        Shuffle(incorrectAnswers); // Randomize incorrect answers
        answer2Text.text = incorrectAnswers[0];
        answer3Text.text = incorrectAnswers[1];
        answer4Text.text = incorrectAnswers[2];
    }

    public void CheckAnswer(string answer)
    {
        if (isQuizActive)
        {
            if (answer == answer1Text.text)
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
    
   