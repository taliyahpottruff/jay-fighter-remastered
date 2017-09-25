using UnityEngine;
using UnityEngine.UI;

/*
 * AUTHOR: Trenton Pottruff
*/

[RequireComponent(typeof(AudioSource))]
public class ButtonSounds : MonoBehaviour {
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private AudioSource source { get{ return GetComponent<AudioSource>(); } }

    public void OnHover() {
        source.PlayOneShot(hoverSound);
    }
}