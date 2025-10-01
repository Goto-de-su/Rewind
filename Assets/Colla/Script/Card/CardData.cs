using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    [SerializeField] private string cardName;
    [SerializeField] private Sprite cardPicture;
    [Min(0), SerializeField] private int cost;
    [Min(1), SerializeField] private int stickerMaxNum = 4;
    [SerializeField] private Sticker[] stickers = System.Array.Empty<Sticker>();

    // プロパティ
    public string CardName => this.cardName;
    public Sprite CardPicture => this.cardPicture;
    public int Cost => this.cost;
    public int StickerMaxNum => this.stickerMaxNum;
    public IReadOnlyList<Sticker> Stickers => this.stickers;


    private void OnValidate() => ClampStickers();
    private void OnEnable() => ClampStickers();

    private void ClampStickers()
    {
        if (stickers == null) { stickers = System.Array.Empty<Sticker>(); }
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
