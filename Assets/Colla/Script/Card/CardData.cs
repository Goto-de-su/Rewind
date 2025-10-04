using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    [SerializeField] private string cardName;
    [SerializeField] private Sprite cardPicture;
    [Min(0), SerializeField] private int cost;
    [SerializeField] private int priority;
    [Min(1), SerializeField] private int stickerMaxNum = 4;
    [SerializeField] private StickerData[] stickers = System.Array.Empty<StickerData>();

    // プロパティ
    public string CardName => this.cardName;
    public Sprite CardPicture => this.cardPicture;
    public int Cost => this.cost;
    public int Priority => this.priority;
    public int StickerMaxNum => this.stickerMaxNum;
    public IReadOnlyList<StickerData> Stickers => this.stickers;

    private void OnValidate() => ClampStickers();
    private void OnEnable() => ClampStickers();

    private void ClampStickers()
    {
        if (stickers == null) { stickers = System.Array.Empty<StickerData>(); }
        if (stickerMaxNum < 1) stickerMaxNum = 1;

        if (stickers.Length > stickerMaxNum)
        {
            System.Array.Resize(ref stickers, stickerMaxNum);
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}
