using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishScript : MonoBehaviour
{
    public AudioSource endingMusic;
    public AudioSource defeatMusic;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            defeatMusic.Play();
        }
        else
        {
            endingMusic.Play();
        }
    }
    public void restartGame()
    {
        SceneManager.LoadScene(0);
    }
}
