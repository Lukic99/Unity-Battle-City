using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 direction;
    private float speed = 15f;
    private int index;
    bool moving = true;
    public Player player;
    public Animator animator;
    public System.Action destroyed; // sort of like a event
    public AudioSource firingSound;
    public AudioSource tankExplosionSound;
    public AudioSource wallHitSound;
   
    void Start()
    {
        firingSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            this.transform.position += this.direction * this.speed * Time.deltaTime;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "brick" || other.gameObject.tag == "wall" || other.gameObject.tag == "enemy")
        {                        
            animator.SetTrigger("trigger");
            this.moving = false;
            animator.speed = 7f;
            destroyed.Invoke();
            Destroy(this.gameObject, 0.20f);
        }
        if(other.gameObject.tag == "enemy"){
            tankExplosionSound.Play();
        }
        if (other.gameObject.tag == "wall" || other.gameObject.tag == "brick"){
            wallHitSound.Play();
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
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 0.5f);
    }
}
