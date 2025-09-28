using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Card
{
    private Guid uuid;
    [SerializeField] private CardData data;

    public Card(CardData cardData)
    {
        this.uuid = Guid.NewGuid();
        this.data = cardData;
    }

    public List<Sticker> GetCardInfo()
    {
        return this.data.stickers;
    }

}
