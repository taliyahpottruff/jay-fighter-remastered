using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

public class Utilities {
    //SOURCE: http://stackoverflow.com/questions/11743160/how-do-i-encode-and-decode-a-base64-string
    /// <summary>
    /// Converts plain text to a Base64 String
    /// </summary>
    /// <param name="plainText">The plain text to encode</param>
    /// <returns>The encoded string</returns>
    public static string Base64Encode(string plainText) {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    //SOURCE: http://stackoverflow.com/questions/11743160/how-do-i-encode-and-decode-a-base64-string
    /// <summary>
    /// Converts a Base64 String to plain text
    /// </summary>
    /// <param name="base64EncodedData">The Base64 string to decode</param>
    /// <returns>The decoded string</returns>
    public static string Base64Decode(string base64EncodedData) {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    /// <summary>
    /// Clears all children of a given Transform
    /// </summary>
    /// <param name="parent">The transform to clear children from</param>
    public static void ClearChildren(Transform parent) {
        for (int i = 0; i < parent.childCount; i++) {
            GameObject.Destroy(parent.GetChild(i).gameObject);
        }
    }
}