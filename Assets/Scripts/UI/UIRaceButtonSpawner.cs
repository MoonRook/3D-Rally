using UnityEngine;

public class UIRaceButtonSpawner : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private UIRaceButton prefab;
    [SerializeField] private RaceInfo[] properties;

    [ContextMenu(nameof(Spawn))]
    public void Spawn()
    {
        if (Application.isPlaying == true) return;

        GameObject[] allObject = new GameObject[parent.childCount];

        for (int i = 0; i < parent.childCount; i++)
        {
            allObject[i] = parent.GetChild(i).gameObject;
        }

        for (int i = 0; i < allObject.Length; i++)
        {
            DestroyImmediate(allObject[i]);
        }

        for (int i = 0; i < properties.Length; i++)
        {
            UIRaceButton button = Instantiate(prefab, parent);
            button.AplyProperty(properties[i]);
        }
    }
}
