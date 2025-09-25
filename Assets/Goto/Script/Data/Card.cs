using UnityEngine;
using System.Collections.Generic;

public class Card : MonoBehaviour
{
    // カードID
    private Grid uid;

    [Header("プロパティ")]
    [Tooltip("カードの名前を入力してください。")]
    [SerializeField] private string cardName = "Template";
    [Tooltip("カードのコストを入力してください。")]
    [SerializeField] private int cost = 1;
    [Tooltip("カードに貼れるステッカーの最大枚数を入力してください。")]
    [SerializeField] private int stickerMaxNum = 4;
    [Tooltip("カードに貼られたステッカーを入力してください。")]
    [SerializeField] private List<Sticker> stickers;



}
