using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
    * 
    * CONTRIBUTOR: Garrett Nicholas
    * (added the checks for the player getting hurt by coliding with an enemy)
*/

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class PlayerCombat : NetworkBehaviour {
    public bool firing = false;

    public AudioClip gunSound;
    public AudioClip coinpickup;

    public GameObject topDown;
    public GameObject topLeft;
    public GameObject topRight;
    public GameObject topUp;

    private Vector2 fireVector = Vector2.zero;
    private Rigidbody2D rb;
    private AudioSource aSource;

    private GameObject bulletPrefab;
    private Vector2 playerPositon = Vector2.zero;

    private void Start() {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");

        rb = GetComponent<Rigidbody2D>();
        aSource = GetComponent<AudioSource>();

        StartCoroutine(FireBullet());
    }

    private void Update() {
        if (!Game.PAUSED)
            DoUpdate();
    }

    public void DoUpdate() {
        //Set sfx audio volume
        aSource.volume = Game.SFX_VOLUME;

        playerPositon = (Vector2)this.transform.position;

        fireVector = new Vector2(Input.GetAxis("FireHorizontal"), Input.GetAxis("FireVertical"));

        if (fireVector != Vector2.zero)
            firing = true;
        else
            firing = false;

        float horizontal = Mathf.Abs(fireVector.x);
        float vertical = Mathf.Abs(fireVector.y);

        if (horizontal != 0 || vertical != 0) {
            if (horizontal > vertical) {
                if (fireVector.x > 0) {
                    //Right
                    topDown.SetActive(false);
                    topLeft.SetActive(false);
                    topRight.SetActive(true);
                    topUp.SetActive(false);
                } else {
                    //Left
                    topDown.SetActive(false);
                    topLeft.SetActive(true);
                    topRight.SetActive(false);
                    topUp.SetActive(false);
                }
            }
            else {
                if (fireVector.y > 0) {
                    //Up
                    topDown.SetActive(false);
                    topLeft.SetActive(false);
                    topRight.SetActive(false);
                    topUp.SetActive(true);
                }
                else {
                    //Down
                    topDown.SetActive(true);
                    topLeft.SetActive(false);
                    topRight.SetActive(false);
                    topUp.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Health health = other.GetComponent<Health>();
        CoinScript Coin = other.GetComponent<CoinScript>();
        CPU cpu = other.GetComponent<CPU>();
        if(Coin != null) {
            aSource.PlayOneShot(coinpickup);
        }
        if(cpu != null) {
            this.gameObject.GetComponent<Health>().DoDamage(5);
        }
    }
    public virtual IEnumerator FireBullet() {
        do {
            if (firing) {
                Vector2 direction = fireVector.normalized;
                GameObject bulletObj = Instantiate(bulletPrefab, this.transform.position + (Vector3)direction, Quaternion.identity) as GameObject;
                Bullet bullet = bulletObj.GetComponent<Bullet>();
                bullet.SetVelocityOnAwake(rb.velocity + (direction * 10));
                bulletObj.GetComponent<Rigidbody2D>().velocity = rb.velocity + (direction * 10);
                aSource.PlayOneShot(gunSound);
            }
            yield return new WaitForSeconds(0.1f);
        } while (true);
    }
}
