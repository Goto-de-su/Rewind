using UnityEngine;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    private List<Card> deck;

    public void Debug()
    {
        CardUtils.Shuffle(deck);
    }
}
