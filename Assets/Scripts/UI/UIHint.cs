using TMPro;
using UnityEngine;

public class UIHint : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hintText;

    void Start()
    {
        // ���������� ���������� �����
        hintText.gameObject.SetActive(true);
    }

    void Update()
    {
        // ��������� ������� ������� Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // �������� �����
            hintText.gameObject.SetActive(false);
        }
    }
}