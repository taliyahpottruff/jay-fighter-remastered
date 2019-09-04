using UnityEngine;
using UnityEngine.Networking;

/*
 * AUTHOR: Trenton Pottruff
 */

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour {
    public string username = "Player";
    public ControlScheme currentScheme = ControlScheme.Keyboard;
    public float score;
    public int coins;

    private SpriteRenderer sr;
    private Health health;

    public void StartLocalPlayer() {
        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = Color.blue;

        //Set local camera to follow this player
        SmoothCamera sc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothCamera>();
        if (sc != null) sc.lookAt = this.transform; //Have the smooth camera target the player

        Toolbar t = GameObject.FindGameObjectWithTag("Toolbar").GetComponent<Toolbar>();
        t.inventory = GetComponent<Inventory>();

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Player"));
    }

    public void Start() {
		StartLocalPlayer();

        health = GetComponent<Health>();
    }

    private void Update() {
		//TODO: Is setting the username necessary?
		if (username.Equals("Player") && Game.STEAM != null) username = Game.STEAM.GetUsername();

        //Set the control scheme to whatever is being used
        if ((Input.anyKey && !Input.GetButton("Fire1")) || Input.GetMouseButton(0))
            currentScheme = ControlScheme.Keyboard;
        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            currentScheme = ControlScheme.Gamepad;
    }

    public void GiveHealth(int amount) {
        health.health += amount;
    }

    public void IncreaseMaxHealth(int amount) {
        health.IncreaseMax(amount);
    }

    public void SpawnItemProxy(string item) {
        Debug.Log("Spawning proxy");
        CmdSpawnItem(item);
    }

    public void CmdSpawnItem(string name) {
        Debug.Log("Spawning actual.");
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + name);
        Instantiate<GameObject>(prefab, this.transform.position, Quaternion.identity);
    }

    public PlayerMovement movemement
    {
        get {
            return GetComponent<PlayerMovement>();
        }
    }
}
