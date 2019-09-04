using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

//Class name was mispelled and I never changed it.
public class SingplayerGame : MonoBehaviour {
    public GameMap map;

    private void Start() {
        map.StartMap();
    }
}