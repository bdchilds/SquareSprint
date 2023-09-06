using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

private BoxMover boxMover;

public class Player_Agent : Agent
{
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(boxMover.canJump);
    }
    
    public override void OnActionReceived(ActionBuffers actions)
    {
        if(actions.DiscreteActions[0] == 1){
            if(boxMover.canJump){
                boxMover.Jump();
            }
        }
    }
}
