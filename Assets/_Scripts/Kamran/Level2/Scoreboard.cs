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
    public void GoNext()
    {
        SceneManager.LoadScene("Level 3");
    }
    private void Start()
    {
        var score= PlayerPrefs.GetInt("Level"+level);
        scoreText.text = score.ToString();
        fillSprite.fillAmount = (float)score / 10f;
        percentage.text = ((float)score * 10f).ToString() + "%";
    }
}
