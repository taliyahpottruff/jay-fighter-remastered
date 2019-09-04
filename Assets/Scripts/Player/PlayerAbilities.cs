using System.Collections.Generic;
using UnityEngine;

/*
/* AUTHOR: Trenton Pottruff
*/

[RequireComponent(typeof(Player))]
public class PlayerAbilities : MonoBehaviour {
    [SerializeField]
    private List<string> abilities = new List<string>();

    private Player player;

    private void Start() {
        player = GetComponent<Player>();
    }

    /// <summary>
    /// Adds an ability to the player's ability list.
    /// </summary>
    /// <param name="ability">The ability to add</param>
    /// <returns>Returns whether or not the ability was added</returns>
    public bool AddAbility(string ability) {
        if (!abilities.Contains(ability)) {
            abilities.Add(ability);
            return true;
        }

        Debug.Log("Player already has ability \"" + ability + "\"");
        return false;
    }

    /// <summary>
    /// Removes an ability.
    /// </summary>
    /// <param name="ability">The ability to remove.</param>
    public void RemoveAbility(string ability) {
        abilities.Remove(ability);
    }

    private void Update() {
        #region Abilities
        if (abilities.Contains("Magnetism")) {
			//Find all coins in range and attract them to the player
            RaycastHit2D[] hits = Physics2D.CircleCastAll(this.transform.position, 4f, Vector2.up);
            for (int i = 0; i < hits.Length; i++) {
                if (hits[i].collider.tag.Equals("Coin")) {
                    Rigidbody2D dropRB = hits[i].rigidbody;
                    Vector2 targetDirection = (player.transform.position - dropRB.transform.position).normalized;
                    float distance = Vector2.Distance(player.transform.position, dropRB.transform.position);
                    dropRB.velocity = targetDirection * Mathf.Clamp(5 - distance*2, 0, 5f);
                }
            }
        }
        #endregion
    }
}