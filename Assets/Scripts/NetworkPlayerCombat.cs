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
public class NetworkPlayerCombat : PlayerCombat {
    private Vector2 fireVector = Vector2.zero;
    private Rigidbody2D rb;

    private GameObject bulletPrefab;
    private Vector2 playerPositon = Vector2.zero;

    private void Start() {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");

        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(FireBullet());
    }

    private void Update() {
        if (!isLocalPlayer)
            return;

        playerPositon = (Vector2)this.transform.position;

        fireVector = new Vector2(Input.GetAxis("FireHorizontal"), Input.GetAxis("FireVertical"));

        if (fireVector != Vector2.zero)
            firing = true;
        else
            firing = false;
    }

    public override IEnumerator FireBullet() {
        do {
            if (firing) {
                CmdFire(playerPositon, fireVector.normalized);
            }
            yield return new WaitForSeconds(0.1f);
        } while (true);
    }

    [Command]
    private void CmdFire(Vector2 position, Vector2 direction) {
        GameObject bulletObj = Instantiate(bulletPrefab, position + direction, Quaternion.identity) as GameObject;
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.SetVelocityOnAwake(rb.velocity + (direction * 10));
        bulletObj.GetComponent<Rigidbody2D>().velocity = rb.velocity + (direction * 10);
        NetworkServer.Spawn(bulletObj);
    }
}
