using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private GameObject playerPrefab;
    private GameObject player;
    public static long Score = 0;
    private Health Health;

    public GameObject SCORE;
    public GameObject HEALTH;

    private Text scoreText;
    private Text healthText;
	void Start () {
        scoreText = SCORE.GetComponent<Text>();
        healthText = HEALTH.GetComponent<Text>();
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
	}
}
