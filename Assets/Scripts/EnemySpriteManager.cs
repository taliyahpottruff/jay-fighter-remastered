using UnityEngine.Networking;

/*
 * AUTHOR: Trenton Pottruff
 */

public abstract class EnemySpriteManager : NetworkBehaviour {
    public abstract void MoveUp();
    public abstract void MoveDown();
    public abstract void MoveLeft();
    public abstract void MoveRight();
    public abstract void HandleIdle();
    public abstract void SpecialAttack();
}