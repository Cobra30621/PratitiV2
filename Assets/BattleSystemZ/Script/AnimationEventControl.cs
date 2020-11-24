using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventControl : MonoBehaviour
{
    [SerializeField]
    PlayerController playerConScipt;
    Rigidbody2D rig2D;
    private void StartAttacking()
    {
        playerConScipt.isAttacking = true;
        
    }
    private void StopAttacking()
    {
        playerConScipt.isAttacking = false;
        playerConScipt.isHitting = false;
        playerConScipt.canMove = true;
    }
    private void StartHitting()
    {
        playerConScipt.isHitting = true;
    }
    private void StopHitting()
    {
        playerConScipt.isHitting = false;
    }
}
