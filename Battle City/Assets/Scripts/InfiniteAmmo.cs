using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteAmmo : MonoBehaviour
{
    public GameManager gameManager;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyIfNotPickedUp());
    }

    private IEnumerator DestroyIfNotPickedUp(){
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
    }
     void OnCollisionEnter2D(Collision2D other)
    {
       if(other.gameObject.tag == "player"){
            Destroy(gameObject);
        } 
    }


}
