using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    public event UnityAction<string> GearChanged;

    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxBrakeTorque;

    [Header("UnityEngine")]
    [SerializeField] private AnimationCurve engineTorqueCurve;
    // debug
    [SerializeField] private float engineMaxTorque;
    [SerializeField] private float engineTorque;
    
    [SerializeField] private float engineRpm;
    [SerializeField] private float engineMinRpm;
    [SerializeField] private float engineMaxRpm;

    [Header("GearBox")]
    [SerializeField] private float[] gears;
    [SerializeField] private float finalDriveRatio;
    // debug
    [SerializeField] private int selectedGearIndex;
    // debug
    [SerializeField] private float selectedGear;
    [SerializeField] private float rearGear;
    [SerializeField] private float upShiftEngineRpm;
    [SerializeField] private float downShiftEngineRpm;

    [SerializeField] private int maxSpeed;
    public float LinearVelocity => chassis.LinearVelocity;
    public float NormalizeLinearVelocity => chassis.LinearVelocity / maxSpeed;
    public float WheelSpeed => chassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;
    // ToDo —сылка на ручной тормоз

    public float EngineRpm => engineRpm;
    public float EngineMaxRpm => engineMaxRpm;

    private CarChassis chassis;
    public Rigidbody Rigidbody => chassis == null ? GetComponent<CarChassis>().Rigidbody: chassis.Rigidbody;

    // debug
    [SerializeField] public float linearVelocity;
    public float TrottleControl;
    public float SteerControl;
    public float BrakeControl;

    private void Start()
    {
        chassis = GetComponent<CarChassis>();
    }
    private void Update()
    {
        linearVelocity = LinearVelocity;

        UpdateEngineTorque();
        AutoGearShift();

        if (LinearVelocity >= maxSpeed)
            engineTorque = 0;

        chassis.MotorTorque = engineTorque * TrottleControl;
        chassis.SteerAngle = maxSteerAngle * SteerControl;
        chassis.BrakeTorque = maxBrakeTorque * BrakeControl;
    }


    // Gearbox

    public string GetSelectedGearName()
    {
        if (selectedGear == rearGear) return "R";

        if (selectedGear == 0) return "N";

        return (selectedGearIndex + 1).ToString();
    }

    private void AutoGearShift()
    {
        if (selectedGear < 0) return;

        if (engineRpm >= upShiftEngineRpm)
            UpGear();

        if (engineRpm < downShiftEngineRpm)
            DownGear();

        selectedGearIndex = Mathf.Clamp(selectedGearIndex, 0, gears.Length - 1);
    }

    public void UpGear()
    {
        ShiftGaer(selectedGearIndex + 1);
    }

    public void DownGear()
    {
        ShiftGaer(selectedGearIndex - 1);
    }

    public void ShiftToReverseGear()
    {
        selectedGear = rearGear;
        GearChanged?.Invoke(GetSelectedGearName());
    }

    public void ShiftToFirstGear()
    {
        ShiftGaer(0);
    }

    public void ShiftToNetlal()
    {
        selectedGear = 0;
        GearChanged?.Invoke(GetSelectedGearName());
    }
    private void ShiftGaer(int gearIndex)
    {

        gearIndex = Mathf.Clamp(gearIndex, 0, gears.Length - 1);
        selectedGear = gears[gearIndex];
        selectedGearIndex = gearIndex;

        GearChanged?.Invoke(GetSelectedGearName());
    }

    private void UpdateEngineTorque()
    {
        engineRpm = engineMinRpm + Mathf.Abs(chassis.GetAverageRpm() * selectedGear * finalDriveRatio);
        engineRpm = Mathf.Clamp(engineRpm, engineMinRpm, engineMaxRpm);

        engineTorque = engineTorqueCurve.Evaluate(engineRpm / engineMaxRpm) * engineMaxTorque
            * finalDriveRatio * Mathf.Sign(selectedGear) * gears[0];
    }

    public void Reset()
    {
        chassis.Reset();

        chassis.MotorTorque = 0;
        chassis.BrakeTorque = 0;
        chassis.SteerAngle = 0;

        TrottleControl = 0;
        SteerControl = 0;
        BrakeControl = 0;
    }

    public void Respawn(Vector3 position, Quaternion rotation)
    {
        Reset();
        
        transform.position = position;
        transform.rotation = rotation;
    }
}