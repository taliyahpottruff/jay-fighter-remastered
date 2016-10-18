using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    private int health = 100;
    private int maxHeath = 100;

    public int GetHealth() {
        return health;    
    }
}