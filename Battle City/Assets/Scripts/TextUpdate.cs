using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextUpdate : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Remaining Tanks: " + gameManager.getLivesRemaining();
        gameManager.livesChanged += UpdateRemainingLivesText;
    }

    public void UpdateRemainingLivesText()
    {
        text.text = "Remaining Tanks: " + gameManager.getLivesRemaining();
    }
}
