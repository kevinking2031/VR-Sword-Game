using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RengeGames.HealthBars;

public class PlayerController : MonoBehaviour
{
    public GameObject meleeMob;
    public TextMeshProUGUI centerText;
    public TextMeshProUGUI scoreText;
    public bool gameActive = false;
    public UltimateCircularHealthBar healthBar;

    public float health = 100;
    public int score = 0;

    private int damage = 20;
    private int spawnInterval = 5;
    private float currentInterval = 0.0f;

    private bool test = true;
    // Start is called before the first frame update
    void Start()
    {
        // setup
    }

    // Update is called once per frame
    void Update()
    {
        // do while gameActive
        // need to handle menu, game start (mob spawning) 
        // for melee mobs:
        /* 
        


        */   
        // if (gameActive) {
        //     if (currentInterval > spawnInterval) {
        //         spawnMelee();
        //         currentInterval = 0.0f;
        //     }

        //     currentInterval += Time.deltaTime;
        // }

        if (test){
            spawnMelee();
            test = false;
        }        
        
             
    }

    private void spawnMelee(){
        float x = (Random.Range(0,2)*2-1) * Random.Range(4.5f, 7.0f); // first eq gives -1 or 1, rest is spawn radius
        float z = (Random.Range(0,2)*2-1) * Random.Range(4.5f, 7.0f);
        Vector3 randPos = new Vector3(x, 0, z);

        randPos = new Vector3(3, 0, 7); // for testing

        GameObject meleeInstance = Instantiate(meleeMob, randPos, Quaternion.identity);
        meleeInstance.GetComponent<MeleeBehaviour>().pc = gameObject.GetComponent<PlayerController>();
    }

    public void increaseScore(int increase){
        score += increase;
        scoreText.text = "Score: " + score;
    }

    private void gameOver() {
        // handle gameover here
        gameActive = false;
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies) {
            Destroy(enemy.transform.parent.gameObject); // destroy parent since hitbox is a child of the prefab
        }
        centerText.text = "GAME OVER\nclick on the right D-pad to play again";
        centerText.enabled = true;
    }

    private void OnTriggerEnter(Collider other) {
        // damage taken 
        if (other.tag != "PlayerSword") {
            health -= damage;
            healthBar.SetPercent(health/100.0f);
        }

        if (health <= 0) {
            gameActive = false;
            gameOver();
        }
    }
}
