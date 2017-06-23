using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

/*
    * AUTHOR: Garrett Nicholas
    * MODIFICATIONS: Trenton Pottruff
*/

public class GameManager : MonoBehaviour {
    private GameObject playerPrefab;
    private GameObject player;
    public bool hasStarted = false;
    public static long Score = 0;
    public static long Coins = 0;
    public int round = 0;
    private int toSpawn = 1;
    private bool hasDied = false;
    private Health Health;
    public GameObject[] spawned = new GameObject[900];
    private GameObject[] spawnPoints;
    private NetworkManager nm;
    public GameObject GAMEOVER;

    private GameObject basicEnemy;
    private GameObject fastEnemy;
    private GameObject shootEnemy;
    private GameObject dupeEnemy;
    private GameObject[] players;
    
    private void Start() {
        Game.PAUSED = false;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bullets"), LayerMask.NameToLayer("Drops"), true);

        basicEnemy = Resources.Load<GameObject>("Prefabs/Enemies/Drone");
        fastEnemy = Resources.Load<GameObject>("Prefabs/Enemies/Speedster");
        shootEnemy = Resources.Load<GameObject>("Prefabs/Enemies/Shooting Enemy");
        dupeEnemy = Resources.Load<GameObject>("Prefabs/Enemies/Duplicator Enemy");

        /* Now in ScoreManager
        scoreText = SCORE.GetComponent<Text>();
        healthText = HEALTHTX.GetComponent<Text>();
        healthSlider = HEALTH.GetComponent<Slider>();
        coinsText = COINS.GetComponent<Text>();
        roundText = ROUND.GetComponent<Text>();*/

        playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
        //player = (GameObject)Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);

        nm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<NetworkManager>();

        //TP: Added a coroutine
        StartCoroutine(DelayedStartSpawn());
    }
	public static void addScore(long s) {
        Score += s;
    }
	void Update () {
        #region Trenton Pottruff: Get Player List
        players = GameObject.FindGameObjectsWithTag("Player");
        #endregion

        if (hasStarted) {
            /* Old Game Over Code
             * if (player == null && hasDied == false) {
                Debug.Log("Player has died!");
                NetworkManager.singleton.StopHost();
                hasDied = true;
                GAMEOVER.SetActive(true);
                for (int i = 0; i < toSpawn; i++) {
                    Destroy(spawned[i]);
                }
            }*/
            #region Trenton Pottruff: Game Over
            if (players.Length < 1) {
                NetworkManager.singleton.StopHost();
            }
            #endregion
            handleRound();
        }
	}

    public void resetGame() {
        NetworkManager.singleton.StopHost();
        Score = 0;
        Coins = 0;
        round = 0;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void goToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    public void handleRound() {
        if (hasDied) return;
        if(checkDead()) {
            round++;
            toSpawn = 1 + round + (int)(Mathf.Round(Random.Range(0f, round)));
            for(int i = 0; i < toSpawn; i++) {
                int sp = selectSP();
                spawned[i] = (GameObject)Instantiate(getEnemy(), spawnPoints[sp].transform.position, Quaternion.identity);
                NetworkServer.Spawn(spawned[i]);
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
        return (int)Mathf.Round(Random.Range(0f, spawnPoints.Length - 1));
    }

    //This returns a random enemy prefab based on what round the game is on
    private GameObject getEnemy() {
        if(round < 5) {
            return basicEnemy;
        } else {
            float ran = Mathf.Round(Random.Range(0f, 3f));
            if(round >= 15) {
                if (ran == 0) {
                    return fastEnemy;
                } else if (ran == 1) {
                    return shootEnemy;
                } else if (ran == 2) {
                    return dupeEnemy;
                } else return basicEnemy;
            } else if(round >= 10) {
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

    //AUTHOR: Trenton Pottruff
    private IEnumerator DelayedStartSpawn() {
        yield return new WaitForSeconds(1f);
        player = GameObject.FindGameObjectWithTag("Player");
        Health = player.GetComponent<Health>();

        //Play HUD Animations
        /*healthAnim.Play("healthPanel-in");
        statsAnim.Play("statsPanel-in");*/

        //Spawning code originally by Garrett Nicholas
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
        //DEBUG OUT
        Debug.Log("I found " + spawnPoints.Length + " Spawn Points!");
        round++;
        toSpawn = round + (int)(Mathf.Round(Random.Range(0f, round)));
        for (int i = 0; i < toSpawn; i++) {
            int sp = selectSP();
            spawned[i] = (GameObject)Instantiate(getEnemy(), spawnPoints[sp].transform.position, Quaternion.identity);
            NetworkServer.Spawn(spawned[i]);
        }
        hasStarted = true;
    }
}
