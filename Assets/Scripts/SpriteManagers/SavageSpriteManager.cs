using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void PlayWalk() {
        //Play Walking Animation
        legAnimator.Play("savage-Walk");
    }
}