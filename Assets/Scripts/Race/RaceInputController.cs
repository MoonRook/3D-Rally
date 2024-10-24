using UnityEngine;

public class RaceInputController : MonoBehaviour, IDependency<CarInputControll>, IDependency<RaceStateTracker>
{
    private CarInputControll carControl;
    public void Construct(CarInputControll obj) => carControl = obj;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceFinished;

        carControl.enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceFinished;
    }

    private void OnRaceStarted()
    {
        carControl.enabled = true;
    }

    private void OnRaceFinished()
    {
        carControl.Stop();
        carControl.enabled = false;
    }
}
