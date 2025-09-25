using UnityEngine;
using UnityEngine.UI;

public class Sticker : MonoBehaviour
{
    [Header("プロパティ")]
    [Tooltip("ステッカーのアイコンを入力してください。")]
    [SerializeField] private Image icon;
    [Tooltip("ステッカーのタイプを入力してください。")]
    [SerializeField] private StickerType type;
    [Tooltip("ステッカーの値を入力してください。")]
    [SerializeField] private int value;
    [Tooltip("ステッカーの追加コストを入力してください。")]
    [SerializeField] private int addCost;

    /// <summary>
    /// 外部からステッカーの値を変更します。
    /// </summary>
    /// <param name="inputValue">変更後のステッカーの値</param>
    public void ChangeValue(int inputValue)
    {
        value = inputValue;
    }

    /// <summary>
    /// 外部からステッカーの追加コストを変更します。
    /// </summary>
    /// <param name="inputValue">変更後のステッカーの追加コスト</param>
    public void ChangeAddCost(int inputAddCost)
    {
        addCost = inputAddCost;
    }

}

enum StickerType
{
    attack,  // 攻撃
    defence, // 防御
    again    // ステッカー効果2倍
}
