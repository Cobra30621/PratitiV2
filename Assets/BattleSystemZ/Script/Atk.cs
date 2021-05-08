using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public enum AtkMode {
    Auto,
    RegularAttack,
    UpAttack,
    AirAttack,
};

public class Atk : Action {
    [SerializeField]
    public SharedGameObject opponent;
    public SharedGameObject AI;

    [SerializeField]
    public AtkMode mode;
    
    public override TaskStatus OnUpdate() {
        PlayerController AIController = AI.Value.GetComponent<PlayerController>();

        if(AIController.AI) {
            AIController.canMove = false;
            AIController.movementInputDirection = 0;
            
            float opponentY = opponent.Value.GetComponent<Transform>().position.y;
            float AIY = AI.Value.GetComponent<Transform>().position.y;

            switch (mode)
            {
                case AtkMode.Auto:
                    if(AIY > -1.5f) {
                        AIController.AirAttack();
                    } else if(opponentY - AIY > 0.75f) {
                        AIController.UpAttack();
                    } else {
                        AIController.RegularAttack();
                    }
                    break;
                case AtkMode.RegularAttack:
                    AIController.RegularAttack();
                    break;
                case AtkMode.UpAttack:
                    AIController.UpAttack();
                    break;
                case AtkMode.AirAttack:
                    if (AIY >= -1.5f) {
                        AIController.AirAttack();
                    } else {
                        return TaskStatus.Failure;
                    }
                    break;           
                default:
                    AIController.RegularAttack();
                    break;                  
            }
        }

        return TaskStatus.Success;
    }
}
