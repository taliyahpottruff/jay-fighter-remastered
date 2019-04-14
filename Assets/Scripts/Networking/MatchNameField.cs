using UnityEngine;
using UnityEngine.UI;

/*
 * AUTHOR: Trenton Pottruff
*/

public class MatchNameField : MonoBehaviour {
    private InputField field;

    private void Start() {
        field = GetComponent<InputField>();

        field.text = Game.STEAM.GetUsername() + "'s Room";
    }
}