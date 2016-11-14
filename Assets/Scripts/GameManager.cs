using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private GameObject playerPrefab;
    private GameObject player;
    public static long Score = 0;
    public static int Round = 1;
    private Health Health;
    private GameObject[] spawned;
    public GameObject[] spawnPoints = new GameObject[4];

    private GameObject basicEnemy;
    private GameObject fastEnemy;
    private GameObject shootEnemy;
    private GameObject dupeEnemy;

    public GameObject SCORE;
    public GameObject HEALTH;
    public GameObject ROUND;

    private Text scoreText;
    private Text healthText;
    private Text roundText;
    void Start () {
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
	}
    //Selects a spawnpoint (this returns an int to be used with the array)
    private int selectSP() {
        return (int)Mathf.Round(Random.Range(0f, 3.1f));
    }
    //private GameObject getEnemy() {
    //    if(Round < 5) {
    //        return basicEnemy;
    //    } else {
    //        float ran = Mathf.Round(Random.Range(0f, 3.1f));
    //        if(Round >= 20) {
    //
    //        }else if(Round >= 15) {
    //
    //        }else if(Round >= 10) {
    //
    //        } else {
    //
    //        }
    //    }
    //}
}
