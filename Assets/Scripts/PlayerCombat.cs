using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/*
 * AUTHOR: Trenton Pottruff
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

    private Player player;

    private void Start() {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");

        rb = GetComponent<Rigidbody2D>();
        aSource = GetComponent<AudioSource>();
        player = GetComponent<Player>();

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

        if (player.currentScheme == ControlScheme.Keyboard)
            fireVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        else
            fireVector = new Vector2(Input.GetAxis("FireHorizontal"), Input.GetAxis("FireVertical"));

        if (Input.GetMouseButton(0) || Input.GetButton("Fire1"))
            firing = true;
        else
            firing = false;

        float horizontal = Mathf.Abs(fireVector.x);
        float vertical = Mathf.Abs(fireVector.y);

        //Set directional data
        if (horizontal != 0 || vertical != 0) {
            if (horizontal > vertical) {
                if (fireVector.x > 0) {
                    //Right
                    topDown.SetActive(false);
                    topLeft.SetActive(false);
                    topRight.SetActive(true);
                    topUp.SetActive(false);
                    shooter0 = topRight.transform.Find("Right Shooter"); 
                    shooter1 = topRight.transform.Find("Right Shooter");
                } else {
                    //Left
                    topDown.SetActive(false);
                    topLeft.SetActive(true);
                    topRight.SetActive(false);
                    topUp.SetActive(false);
                    shooter0 = topLeft.transform.Find("Left Shooter");
                    shooter1 = topLeft.transform.Find("Left Shooter");
                }
            }
            else {
                if (fireVector.y > 0) {
                    //Up
                    topDown.SetActive(false);
                    topLeft.SetActive(false);
                    topRight.SetActive(false);
                    topUp.SetActive(true);
                    shooter0 = topUp.transform.Find("Left Shooter");
                    shooter1 = topUp.transform.Find("Right Shooter");
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
                
                CmdFire(pos, direction, GetComponent<NetworkIdentity>());
                aSource.PlayOneShot(gunSound);
            }
            yield return new WaitForSeconds(0.1f);
        } while (true);
    }

    [Command]
    private void CmdFire(Vector2 position, Vector2 direction, NetworkIdentity _owner) {
        GameObject bulletObj = Instantiate(bulletPrefab, position, Quaternion.FromToRotation(Vector2.up, direction)) as GameObject;
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.playerBullet = true;
        bullet.owner = _owner;
        
        bulletObj.GetComponent<Rigidbody2D>().velocity = (direction * 10);
        NetworkServer.Spawn(bulletObj);
        bulletObj.GetComponent<Rigidbody2D>().velocity = (direction * 10);
    }
}
