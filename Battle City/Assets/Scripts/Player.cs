using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public float speed = 5f;
    public Rigidbody2D rb;
    public Projectile projectile;
    float direction;//direction of projectile
    private bool projectileActive;
    private bool fullAutoFire = false;
    public System.Action destroyed;
    public System.Action lifePickedUp;

    void FixedUpdate() 
    {

        Vector3 position = transform.position;
        float movement;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movement = position.x - this.speed * Time.deltaTime;
            rb.MovePosition(new Vector2(movement, position.y));
            transform.rotation = Quaternion.Euler(0, 0, 90);
            direction = 90f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movement = position.x + this.speed * Time.deltaTime;
            rb.MovePosition(new Vector2(movement, position.y));
            transform.rotation = Quaternion.Euler(0, 0, -90);
            direction = -90f;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movement = position.y + this.speed * Time.deltaTime;
            rb.MovePosition(new Vector2(position.x, movement));
            transform.rotation = Quaternion.Euler(0, 0, 0);
            direction = 0f;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movement = position.y - this.speed * Time.deltaTime;
            rb.MovePosition(new Vector2(position.x, movement));
            transform.rotation = Quaternion.Euler(0, 0, 180);
            direction = 180f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        { 
            Shoot(direction);
        }

    }

    private void Shoot(float direction)
    {
        Projectile proj;
        if (!projectileActive || fullAutoFire)
        {
            switch (direction)
            {
                case 0f:
                    this.projectile.direction = new Vector3(0, 1, 0);
                    proj = Instantiate(this.projectile, rb.position, Quaternion.identity);
                    proj.destroyed += ProjectileDestroyed;
                    projectileActive = true;
                    break;
                case 180f:
                    this.projectile.direction = new Vector3(0, -1, 0);
                    proj = Instantiate(this.projectile, rb.position, Quaternion.identity);
                    proj.destroyed += ProjectileDestroyed;
                    projectileActive = true;
                    break;
                case 90f:
                    this.projectile.direction = new Vector3(-1, 0, 0);
                    proj = Instantiate(this.projectile, rb.position, Quaternion.Euler(0, 0, direction));
                    proj.destroyed += ProjectileDestroyed;
                    projectileActive = true;
                    break;
                case -90f:
                    this.projectile.direction = new Vector3(1, 0, 0);
                    proj = Instantiate(this.projectile, rb.position, Quaternion.Euler(0, 0, direction));
                    proj.destroyed += ProjectileDestroyed;
                    projectileActive = true;
                    break;
            }
        }
    }

    private void ProjectileDestroyed()
    {
        this.projectileActive = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "speedUp")
        {
             StartCoroutine(IncreaseSpeed());
        }
        if (other.gameObject.tag == "infiniteAmmo")
        {
            StartCoroutine(FullAuto());
        }
        if (other.gameObject.tag == "lifeUp")
        {
            lifePickedUp.Invoke();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemyBullet")
        {
            destroyed.Invoke();
            Destroy(gameObject);
        }
    }
    private IEnumerator FullAuto(){
        this.fullAutoFire = true;
        yield return new WaitForSeconds(6);
        this.fullAutoFire = false;
    }
    private IEnumerator IncreaseSpeed(){
        this.speed = 10f;
        yield return new WaitForSeconds(6);
        this.speed = 5f;
    }
}
