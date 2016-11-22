using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private GameObject playerPrefab;
    private GameObject player;
    public static long Score = 0;
    public int Round = 0;
    private int toSpawn = 1;
    private bool hasDied = false;
    private Health Health;
    public GameObject[] spawned = new GameObject[900];
    private GameObject[] spawnPoints;

    private GameObject basicEnemy;
    private GameObject fastEnemy;
    private GameObject shootEnemy;
    private GameObject dupeEnemy;
    

    public GameObject SCORE;
    public GameObject HEALTH;
    public GameObject ROUND;
    public GameObject GAMEOVER;

    private Text scoreText;
    private Text healthText;
    private Text roundText;
    void Start () {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
        //DEBUG OUT
        Debug.Log("I found " + spawnPoints.Length + " Spawn Points!");

        basicEnemy = Resources.Load<GameObject>("Prefabs/Enemies/Enemy");
        fastEnemy = Resources.Load<GameObject>("Prefabs/Enemies/Fast Enemy");
        shootEnemy = Resources.Load<GameObject>("Prefabs/Enemies/Shooting Enemy");
        dupeEnemy = Resources.Load<GameObject>("Prefabs/Enemies/Duplicator Enemy");

        scoreText = SCORE.GetComponent<Text>();
        healthText = HEALTH.GetComponent<Text>();
        roundText = ROUND.GetComponent<Text>();

        playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
        player = (GameObject)Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
        Health = player.GetComponent<Health>();
        
        Round++;
        toSpawn = Round + (int)(Mathf.Round(Random.Range(0f, Round)));
        for (int i = 0; i < toSpawn; i++) {
            int sp = (int)(Mathf.Round(Random.Range(0f, (spawnPoints.Length - 1))));
            spawned[i] = (GameObject)Instantiate(getEnemy(), spawnPoints[sp].transform.position, Quaternion.identity);
        }
    }
	public static void addScore(long s) {
        Score += s;
    }
	void Update () {
	    if(scoreText.text != Score.ToString()) {
            scoreText.text = Score.ToString();
        }
        if(healthText.text != Health.GetHealth().ToString()) {
            healthText.text = Health.GetHealth().ToString();
        }
        if(roundText.text != Round.ToString()) {
            roundText.text = Round.ToString();
        }
        if (player == null && hasDied == false) {
            hasDied = true;
            GAMEOVER.SetActive(true);
            for (int i = 0; i < toSpawn; i++) {
                Destroy(spawned[i]);
            }
        }
        handleRound();
	}

    public void resetGame() {
        Score = 0;
        Round = 0;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void handleRound() {
        if (hasDied) return;
        if(checkDead()) {
            Round++;
            toSpawn = 1 + Round + (int)(Mathf.Round(Random.Range(0f, Round)));
            for(int i = 0; i < toSpawn; i++) {
                int sp = (int)(Mathf.Round(Random.Range(0f, 4f)));
                spawned[i] = (GameObject)Instantiate(getEnemy(), spawnPoints[sp].transform.position, Quaternion.identity);
            }
        }
    }
    public bool checkDead() {
        for(int i = 0; i < toSpawn; i++) {
            if (spawned[i] != null) {
                return false;
            }
        }
        return true;
    }
    //Selects a spawnpoint (this returns an int to be used with the array)
    private int selectSP() {
        return (int)Mathf.Round(Random.Range(0f, 3.1f));
    }

    //This returns a random enemy prefab based on what round the game is on
    private GameObject getEnemy() {
        if(Round < 5) {
            return basicEnemy;
        } else {
            float ran = Mathf.Round(Random.Range(0f, 3f));
            if(Round >= 15) {
                if (ran == 0) {
                    return fastEnemy;
                } else if (ran == 1) {
                    return shootEnemy;
                } else if (ran == 2) {
                    return dupeEnemy;
                } else return basicEnemy;
            } else if(Round >= 10) {
                if(ran == 0) {
                    return fastEnemy;
                }else if(ran == 1) {
                    return dupeEnemy;
                } else {
                    return basicEnemy;
                }
            } else {
                if (ran >= 2) {
                    return fastEnemy;
                } else return basicEnemy;
            }
        }
    }
}
