using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Card
{
    private Guid uuid;
    [SerializeField] private CardData cardData;

    // プロパティ
    public Guid Uuid => this.uuid;
    public CardData CardData => this.cardData;
    public Card(CardData cardData)
    {
        this.uuid = Guid.NewGuid();
        this.cardData = cardData;
    }
}
