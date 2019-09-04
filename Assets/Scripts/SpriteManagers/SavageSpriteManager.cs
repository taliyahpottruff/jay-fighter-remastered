using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
*/

[System.Obsolete("Implements a class that uses old Unity networking")]
public class SavageSpriteManager : EnemySpriteManager {
    public Animator legAnimator;
    public Animator topAnimator;

    public override void MoveDown() {
        PlayWalk();
    }

    public override void MoveLeft() {
        PlayWalk();
    }

    public override void MoveRight() {
        PlayWalk();
    }

    public override void MoveUp() {
        PlayWalk();
    }

    public override void HandleIdle() {
        //Play Idle Animation
        legAnimator.Play("savage-Idle");
    }

    public override void SpecialAttack() {
        topAnimator.Play("protectors-Attack");
    }

    private void PlayWalk() {
        //Play Walking Animation
        legAnimator.Play("savage-Walk");
    }
}