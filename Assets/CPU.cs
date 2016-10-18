using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
public class CPU : MonoBehaviour {
    public float speed = 3;

    private Rigidbody2D rb;

	private void Start() {
        rb = GetComponent<Rigidbody2D>();

        speed = Random.Range(2f, 4.5f);
	}
	
	void Update() {
        Vector2 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 targetDirection = (playerPosition - (Vector2) transform.position).normalized;
        rb.velocity = targetDirection * speed;
	}
}
