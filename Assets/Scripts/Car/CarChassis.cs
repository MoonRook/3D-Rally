using System;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent (typeof(Rigidbody))]
public class CarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxle[] wheelAxles;
    [SerializeField] private float wheelBaseLength;

    [SerializeField] private Transform centerOfMass;

    [Header("DownForce")]
    [SerializeField] private float DownForceMin;
    [SerializeField] private float DownForceMax;
    [SerializeField] private float DownForceFactor;

    [Header("AngularDrag")]
    [SerializeField] private float AngularDragMin;
    [SerializeField] private float AngularDragMax;
    [SerializeField] private float AngularDragFactor;

    // Debug
    public float MotorTorque;
    public float SteerAngle;
    public float BrakeTorque;

    public float LinearVelocity => rigidbody.velocity.magnitude * 3.6f;

    private new Rigidbody rigidbody;
    public Rigidbody Rigidbody => rigidbody == null ? GetComponent<Rigidbody>(): rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        if (centerOfMass != null)
            rigidbody.centerOfMass = centerOfMass.localPosition;
        
        for (int i = 0; i < wheelAxles.Length; i++)
        {
            wheelAxles[i].ConfigureVehicleSubsteps(50, 50, 50);
        }
    }

    private void Update()
    {
        UpdateAngularDrad();
        UpdateDownForce();
        UpdateWheelAxles();
    }

    public float GetAverageRpm()
    {
        float sum = 0;

        for (int i = 0; i < wheelAxles.Length; i++)
        {
            sum += wheelAxles[i].GetAvarageRpm();
        }

        return sum / wheelAxles.Length;
    }

    public float GetWheelSpeed()
    {
        return GetAverageRpm() * wheelAxles[0].GetRadius() * 2 * 0.1885f;
    }

    private void UpdateAngularDrad()
    {
        rigidbody.angularDrag = Mathf.Clamp(AngularDragFactor *
             LinearVelocity, AngularDragMin, AngularDragMax);
    }
    private void UpdateDownForce()
    {
        float downForce = Mathf.Clamp(DownForceFactor * LinearVelocity, DownForceMin, DownForceMax);
        rigidbody.AddForce(-transform.up * downForce);
    }

    private void UpdateWheelAxles() // добавить функционал для ручного тормоза
    {
        int amountMotorWheel = 0;

        for (int i = 0; i < wheelAxles.Length; i++)
        {
            if (wheelAxles[i].IsMotor == true)
                amountMotorWheel += 2;
        }

        for (int i = 0; i < wheelAxles.Length; i++)
        {
            wheelAxles[i].Update();

            wheelAxles[i].ApplyMotorTorque(MotorTorque);
            wheelAxles[i].ApplySteerAngle(SteerAngle, wheelBaseLength);
            wheelAxles[i].ApplyBreakTorque(BrakeTorque);
        }
    }

    public void Reset()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
 


