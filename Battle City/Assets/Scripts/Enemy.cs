using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb ;
    public EnemyProjectile projectile;
    float direction; //direction of projectile
    int tankDirection; //will be hardcoded to [1-4]
    public System.Action destroyed;
    public GameManager gameManager;

    void Start()
    {
        this.tankDirection = Randomizer();
        StartCoroutine(Shoot()); 
    }
    void FixedUpdate()
    {  
        Vector3 position = transform.position;
        float movement;
        if (this.tankDirection == 1) {
            movement = position.x - this.speed*Time.deltaTime;
            rb.MovePosition(new Vector2(movement,position.y));
            transform.rotation = Quaternion.Euler(0,0,90);
            this.direction = 90f;
        }
        else if (this.tankDirection == 2) {
            movement = position.x + this.speed*Time.deltaTime;
            rb.MovePosition(new Vector2(movement,position.y));
            transform.rotation = Quaternion.Euler(0,0,-90);
            this.direction = -90f;
        }
        else if (this.tankDirection == 3) {
            movement = position.y + this.speed*Time.deltaTime;
            rb.MovePosition(new Vector2(position.x, movement));
            transform.rotation = Quaternion.Euler(0,0,0);
            this.direction = 0f;
        }
        else if (this.tankDirection == 4) {
            movement = position.y - this.speed*Time.deltaTime;
            rb.MovePosition(new Vector2(position.x, movement));
            transform.rotation = Quaternion.Euler(0,0,180);
            this.direction = 180f;
        }
    }

    private int Randomizer(){
        return  Random.Range(1,5);
    }

    private IEnumerator Shoot( ){
        int rand = Random.Range(1,5);
        yield return new WaitForSeconds(rand);
        switch(direction){
            case 0f:
                this.projectile.direction = new Vector3(0,1,0);
                Instantiate(this.projectile, rb.position, Quaternion.identity);
                break;
            case 180f:
                this.projectile.direction = new Vector3(0,-1,0);
                Instantiate(this.projectile, rb.position, Quaternion.identity);
                break;
            case 90f:
                this.projectile.direction = new Vector3(-1,0,0);
                Instantiate(this.projectile, rb.position, Quaternion.Euler(0,0,direction));
                break;
            case -90f:
                this.projectile.direction = new Vector3(1,0,0);
                Instantiate(this.projectile, rb.position, Quaternion.Euler(0,0,direction));
                break;
        }
        StartCoroutine(Shoot());
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "brick" || collision.gameObject.tag == "wall" || collision.gameObject.tag == "enemy" || 
            collision.gameObject.tag == "player" || collision.gameObject.tag == "water"){
            int temp = this.tankDirection;
            this.tankDirection = Randomizer();
            if(this.tankDirection == temp){
                while (this.tankDirection == temp){
                    this.tankDirection = Randomizer();
                }
            }
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "bullet"){
            destroyed.Invoke();
            Destroy(gameObject);
        }
    }
}
