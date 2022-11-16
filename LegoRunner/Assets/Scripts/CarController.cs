using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    public bool isBreaking;
    public bool isAI;

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    public float maxSteeringAngle = 30f;
    public float motorForce = 50f;
    public float brakeForce = 0f;

    public MainController brain;
    public NPCAI npcAI;
	private void FixedUpdate()
    {
        HandleMotor();

		if (!isBreaking)
		{
            UpdateWheels();
        }
        
    }
    private void HandleMotor()
    {
		if (isAI)
		{
            frontLeftWheelCollider.motorTorque = npcAI.speed * motorForce;
            frontRightWheelCollider.motorTorque = npcAI.speed * motorForce;
        }
		else
		{
            frontLeftWheelCollider.motorTorque = brain.speed * motorForce;
            frontRightWheelCollider.motorTorque = brain.speed * motorForce;
        }
        

        brakeForce = isBreaking ? 3000f : 0f;
        frontLeftWheelCollider.brakeTorque = brakeForce;
        frontRightWheelCollider.brakeTorque = brakeForce;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;
    }

    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.eulerAngles = new Vector3(trans.rotation.x,trans.rotation.y,rot.eulerAngles.x);
        trans.position = pos;
    }

}