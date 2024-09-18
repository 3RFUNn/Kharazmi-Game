using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private int seconds;
    void Start()
    {
        Next();
    }

    private async void Next()
    {
        
        await Task.Delay(seconds * 1000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
