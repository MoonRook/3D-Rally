using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip hover;

    private AudioSource audio;

    private UIButton[] uIButtons;
    
    private void Start()
    {
        audio = GetComponent<AudioSource>();

        uIButtons = GetComponentsInChildren<UIButton>(true);

        for (int i = 0; i < uIButtons.Length; i++)
        {
            uIButtons[i].PointerEnter += OnPointerEnter;
            uIButtons[i].PointerClick += OnPointerClicked;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < uIButtons.Length; i++)
        {
            uIButtons[i].PointerEnter -= OnPointerEnter;
            uIButtons[i].PointerClick -= OnPointerClicked;
        }
    }

    private void OnPointerEnter(UIButton arg0)
    {
        audio.PlayOneShot(hover);
    }

    private void OnPointerClicked(UIButton arg0)
    {
        audio.PlayOneShot(click);
    }
}
