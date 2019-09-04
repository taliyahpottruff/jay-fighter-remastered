using UnityEngine;
using UnityEngine.UI;

/*
 * AUTHOR: Trenton Pottruff
 */

[RequireComponent(typeof(RawImage))]
[System.Obsolete("MovieTexture has been deprecated. Was not deprecated at the time of development.")]
public class AutoplayMovie : MonoBehaviour {
    private void Start() {
        MovieTexture m = (MovieTexture)GetComponent<RawImage>().texture;
        m.loop = true;
        m.Play();
    }
}