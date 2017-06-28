using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Collider2D))]
public class Mine : NetworkBehaviour {
    [SerializeField]
    private GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Entity") && isServer) {
            GameObject go = Instantiate<GameObject>(explosionPrefab, this.transform.position, Quaternion.identity);
            NetworkServer.Spawn(go);
            Destroy(this.gameObject);
        }
    }
}