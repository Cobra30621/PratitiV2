using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventControl : MonoBehaviour
{
    [SerializeField]
    PlayerController playerConScript;
    [SerializeField]
    Rigidbody2D rig2D;
    private void StartAttacking()
    {
        playerConScript.isAttacking = true;
        
    }
    private void StopAttacking()
    {
        playerConScript.isAttacking = false;
        playerConScript.isHitting = false;
        playerConScript.canMove = true;
        rig2D.gravityScale = 2.5f;
    }
    private void StartHitting()
    {
        playerConScript.isHitting = true;
    }
    private void StopHitting()
    {
        playerConScript.isHitting = false;
        rig2D.gravityScale = 2.5f;
    }
}
