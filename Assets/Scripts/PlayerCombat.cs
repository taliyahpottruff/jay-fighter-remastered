using UnityEngine;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
*/

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCombat : MonoBehaviour {
    public bool firing = false;

    private Vector2 fireVector = Vector2.zero;
    private Rigidbody2D rb;

    private GameObject bulletPrefab;

    private void Start() {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");

        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(FireBullet());
    }

    private void Update() {
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
    private IEnumerator FireBullet() {
        do {
            if (firing) {
                Vector2 direction = fireVector.normalized;
                GameObject bulletObj = Instantiate(bulletPrefab, this.transform.position + (Vector3)direction, Quaternion.identity) as GameObject;
                Bullet bullet = bulletObj.GetComponent<Bullet>();
                bullet.SetVelocityOnAwake(rb.velocity + (direction * 10));
            }
            yield return new WaitForSeconds(0.1f);
        } while (true);
    }
}
