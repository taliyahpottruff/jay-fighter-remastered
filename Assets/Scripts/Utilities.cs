using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Utilities {
    //SOURCE: http://stackoverflow.com/questions/11743160/how-do-i-encode-and-decode-a-base64-string
    public static string Base64Encode(string plainText) {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    //SOURCE: http://stackoverflow.com/questions/11743160/how-do-i-encode-and-decode-a-base64-string
    public static string Base64Decode(string base64EncodedData) {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}