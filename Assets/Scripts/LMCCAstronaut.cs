using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LMCCAstronaut : MonoBehaviour
{

    [SerializeField] private Transform leftHand, rightHand;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
    }

}