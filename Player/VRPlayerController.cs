using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerController : MonoBehaviour
{
    private XRIControl _inputControl;
    private Rigidbody _rigi;

    //Movement fields
    private float maxRayDistance = 1.3f;
    private float walkSpeed = 5f;
    private float maxVelocityChange = 20f;
    private float jumpPower = 1f;
    private float _yaw = 0;
    private float rotationSensitivity = 2f;

    private bool isGrounded = false;

    private void Start()
    {
        _inputControl = GetComponent<XRIControl>();
        _rigi = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Rotate();
        Move();
    }

    private void Move()
    {
        //Add force to move
        Vector3 targetVelocity = new Vector3(_inputControl.leftControllerDirection.x, 0, _inputControl.leftControllerDirection.y);
        _rigi.AddForce(VelocityChange(targetVelocity, walkSpeed), ForceMode.VelocityChange);

        //Jump
        if (_inputControl.rightControllerDirection.y == 1f && isGrounded) 
            _rigi.AddForce(0f, jumpPower, 0f, ForceMode.Impulse);

        //Ground checking
        isGrounded = Physics.Raycast(transform.position, Vector3.down, maxRayDistance);
    }

    private Vector3 VelocityChange(Vector3 targetVelocity, float speed)
    {
        targetVelocity = transform.TransformDirection(targetVelocity) * speed;

        Vector3 velocity = _rigi.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        return velocityChange;
    }

    private void Rotate()
    {
        _yaw = transform.localEulerAngles.y + _inputControl.rightControllerDirection.x * rotationSensitivity;
        transform.localEulerAngles = new Vector3(0, _yaw, 0);
    }
}
