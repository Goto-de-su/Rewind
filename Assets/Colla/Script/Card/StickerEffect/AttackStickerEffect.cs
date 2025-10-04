using UnityEngine;

public class AttackStickerEffect:MonoBehaviour, IStickerEffect
{
    public void UseEffect(EffectContext context, int value)
    {
        Charactor enemy = context.enemy.GetComponent<Charactor>();
        if (enemy == null)
        {
            Debug.Log("エネミーオブジェクトにキャラクターがついていません。");
        }
        enemy.SubstractHp(value);
    }

}
