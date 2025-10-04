using UnityEngine;

[CreateAssetMenu(fileName = "AttackStickerEffect", menuName = "Scriptable Objects/AttackStickerEffect")]
public class AttackStickerEffect : ScriptableObject, IStickerEffect
{
    public void UseEffect(EffectContext context, int value)
    {
        Charactor enemy = context.enemy.GetComponent<Charactor>();
        if (enemy == null)
        {
            Debug.Log("エネミーオブジェクトにキャラクタークラスがついていません。");
        }
        enemy.SubstractHp(value);
        Debug.Log("攻撃しました。");
    }

    private void OnValidate() => ClampStickers();
    private void OnEnable() => ClampStickers();

    private void ClampStickers()
    {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
