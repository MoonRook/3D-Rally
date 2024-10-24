using UnityEngine;
using TMPro;

public class CarSpeedIndicator : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private TextMeshProUGUI text;

    void Update()
    {
        text.text = car.LinearVelocity.ToString("F0");
    }
}
