using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IfBeneath : Conditional {
    [SerializeField]
    public SharedGameObject opponent;
    public SharedGameObject AI;
    
    public override TaskStatus OnUpdate() {
        float opponentY = opponent.Value.GetComponent<Transform>().position.y;
        float AIY = AI.Value.GetComponent<Transform>().position.y;

        if(AIY - opponentY > 0) {
            // PlayerController AIController = AI.Value.GetComponent<PlayerController>();
            // if(AIController.movementInputDirection == 0) {
            //     AIController.movementInputDirection = -1;
            // } else {
            //     AIController.movementInputDirection *= -1;
            // }

            // if(AIController.wantMoveDirection == 0) {
            //     AIController.wantMoveDirection = -1;
            // } else {
            //     AIController.wantMoveDirection *= -1;
            // }

            return TaskStatus.Success;
        } else {
            return TaskStatus.Failure;
        }
    }
}
