using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManeuveringDevice : MonoBehaviour
{
    public bool isRight = true;

    public GameObject projectile;
    public Transform startPos;

    private Rigidbody _rigi;
    private GameObject _pj;
    private XRIControl _inputControl;
    private GameObject firedProjectile;
    public Action onProjectileReturnsBack;

    public static float attractionForce = 40.0f;
    public static float force = 50f;
    public static float maxSpeed = 10.0f;
    public static float accelerationFactor = 1.0f;

    public bool isPull = false;
    private bool isFired = false;

    private void Start()
    {
        _inputControl = FindObjectOfType<XRIControl>();

        if (_inputControl == null)
            return;

        if(isRight)
            XRIControl.onTriggerLeftController += () => 
            OnTriggerController();
        else
            XRIControl.onTriggerRightController += () => 
            OnTriggerController();

        _rigi = _inputControl.gameObject.GetComponent<Rigidbody>();
    }

    private void OnTriggerController()
    {
        if(isFired)
            return;

        var rigi = Instantiate(projectile, startPos.position, Quaternion.identity).GetComponent<Rigidbody>();
        rigi.AddForce(transform.forward * force, ForceMode.Impulse);

        firedProjectile = rigi.gameObject;

        isFired = true;
    }

    private void Update()
    {
        isPull = IsPull();

        if (!isPull)
            return;


        if (_pj != null && _pj.GetComponent<ManeuveringDeviceProjectile>().collide)
        {
            transform.LookAt(_pj.transform);

            Vector3 directionToTarget = transform.position - _pj.transform.position;
            float distance = Vector3.Distance(_pj.transform.position, transform.position);
            _rigi.AddForce(-directionToTarget.normalized * Mathf.Clamp(distance, 0, 100), ForceMode.Acceleration);
        }
    }

    private bool IsPull()
    {
        if (isRight)
        {
            return isFired && _inputControl.grabLeftController == 1;
        }
        else
        {
            return isFired && _inputControl.grabRightController == 1;
        }
    }

    private void ReturnBackProjectile()
    {
        if(firedProjectile != null)
        {
            Destroy(firedProjectile);
            onProjectileReturnsBack?.Invoke();
        }
    }
}
