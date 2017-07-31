using UnityEngine;
using UnityEngine.Networking;

// @NOTE the attached sprite's position should be "top left" or the children will not align properly
// Strech out the image as you need in the sprite render, the following script will auto-correct it when rendered in the game
[RequireComponent(typeof(SpriteRenderer))]

// Generates a nice set of repeated sprites inside a streched sprite renderer
// @NOTE Vertical only, you can easily expand this to horizontal with a little tweaking
public class RepeatSpriteBoundary : NetworkBehaviour {
    SpriteRenderer sprite;

    void Start() {
        // Get the current sprite with an unscaled size
        sprite = GetComponent<SpriteRenderer>();
        Vector2 spriteSize = new Vector2(sprite.bounds.size.x / transform.localScale.x, sprite.bounds.size.y / transform.localScale.y);

        // Generate a child prefab of the sprite renderer
        GameObject childPrefab = new GameObject();
        SpriteRenderer childSprite = childPrefab.AddComponent<SpriteRenderer>();
        childPrefab.transform.position = transform.position;
        childSprite.sprite = sprite.sprite;
        childSprite.sortingOrder = sprite.sortingOrder;

        // Loop through and spit out repeated tiles
        GameObject child;
        for (int i = 0; i < (int)Mathf.Round(sprite.bounds.size.x); i++) {
            for (int j = 0; j < (int)Mathf.Round(sprite.bounds.size.y); j++) {
                child = Instantiate(Resources.Load<GameObject>("Prefabs/MapSprites/" + this.gameObject.name)) as GameObject;
                child.transform.position = transform.position - (new Vector3(0, spriteSize.y, 0) * j) + (new Vector3(spriteSize.x, 0, 0) * i) - new Vector3(0.5f, -0.5f, 0);
                child.transform.parent = transform;
                NetworkServer.Spawn(child);
            }
        }

        // Set the parent last on the prefab to prevent transform displacement
        childPrefab.transform.parent = transform;
        Destroy(childPrefab);

        // Disable the currently existing sprite component since its now a repeated image
        sprite.enabled = false;
    }
}