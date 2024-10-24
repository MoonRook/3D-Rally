using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class AudioMixerSetting : Setting
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string nameParametry;
    
    [SerializeField] private float minRealValue;
    [SerializeField] private float maxRealValue;
    
    [SerializeField] private float virtualStep;
    [SerializeField] private float minVirtualValue;
    [SerializeField] private float maxvirtualValue;

    private float currentValue = 0;

    public override bool isMinvalue { get => currentValue == minRealValue; }
    public override bool isMaxvalue { get => currentValue == maxRealValue; }

    public override void SetNextValue()
    {
        AddValue(Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
    }

    public override void SetPreviousValue()
    {
        AddValue(-Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
    }

    public override string GetStringValue()
    {
        return Mathf.Lerp(minVirtualValue, maxvirtualValue, (currentValue - minRealValue) /
            (maxRealValue - minRealValue)).ToString();
    }
    public override object GetValue()
    {
        return currentValue;
    }

    private void AddValue(float value)
    {
        currentValue += value;
        currentValue = Mathf.Clamp(currentValue, minRealValue, maxRealValue);
    }
    
    public override void Apply()
    {
        audioMixer.SetFloat(nameParametry, currentValue);

        Save();
    }

    public override void Load()
    {
        currentValue = PlayerPrefs.GetFloat(title, 0);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(title, currentValue);
    }

}
