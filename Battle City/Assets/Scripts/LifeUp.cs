using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUp : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;
    void Start()
    {
        StartCoroutine(DestroyIfNotPickedUp());
    }

    private IEnumerator DestroyIfNotPickedUp(){
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
    }

    void Update()
    {
       
    }
    void OnCollisionEnter2D(Collision2D other)
    {
       if(other.gameObject.tag == "player"){
            Destroy(gameObject);
        } 
    }
}
