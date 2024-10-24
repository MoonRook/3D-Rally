using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EngineSound : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private float pitchModifier;
    [SerializeField] private float volumeModifire;
    [SerializeField] private float RpmModifire;

    [SerializeField] private float basePitch = 1.0f;
    [SerializeField] private float baseVolume = 0.4f;


    private AudioSource engineAudioSourse;

    private void Start()
    {
        engineAudioSourse = GetComponent<AudioSource>();
    }

    private void Update()
    {
        engineAudioSourse.pitch = basePitch + pitchModifier * ((car.EngineRpm / car.EngineMaxRpm) 
            * RpmModifire);
        engineAudioSourse.volume = baseVolume + volumeModifire * (car.EngineRpm / car.EngineMaxRpm);
    }
}
