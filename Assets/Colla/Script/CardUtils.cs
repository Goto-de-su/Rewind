using UnityEngine;
using System.Collections.Generic;

public class CardUtils:MonoBehaviour
{
    /// <summary>
    /// Fisher–Yates アルゴリズムで List<Card> をシャッフル
    /// </summary>
    public static void Shuffle(List<Card> cards)
    {
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1); // UnityEngine.Random
            Card temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }
    }
}
