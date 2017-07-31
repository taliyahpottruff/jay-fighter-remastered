using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class AutoplayMovie : MonoBehaviour {
    private void Start() {
        MovieTexture m = (MovieTexture)GetComponent<RawImage>().texture;
        m.loop = true;
        m.Play();
    }
}