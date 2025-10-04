using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Card
{
    private Guid uuid;
    private int priority;
    [SerializeField] private CardData cardData;

    // プロパティ
    public Guid Uuid => this.uuid;
    public CardData CardData => this.cardData;
    public int Priority => this.priority;
    public Card(CardData cardData)
    {
        this.uuid = Guid.NewGuid();
        this.cardData = cardData;
    }
}
