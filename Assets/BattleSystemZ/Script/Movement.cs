using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System;

public enum MovementMode {
    Chase,
    Dash,
    RunAway,
    Jump,
    Freeze,
    Idle
};

public class Movement : BehaviorDesigner.Runtime.Tasks.Action {
    [SerializeField]
    public SharedGameObject opponent;
    public SharedGameObject AI;

    [SerializeField]
    public MovementMode mode;
    
    public override TaskStatus OnUpdate() {
        float opponentX = opponent.Value.GetComponent<Transform>().position.x;
        float AIX = AI.Value.GetComponent<Transform>().position.x;

        PlayerController AIController = AI.Value.GetComponent<PlayerController>();

        if(AIController.AI) {
            if(mode == MovementMode.Idle) {
                AIController.Idle();
            } else if(mode == MovementMode.Freeze) {
                AIController.Freeze();
            } else if(mode == MovementMode.Jump) {
                AIController.RegularJump();
            } else if(mode == MovementMode.RunAway) {
                if(AIX > 7.5) {
                    AIController.LeftMove();
                    AIController.wantMoveDirection = -1;
                } else if(AIX < -7.5) {
                    AIController.RightMove();
                    AIController.wantMoveDirection = 1;
                } else {
                    if(AIX - opponentX < 0) {
                        AIController.LeftMove();
                        AIController.wantMoveDirection = -1;
                    } else {
                        AIController.RightMove();
                        AIController.wantMoveDirection = 1;
                    }
                }

                AIController.AIDash();
            } else {
                if(Math.Abs(AIX - opponentX) < 0.5) {
                    AIController.Idle();
                } else if(AIX - opponentX > 0) {
                    AIController.LeftMove();
                    AIController.wantMoveDirection = -1;
                } else {
                    AIController.RightMove();
                    AIController.wantMoveDirection = 1;
                }

                // 移動時有觸發Dash則Dash
                if(mode == MovementMode.Dash) {
                    AIController.AIDash();
                }
            }
        }
        
        return TaskStatus.Success;
    }
}
