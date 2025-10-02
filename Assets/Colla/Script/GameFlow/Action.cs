using UnityEngine;
using System.Collections.Generic;

public class Action
{
    [SerializeField] private CharactorRole role;
    private bool isAvailable = true;
    private List<Card> cards;

    // プロパティ
    private CharactorRole Role => this.role;
    private bool IsAvailable => this.isAvailable;
    public Card Card { get; set; }

    public void TakeAction()
    {
        foreach (Card card in this.cards)
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
        foreach (Sticker sticker in card.CardData.Stickers)
        {
            sticker.UseSticker(CharactorController.player, CharactorController.enemy);
        }
    }
}
