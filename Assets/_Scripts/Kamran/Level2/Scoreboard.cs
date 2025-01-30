using RTLTMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro scoreText;
    [SerializeField] RTLTextMeshPro percentage;
    [SerializeField] Image fillSprite;
    [SerializeField] int level;
    [SerializeField] string nextLevelName;
    public void GoNext()
    {
        SceneManager.LoadScene(nextLevelName);
    }
    private void Start()
    {
        var score= PlayerPrefs.GetInt("Level"+level);
        scoreText.text = score.ToString();
        float maxScore = level switch
        {
            3 => 3,
            _ => 10,
        };
        fillSprite.fillAmount = (float)score / maxScore;
        percentage.text = (((float)score * 100f)/(float)maxScore).ToString() + "%";
    }
}
