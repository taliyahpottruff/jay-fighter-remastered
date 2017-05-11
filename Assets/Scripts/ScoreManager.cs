using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ScoreManager : NetworkBehaviour {
    public long score = 0;
    public long coins = 0;
    public int round = 0;

    public GameObject SCORE;
    public GameObject HEALTH;
    public GameObject COINS;
    public GameObject HEALTHTX;
    public GameObject ROUND;
    
    public Animator healthAnim;
    public Animator statsAnim;

    private Text scoreText;
    private Slider healthSlider;
    private Text healthText;
    private Text coinsText;
    private Text roundText;

    //Unknown
    private Health health;

    private void Start() {
        scoreText = SCORE.GetComponent<Text>();
        healthText = HEALTHTX.GetComponent<Text>();
        healthSlider = HEALTH.GetComponent<Slider>();
        coinsText = COINS.GetComponent<Text>();
        roundText = ROUND.GetComponent<Text>();

        healthAnim.Play("healthPanel-in");
        statsAnim.Play("statsPanel-in");
    }

    private void Update() {
        if (scoreText.text != score.ToString()) {
            scoreText.text = score.ToString();
        }
        if (health != null) {
            if (healthText.text != health.GetHealth().ToString()) {
                healthText.text = health.GetHealth().ToString();
            }
            if (coinsText.text != ("$" + coins.ToString())) {
                coinsText.text = "$" + coins.ToString();
            }
            if (healthSlider.value != health.GetHealth()) {
                healthSlider.value = health.GetHealth();
            }
            if (healthSlider.maxValue != health.GetMaxHealth()) {
                healthSlider.maxValue = health.GetMaxHealth();
            }
        }
        if (roundText.text != round.ToString()) {
            roundText.text = round.ToString();
        }
    }
}