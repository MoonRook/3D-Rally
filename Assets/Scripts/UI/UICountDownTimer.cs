using TMPro;
using UnityEngine;

public class UICountDownTimer : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private AudioClip countdownClip;  // ��� ����� ���� ��������� �������
    private AudioSource audioSource;                   // ��������� AudioSource ��� ��������������� �����

    private Timer countDownTimer;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // ��������� ��������� AudioSource
        audioSource.clip = countdownClip;                    // ������������� ����� ����

        raceStateTracker.PreparationStarted += OnPreparationStarted;
        raceStateTracker.Started += OnRaceStarted;
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;
        raceStateTracker.Started -= OnRaceStarted;
    }

    private void OnPreparationStarted()
    {
        text.enabled = true;
        enabled = true;
        audioSource.Play(); // �������� �������������� ����
    }

    private void OnRaceStarted()
    {
        text.enabled = false;
        enabled = false;
    }

    private void Update()
    {
        text.text = raceStateTracker.CountDownTimer.Value.ToString("F0");

        if (text.text == "0")
            text.text = "G0";
    }
}


