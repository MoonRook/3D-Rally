using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIRaceButton : UISelectableButton
{
    [SerializeField] private RaceInfo raceInfo;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;

    private void Start()
    {
        AplyProperty(raceInfo);
    }
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        
        if (raceInfo == null) return;

        SceneManager.LoadScene(raceInfo.SceneName);
    }

    public void AplyProperty(RaceInfo property)
    {
        if (property == null) return;

        raceInfo = property;

        icon.sprite = raceInfo.Icon;
        title.text = raceInfo.Title;
    }
}
