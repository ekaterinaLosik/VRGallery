using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof (Animator))]
/**
* @class Hand
*
* @brief Animate hand
*
* It's a class that animates a hand model */
public class Hand : MonoBehaviour
{
    public float speed;

    Animator animator;

    private float gripTarget;

    private float triggerTarget;

    private float gripCurrent;

    private float triggerCurrent;

    /// The function is called when the script is first run. It gets the Animator component from
    /// the GameObject that the script is attached to
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// The function is called every frame. It checks if the hand is open or closed, and
    /// if it is open, it checks if the hand is moving. If the hand is moving, it plays the animation
    /// for the hand moving. If the hand is not moving, it plays the animation for the hand being open.
    /// If the hand is closed, it plays the animation for the hand being closed
    void Update()
    {
        AnimateHand();
    }

    /// If the current grip value is not equal to the target grip value, then move the current grip
    /// value towards the target grip value by a certain amount each frame
    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat("Grip", gripCurrent);
        }
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
            animator.SetFloat("Trigger", triggerCurrent);
        }
    }

    /// This function sets the gripTarget variable to the value passed in
    /// 
    /// @param v The value to set the grip to.
    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    /// This function sets the triggerTarget variable to the value passed in
    /// 
    /// @param v The value to set the trigger to.
    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }
}
