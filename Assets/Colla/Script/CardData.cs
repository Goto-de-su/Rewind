using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    public string cardName;
    public int cost;
    public int stickerMaxNum;
    public List<Sticker> stickers;
}
