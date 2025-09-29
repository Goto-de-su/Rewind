using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    private string cardName;
    private Sprite cardPicture;
    private int cost;
    private int stickerMaxNum;
    private List<Sticker> stickers;

    public string CardName => this.cardName;
    public Sprite CardPicture => this.cardPicture;
    public int Cost => this.cost;
    public int StickerMaxNum => this.stickerMaxNum;
    public List<Sticker> Stickers => this.stickers;


}
