using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine.Networking;

/*
 * AUTHOR: Trenton Pottruff
 * 
 * CONTRIBUTOR: Garrett Nicholas
 * (Logic for the different types of enemies and balancing)
 */

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class CPU : NetworkBehaviour {
    #region Member Variables
    public float minSpeed = 1;
    public float maxSpeed = 3;
    public float speed = 3;
    public bool shooter = false; //Is this enemy a Shooter?
    public bool duplicator = false; //Is this enemy a Savage?
    public bool firing = false;
    public int ScoreOnDeath;
    public int minCoins;
    public int maxCoins;
    public GameObject FullHealthBar;
    public GameObject HealthBar;
    public EnemySpriteManager spriteManager;
    public AudioClip spawnSound;
    [SyncVar]
    public bool hideHealth;
    #endregion

    #region Private Variables
    private Rigidbody2D rb;
    private GameObject bulletPrefab;
    private GameObject basicEnemy;
    private Vector2 td; //Direction of the target player
    private Health health;
    private GameObject player;
    private bool melee = false;
    private RaycastHit2D meleeHit;
    private Timer HealthBarTimer = new Timer();
    private GameObject BronzeCoin;
    private GameObject SilverCoin;
    private GameObject GoldCoin;
    private Pathfinding pathfinder;
    private float previousHealth;
    private AudioSource aSource;
    private Vector2 playerPosition = Vector2.zero;
    #endregion

    #region Initialize
    private void Awake() {
        pathfinder = GameObject.FindGameObjectWithTag("Pathfinder").GetComponent<Pathfinding>();
    }

    private void Start() {
        //TODO Remove collsions from enemies beautifully
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Player"));
        //Set all required variables
        BronzeCoin = Resources.Load<GameObject>("Prefabs/BronzeCoin");
        SilverCoin = Resources.Load<GameObject>("Prefabs/SilverCoin");
        GoldCoin = Resources.Load<GameObject>("Prefabs/GoldCoin");
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        basicEnemy = Resources.Load<GameObject>("Prefabs/Enemies/DupeMinion");
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        speed = Random.Range(minSpeed, maxSpeed);
        player = GameObject.FindGameObjectWithTag("Player");
        FullHealthBar.SetActive(false);
        previousHealth = health.health;
        aSource = GetComponent<AudioSource>();

        //Play Spawn Sound, If Exists
        if (spawnSound != null) {
            aSource.PlayOneShot(spawnSound);
        }

        //Start appropriate co-routines
        StartCoroutine(FireBullet());
        StartCoroutine(MeleeAttack());
	}
    #endregion

    #region Update
    void Update() {
        HealthBar.transform.localScale = new Vector3((float)(health.health / 100), 0.9081425f, 0.908152f);

        player = GetClosestPlayer(); //Reference the closest player
        #region Health Hiding
        if (previousHealth != health.health) resetTimer(); //If there are any changes in the enemies health, reset the health bar's timer

        if (hideHealth) {
            FullHealthBar.SetActive(false);
            hideHealth = false;
        }

        previousHealth = health.health;
        #endregion
        #region Game Logic
        if (!Game.PAUSED && isServer) {
            playerPosition = this.transform.position;
            if (player != null) playerPosition = player.transform.position;

            List<Node> path = pathfinder.FindPath(this.transform.position, playerPosition);
            if (path != null && path.Count > 0) playerPosition = path[0].position; //If the pathfinder can find a path to the player, use the first node as a target for movement instead
            Vector2 targetDirection = (playerPosition - (Vector2)transform.position).normalized; //Set the target direction towards whatever target is set
            td = targetDirection;

            #region Direction Handling
            if (spriteManager != null) {
                if (!targetDirection.Equals(Vector2.zero)) {
                    Vector2 absVector = new Vector2(Mathf.Abs(targetDirection.x), Mathf.Abs(targetDirection.y));

                    //Handle sprite animations
                    #region Handle Horizontal
                    if (absVector.x > absVector.y) {
                        if (targetDirection.x > 0)
                            spriteManager.MoveRight();
                        else
                            spriteManager.MoveLeft();
                    }
                    #endregion
                    #region Handle Vertical
                    else {
                        if (targetDirection.y > 0)
                            spriteManager.MoveUp();
                        else
                            spriteManager.MoveDown();
                    }
                    #endregion
                }
            }
            #endregion

            rb.velocity = targetDirection * speed; //Move the enemy towards the appropriate target
            
            EnemyLogic();
        } else {
            rb.velocity = Vector2.zero;
        }
        #endregion
    }
    #endregion

    #region Player Chooser
    /// <summary>
    /// Finds the closest player to the enemy.
    /// </summary>
    /// <returns>The GameObject of the closest player</returns>
    public GameObject GetClosestPlayer() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        if (objs.Length > 0) {
            float shortestDistance = Vector2.Distance(this.transform.position, objs[0].transform.position);
            GameObject go = objs[0];

            for (int i = 1; i < objs.Length; i++) {
                float newDistance = Vector2.Distance(this.transform.position, objs[i].transform.position);

                if (newDistance < shortestDistance) go = objs[i];
            }

            return go;
        }

        return null;
    }
    #endregion

    #region Coin Dropping
    /// <summary>
    /// Drop coins based on the pre-set ranges.
    /// </summary>
    public void DropCoins() {
        int drop = Random.Range(minCoins, maxCoins+1);
        int goldCoins = drop / 10;
        int goldRemain = drop % 10;
        int silverCoins = goldRemain / 5;
        int bronzeCoins = goldRemain % 5;

        #region Spawn Gold Coins
        for (int i = 0; i < goldCoins; i++) {
            GameObject gold = Instantiate(GoldCoin, this.gameObject.transform.position, Quaternion.identity);
            Rigidbody2D rb = gold.GetComponent<Rigidbody2D>();
            Vector2 direction = Vector2.up;
            float angle = Random.Range(0f, 360f);
            direction = Quaternion.Euler(0, 0, angle) * direction;
            rb.AddForce(direction * 5, ForceMode2D.Impulse);
            NetworkServer.Spawn(gold);
        }
        #endregion
        #region Spawn Silver Coins
        for (int i = 0; i < silverCoins; i++) {
            GameObject silver = Instantiate(SilverCoin, this.gameObject.transform.position, Quaternion.identity);
            Rigidbody2D rb = silver.GetComponent<Rigidbody2D>();
            Vector2 direction = Vector2.up;
            float angle = Random.Range(0f, 360f);
            direction = Quaternion.Euler(0, 0, angle) * direction;
            rb.AddForce(direction * 5, ForceMode2D.Impulse);
            NetworkServer.Spawn(silver);
        }
        #endregion
        #region Spawn Bronze Coins
        for (int i = 0; i < bronzeCoins; i++) {
            GameObject bronze = Instantiate(BronzeCoin, this.gameObject.transform.position, Quaternion.identity);
            Rigidbody2D rb = bronze.GetComponent<Rigidbody2D>();
            Vector2 direction = Vector2.up;
            float angle = Random.Range(0f, 360f);
            direction = Quaternion.Euler(0, 0, angle) * direction;
            rb.AddForce(direction * 5, ForceMode2D.Impulse);
            NetworkServer.Spawn(bronze);
        }
        #endregion
    }
    #endregion

    #region Garrett Nicholas: Enemy Logic
    private void EnemyLogic() {
        float duperate = Mathf.Round(Random.Range(0f, 100f));
        float firerate = Mathf.Round(Random.Range(0f, 1.5f));
        if (shooter && (firerate == 0f)) {
            firing = true;
        } else firing = false;
        #region Savage Spawn Code
        if (duplicator && (duperate == 0f)) {
            spriteManager.SpecialAttack();
            StartCoroutine(SpawnMinions());
        }
        #endregion

        if (!shooter) {
            float distance = Vector2.Distance(playerPosition, this.transform.position);
            if (distance <= 2) {
                Vector2 playerDirection = (playerPosition - (Vector2)this.transform.position).normalized;
                meleeHit = Physics2D.Raycast(this.transform.position, playerDirection, 2f, 1 << LayerMask.NameToLayer("Player"));
                Debug.DrawRay(this.transform.position, playerDirection * 2, Color.red, 5000);
                melee = (bool) meleeHit;
            } else {
                melee = false;
            }            
        }
    }
    #endregion

    #region Co-Routines
    #region Bullet Firing
    private IEnumerator FireBullet() {
        do {
            if (firing) {
                Vector2 direction = td.normalized;
                GameObject bulletObj = Instantiate(bulletPrefab, this.transform.position + (Vector3)direction, Quaternion.identity) as GameObject;
                Bullet bullet = bulletObj.GetComponent<Bullet>();
                bullet.playerBullet = false;
                bullet.damage = 5;
                bulletObj.GetComponent<Rigidbody2D>().velocity = (rb.velocity + (direction * 10));
            }
            yield return new WaitForSeconds(0.1f);
        } while (true);
    }
    #endregion

    #region Melee Attack
    private IEnumerator MeleeAttack() {
        while (true) {
            if (melee) {
                Health playerHealth = meleeHit.collider.gameObject.GetComponent<Health>();
                playerHealth.DoDamage(1);    
            }
            yield return new WaitForSeconds(1);
        }
    }
    #endregion

    #region Minion Spawning
    private IEnumerator SpawnMinions() {
        yield return new WaitForSeconds(1f);
        GameObject go = Instantiate(basicEnemy, this.transform.position + new Vector3(0, 1), Quaternion.identity); //Spawn Minions
        NetworkServer.Spawn(go); //Ensure that the minions get spawned server side too
    }
    #endregion
    #endregion

    #region Garrett Nicholas: Health Bar
    public void resetTimer() {
        FullHealthBar.SetActive(true);
        HealthBarTimer.Stop();
        HealthBarTimer.Dispose();
        HealthBarTimer = new Timer(5000);
        HealthBarTimer.Elapsed += (sender, e) => HandleTimer();
        HealthBarTimer.Start();
    }
    public void disposeTimer() {
        HealthBarTimer.Stop();
        HealthBarTimer.Dispose();
    }
    private void HandleTimer() {
        hideHealth = true;
        HealthBarTimer.Stop();
    }
    #endregion
}
