using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsernameForm : MonoBehaviour {
    public InputField usernameField;

    public void SetUsername() {
        string username = usernameField.text;
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.Save();
    }
}