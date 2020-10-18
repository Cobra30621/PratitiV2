using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventControl : MonoBehaviour
{
    [SerializeField]
    PlayerController playerConScipt;
    private void StartAttacking()
    {
        playerConScipt.isAttacking = true;
    }
    private void StopAttacking()
    {
        playerConScipt.isAttacking = false;
        playerConScipt.canMove = true;
    }
}
