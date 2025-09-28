using UnityEngine;
using System;

[Serializable]
public class Sticker
{
    [SerializeField] private StickerType type;
    [SerializeField] private int value;
    [SerializeField] private Sprite icon;

    public StickerType GetStickerType()
    {
        return this.type;
    }

    public int GetStickerValue()
    {
        return this.value;
    }

    /// <summary>
    /// 攻撃ステッカーの行動処理
    /// </summary>
    public void Attack(GameObject enemy)
    {
        enemy.GetComponent<Charactor>().SubstractHp(value);
    }

    /// <summary>
    /// 防御ステッカーの行動処理
    /// </summary>
    public void Defence(GameObject you)
    {
        you.GetComponent<Charactor>().AddDefence(value);
    }

    /// <summary>
    /// ×２ステッカーの行動処理
    /// </summary>
    public void Double(GameObject you)
    {
        you.GetComponent<Charactor>().RemoveAction();
        you.GetComponent<Charactor>().ChangeIndex();
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
    public void AfterOpponentAction(GameObject you)
    {
        you.GetComponent<Charactor>().HasBroken();
    }

    /// <summary>
    /// 回復ステッカーの行動処理
    /// </summary>
    public void Recover(GameObject you)
    {
        you.GetComponent<Charactor>().SubstractHp(value);
    }
}
