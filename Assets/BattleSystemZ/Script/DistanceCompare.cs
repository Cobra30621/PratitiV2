using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System;

public enum CompareMode {
    BiggerThan,
    LessThan
};

public class DistanceCompare : Conditional {
    [SerializeField]
    public SharedGameObject opponent;
    public SharedGameObject AI;
    
    [SerializeField]
    public float distance;
    
    [SerializeField]
    public CompareMode mode;
    
    public override TaskStatus OnUpdate() {
        float opponentX = opponent.Value.GetComponent<Transform>().position.x;
        float AIX = AI.Value.GetComponent<Transform>().position.x;

        if(mode == CompareMode.BiggerThan) {
            if(Math.Abs(AIX - opponentX) > distance) {
                return TaskStatus.Success;
            } else {
                return TaskStatus.Failure;
            }
        } else {
            if(Math.Abs(AIX - opponentX) < distance) {
                return TaskStatus.Success;
            } else {
                return TaskStatus.Failure;
            }
        }
    }
}
