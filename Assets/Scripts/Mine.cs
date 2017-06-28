using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Mine : MonoBehaviour {
    [SerializeField]
    private GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Entity")) {
            Instantiate<GameObject>(explosionPrefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}