using UnityEngine;

/// <summary>
/// Author: Taliyah Pottruff
/// </summary>

[RequireComponent(typeof(Collider2D))]
public class Mine : MonoBehaviour {
    [SerializeField]
    private GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Entity") /*&& isServer*/) { //Explode if the colliding object is an entity
            GameObject go = Instantiate<GameObject>(explosionPrefab, this.transform.position, Quaternion.identity);
            //NetworkServer.Spawn(go);
            Destroy(this.gameObject);
        }
    }
}