using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScript : MonoBehaviour
{

    public AudioSource introMusic;

    void Start()
    {
        introMusic.Play();
    }
    public void StartTheGame(){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

   public void QuitTheGame(){
        Application.Quit();
    } 
}
