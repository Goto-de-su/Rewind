using UnityEngine;
using System.Collections.Generic;

public class Act
{
    [SerializeField] private CharactorRoleData role;
    private bool isAvailable = true;
    private EffectContext context;

    // プロパティ
    private CharactorRoleData Role => this.role;
    private bool IsAvailable => this.isAvailable;
    public List<Card> Cards { get; set; }

    public void RegisterAction(Card card)
    {
        Cards.Add(card);
    }

    public void TakeAction()
    {
        foreach (Card card in this.Cards)
        {
            this.UseCard(card);
        }
    }

    /// <summary>
    /// カードの効果を順に発動
    /// </summary>
    /// <param name="card"></param>
    private void UseCard(Card card)
    {
        foreach (StickerData sticker in card.CardData.Stickers)
        {
            sticker.UseSticker(context);
        }
    }
}
