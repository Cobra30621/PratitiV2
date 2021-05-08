using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System;

public enum SpecialAtkMode {
    Auto,
    RegularSpecialAttack,
    UPSpecialAttack,
    FrontSpecialAttack,
    DownSpecialAttack
};

public class SpecialAtk : BehaviorDesigner.Runtime.Tasks.Action {
    [SerializeField]
    public SharedGameObject opponent;
    public SharedGameObject AI;

    [SerializeField]
    public SpecialAtkMode mode;
    
    public override TaskStatus OnUpdate() {
        PlayerController AIController = AI.Value.GetComponent<PlayerController>();

        if(AIController.AI && AI.Value.GetComponent<Transform>().position.y < -2.4f) {
            AIController.canMove = false;
            AIController.movementInputDirection = 0;
            switch (mode)
            {
                case SpecialAtkMode.Auto:
                    // float opponentY = opponent.Value.GetComponent<Transform>().position.y;
                    // float AIY = AI.Value.GetComponent<Transform>().position.y;

                    // if(opponentY - AIY > 2f) {
                    //     AIController.UPSpecialAttack();
                    // } else if(opponentY - AIY > 1.5f) {
                    //     AIController.FrontSpecialAttack();
                    // } else if(opponentY - AIY < 0.75f) {
                    //     AIController.DownSpecialAttack();
                    // } else {
                    //     AIController.RegularSpecialAttack();
                    // }

                    float angle = GetAngle(opponent.Value.GetComponent<Rigidbody2D>().position, AI.Value.GetComponent<Rigidbody2D>().position);
                    
                    // Debug.Log(angle);

                    if(angle <= 90f && angle > 70f) {
                        AIController.UPSpecialAttack();
                    } else if(angle <= 70f && angle > 40f) {
                        AIController.RegularSpecialAttack();
                    } else if(angle <= 40f && angle > 15f) {
                        AIController.FrontSpecialAttack();
                    } else if(angle <= 15f && angle > -5f) {
                        AIController.DownSpecialAttack();
                    } else {
                        return TaskStatus.Failure;
                    }

                    break;
                case SpecialAtkMode.RegularSpecialAttack:
                    AIController.RegularSpecialAttack();
                    break;
                case SpecialAtkMode.UPSpecialAttack:
                    AIController.UPSpecialAttack();
                    break;
                case SpecialAtkMode.FrontSpecialAttack:
                    AIController.FrontSpecialAttack();
                    break;   
                case SpecialAtkMode.DownSpecialAttack:
                    AIController.DownSpecialAttack();
                    break;                  
                default:
                    AIController.RegularSpecialAttack();
                    break;                  
            }
        } else {
            return TaskStatus.Failure;
        }

        return TaskStatus.Success;
    }

    public float ToDegrees(float radians) => radians * 180f / Mathf.PI;

    public float GetAngle(Vector2 point, Vector2 center) {
        Vector2 relPoint = point - center;
        return ToDegrees(Mathf.Atan2(relPoint.y, relPoint.x));
    }
}
