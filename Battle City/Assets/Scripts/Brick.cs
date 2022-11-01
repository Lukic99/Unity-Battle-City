using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "bullet" || other.gameObject.tag == "enemyBullet" ){
            Destroy(gameObject);
        }
    }
}
