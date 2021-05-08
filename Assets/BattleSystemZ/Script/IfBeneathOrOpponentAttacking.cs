using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IfBeneathOrOpponentAttacking : Conditional {
    [SerializeField]
    public SharedGameObject opponent;
    public SharedGameObject AI;
    
    public override TaskStatus OnUpdate() {
        float opponentY = opponent.Value.GetComponent<Transform>().position.y;
        float AIY = AI.Value.GetComponent<Transform>().position.y;

        if(AIY - opponentY > 0 || opponent.Value.GetComponent<PlayerController>().isAttacking) {
            return TaskStatus.Success;
        } else {
            return TaskStatus.Failure;
        }
    }
}
