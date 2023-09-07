using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Player_Agent : Agent
{
    private BoxMover boxMover;
    private Rigidbody2D body;
    private RayPerceptionSensorComponent2D rayPerceptionSensorComponent2D;

    public override void Initialize()
    {
        rayPerceptionSensorComponent2D = GetComponent<RayPerceptionSensorComponent2D>();
        body = GetComponent<Rigidbody2D>();
        boxMover = GetComponent<BoxMover>();
    }

    public override void OnEpisodeBegin()
    {
        transform.position = new Vector3(0, 0);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(boxMover.canJump);
        sensor.AddObservation(boxMover.isTouchingGround());
        //just need y velocity since we are moving horizontally at a constant velocity
        sensor.AddObservation(body.velocity.y);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        AddReward(0.1f);
        if(actions.DiscreteActions[0] == 1){
            if(boxMover.isTouchingGround()){
                boxMover.Jump();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            AddReward(-1f);
            EndEpisode();
        }
    }
}
