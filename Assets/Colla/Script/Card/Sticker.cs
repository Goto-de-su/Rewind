using UnityEngine;
using System;

[Serializable]
public class Sticker:MonoBehaviour
{
    [SerializeField] private StickerData stickerData;
    public StickerData StickerData;

    public void UseSticker(EffectContext context)
    {
        if(stickerData.Effect == null)
        {
            return;
        }

        if(stickerData.Effect is IStickerEffect effect)
        {
            effect.UseEffect(context, stickerData.Value);
        }
    }

    private IStickerEffect ConvertType2Effect()
    {
        switch (stickerData.Type)
        {
            case StickerType.Attack:
                return GetComponent<AttackStickerEffect>();
            default:
                return null;
        }
    }

    /// <summary>
    /// 攻撃ステッカーの行動処理
    /// </summary>
    public void Attack(GameObject enemy)
    {
        enemy.GetComponent<Charactor>().SubstractHp(stickerData.Value);
    }

    /// <summary>
    /// 防御ステッカーの行動処理
    /// </summary>
    public void Defence(GameObject you)
    {
        you.GetComponent<Charactor>().AddDefence(stickerData.Value);
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
        you.GetComponent<Charactor>().SetBroken();
    }

    /// <summary>
    /// 回復ステッカーの行動処理
    /// </summary>
    public void Recover(GameObject you)
    {
        you.GetComponent<Charactor>().SubstractHp(stickerData.Value);
    }
}
