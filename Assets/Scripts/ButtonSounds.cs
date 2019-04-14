using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

[RequireComponent(typeof(AudioSource))]
public class ButtonSounds : MonoBehaviour {
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private AudioSource source { get{ return GetComponent<AudioSource>(); } }

    public void OnHover() {
        source.PlayOneShot(hoverSound); //Play the hover sound
    }
}