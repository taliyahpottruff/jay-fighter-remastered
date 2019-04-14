using UnityEngine;
using UnityEngine.UI;

//AUTHOR: Trenton Pottruff

[RequireComponent(typeof(Animator))]
public class MusicNotification : MonoBehaviour {
    public string song;
    public string artist;
    public Text songName;
    public Text artistName;

    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void Update() {
        songName.text = song;
        artistName.text = artist;
    }

    public void Open() {
        anim.Play("notification-Open");
    }
}