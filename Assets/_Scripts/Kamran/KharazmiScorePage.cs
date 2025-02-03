using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KharazmiScorePage : MonoBehaviour
{
    [SerializeField] int level;
    [SerializeField] string nextLevelName;
    [SerializeField] Transform GridParent;
    [SerializeField] Image Prefab;
    private void Start()
    {
        foreach(Transform t in GridParent)
        {
            Destroy(t.gameObject);
        }
        var score = PlayerPrefs.GetInt("Level" + level);
        for(int i = 0; i < score; i++)
        {
            Instantiate(Prefab, GridParent);
        }
    }
    public void GoNext()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
