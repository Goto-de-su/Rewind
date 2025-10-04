using UnityEngine;
using System.Collections.Generic;

public class Garbage : MonoBehaviour
{
    private List<Card> garbageCards;
    public List<Card> GarbageCards => garbageCards;

    public List<Card> ReleaseGarbage()
    {
        List<Card> releaseCards = this.garbageCards;
        releaseCards.Clear();
        return releaseCards;
    }

    public void GetGarbage(Card card)
    {
        garbageCards.Add(card);
    }
}