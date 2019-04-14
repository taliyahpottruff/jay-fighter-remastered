using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
*/

[System.Obsolete("Implements a class that uses old Unity networking")]
public class DroneSpriteManager : EnemySpriteManager {
    public GameObject[] wheels;
    public GameObject[] faces;

    public override void MoveDown() {
        //Set Wheels
        wheels[0].SetActive(true);
        wheels[1].SetActive(false);
        //Set Face
        faces[0].SetActive(false);
        faces[1].SetActive(true);
        faces[2].SetActive(false);
        faces[3].SetActive(false);
    }

    public override void MoveLeft() {
        //Set Wheels
        wheels[0].SetActive(false);
        wheels[1].SetActive(true);
        //Set Face
        faces[0].SetActive(false);
        faces[1].SetActive(false);
        faces[2].SetActive(true);
        faces[3].SetActive(false);
    }

    public override void MoveRight() {
        //Set Wheels
        wheels[0].SetActive(false);
        wheels[1].SetActive(true);
        //Set Face
        faces[0].SetActive(false);
        faces[1].SetActive(false);
        faces[2].SetActive(false);
        faces[3].SetActive(true);
    }

    public override void MoveUp() {
        //Set Wheels
        wheels[0].SetActive(true);
        wheels[1].SetActive(false);
        //Set Face
        faces[0].SetActive(true);
        faces[1].SetActive(false);
        faces[2].SetActive(false);
        faces[3].SetActive(false);
    }

    public override void HandleIdle() {
        //Do Nothing
    }

    public override void SpecialAttack() {
        //Do Nothing
    }
}