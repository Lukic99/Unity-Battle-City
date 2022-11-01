using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    private int screenSize;

    [SerializeField] Player player;
    public LifeUp lifeUp;
    public InfiniteAmmo infiniteAmmo;
    public SpeedUp speedUp;
    private int livesRemaining;
    public Enemy enemy;
    private List<Enemy> enemies;
    private bool finishedGeneratingEnemies;
    public AudioSource nextLevelSound;
    public AudioSource playerDestroyedSound;
    
    public System.Action livesChanged;
    void Start()
    {       //               +screenSize
            // -screenSize <-- Camera --> +screenSize
            //               -screenSize
        screenSize = (int)(Camera.main.orthographicSize - 2); // 2 is subtractred for the width of the surrounding  walls
        this.livesRemaining = 3;
        this.player = Instantiate(player, new Vector3(-6, -14, 0), Quaternion.identity); // hardcoded Location
        player.destroyed += PlayerDestroyed;
        player.lifePickedUp += IncreaseLives;
        this.enemies = new List<Enemy>();
        this.finishedGeneratingEnemies = false;
        StartCoroutine(GeneratePowerUp());
        StartCoroutine(GenerateEnemies());
    }

    void Update()
    {
        if (finishedGeneratingEnemies)
        {
            if (this.enemies.Count == 0)
            {
                LoadNextLevel();
            }
        }
    }

    private void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 || SceneManager.GetActiveScene().buildIndex != 3)
        {
            nextLevelSound.Play();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public int getLivesRemaining(){
        return this.livesRemaining;
    }
    private void PlayerDestroyed()
    {
        playerDestroyedSound.Play();
        if (livesRemaining > 0)
        {
            this.player = Instantiate(player, new Vector3(-6, -14, 0), Quaternion.identity);
            player.destroyed += PlayerDestroyed;
            player.lifePickedUp += IncreaseLives;
            DecreaseLives();
        }
        else
        {
            SceneManager.LoadScene(5);
        }
    }
    private IEnumerator GenerateEnemies()
    {
        float enemyRow = screenSize - 1; // upper portion of scene where enemies are suposed to be summoned
        Enemy e;
        for (int i = 1; i <= 3; i++){
            e = Instantiate(enemy, new Vector3(-screenSize + 3, enemyRow, 0), Quaternion.identity);
            this.enemies.Add(e);
            e.destroyed += removeEnemy;
            yield return new WaitForSeconds(Random.Range(1, 3));
            e = Instantiate(enemy, new Vector3(0, enemyRow, 0), Quaternion.identity);
            this.enemies.Add(e);
            e.destroyed += removeEnemy;
            yield return new WaitForSeconds(Random.Range(1, 3));
            e = Instantiate(enemy, new Vector3(screenSize - 3, enemyRow, 0), Quaternion.identity);
            this.enemies.Add(e);
            e.destroyed += removeEnemy;
            yield return new WaitForSeconds(Random.Range(2, 4));

        }
        finishedGeneratingEnemies = true;
    }

    public void removeEnemy()
    {
        int lastElement = enemies.Count - 1;
        this.enemies.RemoveAt(enemies.Count - 1);
    }
    private IEnumerator GeneratePowerUp()
    {
        yield return new WaitForSeconds(10);
        int power = Random.Range(1, 4);
        switch (power)
        {
            case 1:
                Instantiate(this.lifeUp, new Vector3(Randomize(screenSize), Randomize(screenSize), 0.5f), Quaternion.identity);
                break;
            case 2:
                Instantiate(this.infiniteAmmo, new Vector3(Randomize(screenSize), Randomize(screenSize), 0.52f), Quaternion.identity);
                break;
            case 3:
                Instantiate(this.speedUp, new Vector3(Randomize(screenSize), Randomize(screenSize), 0.5f), Quaternion.identity);
                break;
        }
        StartCoroutine(GeneratePowerUp());
    }
    private int Randomize(int br)
    {
        return Random.Range(-br, br);
    }


    public void IncreaseLives()
    {
        this.livesRemaining++;
        livesChanged.Invoke();
    }

    public void DecreaseLives()
    {
        this.livesRemaining--;
        livesChanged.Invoke();
    }

}
