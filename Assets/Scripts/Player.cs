using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour {
    public uint id = 0;
    public string username = "Player";
    public ControlScheme currentScheme = ControlScheme.Keyboard;
    public float score;
    public int coins;
    public bool isLocalPlayer = true;

    private SpriteRenderer sr;
    private Health health;

    public static Player singleton;

    private void OnStartLocalPlayer() {
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
        health = GetComponent<Health>();

        if (isLocalPlayer) {
            singleton = this;

            int newGP = PlayerPrefs.GetInt("gamesPlayed") + 1;
            PlayerPrefs.SetInt("gamesPlayed", newGP);
            PlayerPrefs.Save();

            OnStartLocalPlayer();
        }
    }

    private void Update() {
        if (isLocalPlayer && username.Equals("Player") && Game.STEAM != null) username = Game.STEAM.GetUsername();

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
        GameObject go = Instantiate<GameObject>(prefab, this.transform.position, Quaternion.identity);
    }

    public PlayerMovement movemement
    {
        get {
            return GetComponent<PlayerMovement>();
        }
    }
}
