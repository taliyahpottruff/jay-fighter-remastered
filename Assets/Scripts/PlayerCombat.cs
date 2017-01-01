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
        playerPositon = (Vector2)this.transform.position;

        fireVector = new Vector2(Input.GetAxis("FireHorizontal"), Input.GetAxis("FireVertical"));

        if (fireVector != Vector2.zero)
            firing = true;
        else
            firing = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Health health = other.GetComponent<Health>();
        CPU cpu = other.GetComponent<CPU>();
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
