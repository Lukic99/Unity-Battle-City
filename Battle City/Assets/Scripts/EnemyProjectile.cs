using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyProjectile : MonoBehaviour
{
    public Vector3 direction;
    private float speed = 15f;
    private int index;
    bool moving = true;
    public Animator animator;
    void Start()
    {

    }
    void Update()
    {
        if (moving)
        {
            this.transform.position += this.direction * this.speed * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "brick" || other.gameObject.tag == "wall" || other.gameObject.tag == "player") //|| other.gameObject.tag == "enemy"
        { // or add a new Layer and configure layer colliding
            animator.SetTrigger("trigger");
            this.moving = false;
            animator.speed = 7f;
            Destroy(this.gameObject, 0.25f);
        }
        if (other.gameObject.tag == "base"){
            ShowDefeatScene();
        }
        
    }
    private void ShowDefeatScene(){
        SceneManager.LoadScene(5);
    }
    public void _TriggerAnimation()
    {
        animator.SetTrigger("trigger");
        animator.speed = 5;
        //    animator.
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 0.5f);

    }
}
