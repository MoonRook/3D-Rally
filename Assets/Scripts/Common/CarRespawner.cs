using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRespawner : MonoBehaviour, IDependency<RaceStateTracker>, 
    IDependency<Car>, IDependency<CarInputControll>
{
    [SerializeField] private float respawHeight;
    
    private TrackPoint respawnTrackpoint;
    
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private Car car;
    public void Construct(Car obj) => car = obj;

    private CarInputControll carControl;
    public void Construct(CarInputControll obj) => carControl = obj;

    private void Start()
    {
        raceStateTracker.TrackPointPassed += OnTrackPointPassed;
    }

    private void OnDestroy()
    {
        raceStateTracker.TrackPointPassed -= OnTrackPointPassed;
    }

    private void OnTrackPointPassed(TrackPoint point)
    {
        respawnTrackpoint = point;
    }

    
    // авто встает поперек трассы???
    public void Respawn()
    {
        if (respawnTrackpoint == null) return;

        if (raceStateTracker.State != RaceState.Race) return;

        car.Respawn(respawnTrackpoint.transform.position + respawnTrackpoint.transform.up * respawHeight,
            respawnTrackpoint.transform.rotation);

        carControl.Reset();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) == true)
        {
            Respawn();
        }
    }
}
