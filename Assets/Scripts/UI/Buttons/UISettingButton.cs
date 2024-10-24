using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettingButton : UISelectableButton
{
    [SerializeField] private Setting setting;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI tvalueText;

    [SerializeField] private Image previousImage;
    [SerializeField] private Image nextImage;

    private void Start()
    {
        AplyProperty(setting);
    }

    public void SetNextValueSetting()
    {
        setting?.SetNextValue();
        setting?.Apply();
        UpdateInfo();
    }

    public void SetPreviousValueSetting() 
    {
        setting?.SetPreviousValue();
        setting?.Apply();
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        titleText.text = setting.Title;
        tvalueText.text = setting.GetStringValue();

        previousImage.enabled = !setting.isMinvalue;
        nextImage.enabled = !setting.isMaxvalue;
    }

    public void AplyProperty(Setting property)
    {
        if (property == null) return;

        setting = property;

        UpdateInfo();
    }
}

   
