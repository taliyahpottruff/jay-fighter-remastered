using System.Collections;
using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

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