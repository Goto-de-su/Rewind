using UnityEngine;

[CreateAssetMenu(fileName = "StickerData", menuName = "Scriptable Objects/StickerData")]
public class StickerData : ScriptableObject
{
    [SerializeField] private StickerType type;
    [SerializeField] private int value;
    [SerializeField] private Sprite icon;
    [SerializeField] private ScriptableObject effect;

    // プロパティ
    public StickerType Type => type;
    public int Value => value;
    public Sprite Icon => icon;

    public ScriptableObject Effect => effect;

    public void UseSticker(EffectContext context)
    {
        if (this.effect == null)
        {
            return;
        }

        if (this.effect is IStickerEffect effect)
        {
            effect.UseEffect(context, this.value);
        }
    }

    ///// <summary>
    ///// 攻撃ステッカーの行動処理
    ///// </summary>
    //public void Attack(GameObject enemy)
    //{
    //    enemy.GetComponent<Charactor>().SubstractHp(this.value);
    //}

    ///// <summary>
    ///// 防御ステッカーの行動処理
    ///// </summary>
    //public void Defence(GameObject you)
    //{
    //    you.GetComponent<Charactor>().AddDefence(this.value);
    //}

    ///// <summary>
    ///// ×２ステッカーの行動処理
    ///// </summary>
    //public void Double(GameObject you)
    //{
    //    you.GetComponent<Charactor>().RemoveAction();
    //    you.GetComponent<Charactor>().ChangeIndex();
    //}

    ///// <summary>
    ///// ドローステッカーの行動処理
    ///// </summary>
    //public void Draw()
    //{

    //}

    ///// <summary>
    ///// 攻撃受けステッカーの行動処理
    ///// </summary>
    //public void AfterOpponentAction(GameObject you)
    //{
    //    you.GetComponent<Charactor>().SetBroken();
    //}

    ///// <summary>
    ///// 回復ステッカーの行動処理
    ///// </summary>
    //public void Recover(GameObject you)
    //{
    //    you.GetComponent<Charactor>().SubstractHp(this.value);
    //}

    private void OnValidate() => ClampStickers();
    private void OnEnable() => ClampStickers();

    private void ClampStickers()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
