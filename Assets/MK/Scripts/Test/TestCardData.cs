using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Card Data", fileName = "CardData")]
public sealed class TestCardData : ScriptableObject
{
    [SerializeField] private Sprite art;
    [Min(0), SerializeField] private int cost;
    [Min(0), SerializeField] private int maxStickerNum = 3;

    // 内部は配列で保持。公開は IReadOnlyList で読み取り専用に見せる
    [SerializeField] private Sticker[] stickers = System.Array.Empty<Sticker>();

    public Sprite Art => art;
    public int Cost => cost;
    public int MaxStickerNum => maxStickerNum;
    public IReadOnlyList<Sticker> Stickers => stickers; // 外部からAdd/Remove不可

    private void OnValidate() => ClampStickers();
    private void OnEnable()   => ClampStickers(); // ビルドでも保証

    private void ClampStickers()
    {
        if (stickers == null) { stickers = System.Array.Empty<Sticker>(); }
        if (maxStickerNum < 0) maxStickerNum = 0;

        if (stickers.Length > maxStickerNum)
        {
            System.Array.Resize(ref stickers, maxStickerNum);
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}