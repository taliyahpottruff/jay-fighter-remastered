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

    private Transform shooter0;
    private Transform shooter1;
    private bool shooter = false;

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

        fireVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;

        if (Input.GetMouseButton(0))
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
                    shooter0 = topRight.transform.FindChild("Right Shooter"); 
                    shooter1 = topRight.transform.FindChild("Right Shooter");
                } else {
                    //Left
                    topDown.SetActive(false);
                    topLeft.SetActive(true);
                    topRight.SetActive(false);
                    topUp.SetActive(false);
                    shooter0 = topLeft.transform.FindChild("Left Shooter");
                    shooter1 = topLeft.transform.FindChild("Left Shooter");
                }
            }
            else {
                if (fireVector.y > 0) {
                    //Up
                    topDown.SetActive(false);
                    topLeft.SetActive(false);
                    topRight.SetActive(false);
                    topUp.SetActive(true);
                    shooter0 = topUp.transform.FindChild("Left Shooter");
                    shooter1 = topUp.transform.FindChild("Right Shooter");
                }
                else {
                    //Down
                    topDown.SetActive(true);
                    topLeft.SetActive(false);
                    topRight.SetActive(false);
                    topUp.SetActive(false);
                    shooter0 = topDown.transform.Find("Left Shooter");
                    shooter1 = topDown.transform.Find("Right Shooter");
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
            if (firing && isLocalPlayer && !Game.PAUSED) {
                Vector2 direction = fireVector.normalized;

                Vector3 pos = Vector3.zero;

                if (shooter) {
                    pos = shooter0.position;
                    shooter = false;
                }
                else {
                    pos = shooter1.position;
                    shooter = true;
                }

                GameObject bulletObj = Instantiate(bulletPrefab, pos + (Vector3)direction, Quaternion.identity) as GameObject;
                Bullet bullet = bulletObj.GetComponent<Bullet>();
                bullet.SetVelocityOnAwake(rb.velocity + (direction * 10));
                bulletObj.GetComponent<Rigidbody2D>().velocity = rb.velocity + (direction * 10);
                aSource.PlayOneShot(gunSound);
            }
            yield return new WaitForSeconds(0.1f);
        } while (true);
    }
}
