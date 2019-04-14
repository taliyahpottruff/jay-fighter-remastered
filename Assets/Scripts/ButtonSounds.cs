using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

[RequireComponent(typeof(AudioSource))]
public class ButtonSounds : MonoBehaviour {
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private AudioSource src { get{ return GetComponent<AudioSource>(); } }

    public void OnHover() {
        src.PlayOneShot(hoverSound); //Play the hover sound
    }
}