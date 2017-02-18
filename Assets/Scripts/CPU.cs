using UnityEngine;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
    * 
    * CONTRIBUTOR: Garrett Nicholas
    * (Logic for the different types of enemies and balancing)
*/

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
public class CPU : MonoBehaviour {
    public float minSpeed = 1;
    public float maxSpeed = 3;
    public float speed = 3;
    public bool shooter = false;
    public bool duplicator = false;
    public bool firing = false;
    public int ScoreOnDeath;
    private Rigidbody2D rb;
    private GameObject bulletPrefab;
    private GameObject basicEnemy;
    public GameObject FullHealthBar;
    public GameObject HealthBar;
    private Vector2 td;
    private Health health;

	private void Start() {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        basicEnemy = Resources.Load<GameObject>("Prefabs/Enemies/DupeMinion");
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        StartCoroutine(FireBullet());
        speed = Random.Range(minSpeed, maxSpeed);
	}
	
	void Update() {
        if (!Game.PAUSED) {
            Vector2 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector2 targetDirection = (playerPosition - (Vector2)transform.position).normalized;
            td = targetDirection;
            rb.velocity = targetDirection * speed;
            HealthBar.transform.localScale = new Vector3((float)(health.health / 100), 0.9081425f, 0.908152f);
            enemyLogic();
        } else {
            rb.velocity = Vector2.zero;
        }
    }
    private void enemyLogic() {
        float duperate = Mathf.Round(Random.Range(0f, 100f));
        float firerate = Mathf.Round(Random.Range(0f, 1.5f));
        if (shooter && (firerate == 0f)) {
            firing = true;
        } else firing = false;
        if(duplicator && (duperate == 0f)) {
            Instantiate(basicEnemy, this.transform.position + new Vector3(0, 1), Quaternion.identity);
        }
    }

    private IEnumerator FireBullet() {
        do {
            if (firing) {
                Vector2 direction = td.normalized;
                GameObject bulletObj = Instantiate(bulletPrefab, this.transform.position + (Vector3)direction, Quaternion.identity) as GameObject;
                Bullet bullet = bulletObj.GetComponent<Bullet>();
                bullet.damage = 5;
                bullet.SetVelocityOnAwake(rb.velocity + (direction * 10));
            }
            yield return new WaitForSeconds(0.1f);
        } while (true);
    }
}
