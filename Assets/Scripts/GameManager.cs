using UnityEngine;
using System.Collections;
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

        playerPrefab = Resources.Load<GameObject>("Prefabs/Player");

        nm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<NetworkManager>();

        //TP: Added a coroutine
        StartCoroutine(DelayedStartSpawn());
    }

    /// <summary>
    /// Adds a number to the score
    /// </summary>
    /// <param name="s">The amount to add</param>
	public static void addScore(long s) {
        Score += s;

        if (Game.IS_MP) {
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + (int)s);
            PlayerPrefs.Save();
        }
    }

	void Update () {
        #region Trenton Pottruff: Get Player List
        players = GameObject.FindGameObjectsWithTag("Player");
        #endregion

        if (hasStarted) {
            #region Trenton Pottruff: Game Over
            if (players.Length < 1) { //When there's no players left
                GAMEOVER.SetActive(true);
                NetworkManager.singleton.StopHost();
            }
            #endregion
            handleRound();
        }
	}

    /// <summary>
    /// Resets all the stats
    /// </summary>
    public void resetGame() {
        NetworkManager.singleton.StopHost();
        Score = 0;
        Coins = 0;
        round = 0;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    /// <summary>
    /// Returns to main menu.
    /// </summary>
    public void goToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Goes to the next round
    /// </summary>
    public void handleRound() {
        if (hasDied) return;
        if(checkDead()) { //Are all enemies dead?
            round++;

            int pRound = PlayerPrefs.GetInt("highestRound");
            if (round > pRound) {
                PlayerPrefs.SetInt("highestRound", round);
                PlayerPrefs.Save();
            }

            toSpawn = 1 + round + (int)(Mathf.Round(Random.Range(0f, round)));
            for(int i = 0; i < toSpawn; i++) {
                int sp = selectSP(); //Select a spawn point
                spawned[i] = (GameObject)Instantiate(getEnemy(), spawnPoints[sp].transform.position, Quaternion.identity);
                NetworkServer.Spawn(spawned[i]);
            }
        }
    }

    /// <summary>
    /// Checks to see if all enemies are dead.
    /// </summary>
    /// <returns></returns>
    public bool checkDead() {
        for(int i = 0; i < toSpawn; i++) {
            if (spawned[i] != null) {
                return false;
            }
        }
        
        return true;
    }

    /// <summary>
    /// Selects a spawnpoint.
    /// </summary>
    /// <returns>An int to be used with a spawn point array</returns>
    private int selectSP() {
        return (int)Mathf.Round(Random.Range(0f, spawnPoints.Length - 1));
    }

    /// <summary>
    /// This returns a random enemy prefab based on what round the game is on
    /// </summary>
    /// <returns>An enemy prefab</returns>
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

        //Spawning code originally by Garrett Nicholas
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
        //DEBUG OUT
        round++;
        toSpawn = round + (int)(Mathf.Round(Random.Range(0f, round)));
        for (int i = 0; i < toSpawn; i++) {
            int sp = selectSP(); //Select a spawn point
            spawned[i] = (GameObject)Instantiate(getEnemy(), spawnPoints[sp].transform.position, Quaternion.identity);
            NetworkServer.Spawn(spawned[i]);
        }
        hasStarted = true;
    }
}
