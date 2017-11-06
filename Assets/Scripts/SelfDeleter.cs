using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDeleter : MonoBehaviour {
    public float lifespan;

    private void Start() {
        StartCoroutine(Kill());
    }

    private IEnumerator Kill() {
        yield return new WaitForSeconds(lifespan);
        Destroy(this.gameObject);
    }
}