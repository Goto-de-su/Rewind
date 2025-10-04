using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DeckPresetData", menuName = "Scriptable Objects/DeckPresetData")]
public class DeckPresetData : ScriptableObject
{
    [SerializeField] private List<CardData> deckPreset;

    public List<CardData> DeckPreset => deckPreset;

    private void OnValidate() => ClampStickers();
    private void OnEnable() => ClampStickers();

    private void ClampStickers()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
