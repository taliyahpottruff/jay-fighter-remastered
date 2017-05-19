﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ScoreManager : NetworkBehaviour {
    private Player player;
    
    public GameManager gameManager;

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

    //Player Stuff
    private GameObject playerObj;
    private Health health;

    private void Start() {

        scoreText = SCORE.GetComponent<Text>();
        healthText = HEALTHTX.GetComponent<Text>();
        healthSlider = HEALTH.GetComponent<Slider>();
        coinsText = COINS.GetComponent<Text>();
        roundText = ROUND.GetComponent<Text>();

        healthAnim.Play("healthPanel-in");
        statsAnim.Play("statsPanel-in");

        health = playerObj.GetComponent<Health>();
        player = playerObj.GetComponent<Player>();
    }

    private void Update() {
        if (playerObj == null) {
            playerObj = NetworkManager.singleton.client.connection.playerControllers[0].gameObject;
            return;
        }

        health = playerObj.GetComponent<Health>();
        player = playerObj.GetComponent<Player>();

        if (scoreText == null) {
            Debug.LogError("Uh oh");
        }

        if (scoreText.text != Mathf.FloorToInt(player.score * 1).ToString()) {
            scoreText.text = Mathf.FloorToInt(player.score * 1).ToString();
        }
        if (health != null) {
            if (healthText.text != health.GetHealth().ToString()) {
                healthText.text = health.GetHealth().ToString();
            }
            if (coinsText.text != ("$" + player.coins.ToString())) {
                coinsText.text = "$" + player.coins.ToString();
            }
            if (healthSlider.value != health.GetHealth()) {
                healthSlider.value = health.GetHealth();
            }
            if (healthSlider.maxValue != health.GetMaxHealth()) {
                healthSlider.maxValue = health.GetMaxHealth();
            }
        }
        if (roundText.text != gameManager.round.ToString()) {
            roundText.text = gameManager.round.ToString();
        }
    }
}