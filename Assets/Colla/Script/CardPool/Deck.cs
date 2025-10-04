using UnityEngine;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    private List<Card> deckCards;
    [SerializeField] private DeckPresetData deckPreset;
    [SerializeField] private CardContext context;

    public List<Card> DeckCards => this.deckCards;

    private void Start()
    {
        this.deckCards.Clear();
        CreateDeckCards();
        ShuffleDeck();
    }

    /// <summary>
    /// 戦闘開始時のデッキ生成
    /// </summary>
    private void CreateDeckCards()
    {
        foreach (CardData deckPreset in this.deckPreset.DeckPreset)
        {
            Card newCard = new Card(deckPreset);
            this.AddDeckCards(newCard);
        }
    }
    
    /// <summary>
    /// デッキにカードを追加(戦闘内のみ)
    /// </summary>
    /// <param name="card"></param>
    public void AddDeckCards(Card card)
    {
        this.deckCards.Add(card);
    }

    /// <summary>
    /// ドロー時
    /// </summary>
    public Card Pop()
    {
        if (deckCards.Count == 0)
        {
            this.BringCardsByGarbage();
        }

        Card drawCard = this.deckCards[0];
        deckCards.RemoveAt(0);

        return drawCard;
    }

    /// <summary>
    /// 捨て札をデッキへ
    /// </summary>
    private void BringCardsByGarbage()
    {
        this.deckCards = this.context.Garbage.ReleaseGarbage();
        this.ShuffleDeck();

    }


    /// <summary>
    /// シャッフル
    /// </summary>
    public void ShuffleDeck()
    {
        CardUtils.Shuffle(this.deckCards);
    }
}
