using UnityEngine;

public class Sticker:MonoBehaviour
{
    [SerializeField] private StickerType type;
    [SerializeField] private int value;
    [SerializeField] private Sprite icon;

    // 行動するオブジェクト
    [SerializeField] private GameObject me;
    // 行動により影響を受けるオブジェクト
    [SerializeField] private GameObject you;

    /// <summary>
    /// 攻撃ステッカーの行動処理
    /// </summary>
    public void Attack()
    {

    }

    /// <summary>
    /// 防御ステッカーの行動処理
    /// </summary>
    public void Defence()
    {

    }

    /// <summary>
    /// ×２ステッカーの行動処理
    /// </summary>
    public void Double()
    {

    }

    /// <summary>
    /// ドローステッカーの行動処理
    /// </summary>
    public void Draw()
    {

    }

    /// <summary>
    /// 攻撃受けステッカーの行動処理
    /// </summary>
    public void AfterOpponentAction()
    {

    }

    /// <summary>
    /// 回復ステッカーの行動処理
    /// </summary>
    public void Recover()
    {

    }
}

enum StickerType
{
    Attack,
    Defence,
    Double,
    Draw,
    AfterOpponentAction,
    Recover
}
