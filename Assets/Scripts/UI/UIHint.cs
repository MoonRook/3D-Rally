using TMPro;
using UnityEngine;

public class UIHint : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hintText;

    void Start()
    {
        // Изначально показываем текст
        hintText.gameObject.SetActive(true);
    }

    void Update()
    {
        // Проверяем нажатие клавиши Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Скрываем текст
            hintText.gameObject.SetActive(false);
        }
    }
}