using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Player_Agent : Agent
{
    public override OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
    }
}
