using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Login : MonoBehaviour {
    public void LoginTo() {
        StartCoroutine(TestRequest());
    }

	IEnumerator TestRequest() {
        WWWForm data = new WWWForm();
        data.AddField("username", "TrentonPottruff");
        data.AddField("password", "Tlosiitfa815!");
        UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/methods/loginToken", data);
        yield return www.Send();

        if (www.isError) {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }
}