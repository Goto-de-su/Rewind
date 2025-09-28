using UnityEngine;
using System.Collections.Generic;

public class Charactor : MonoBehaviour
{
    [SerializeField] private string charactorName;
    [SerializeField] private int hp;
    [SerializeField] private int defence;
    private List<Card> cardList;
    private  List<Sticker> actionList;
    private int actionIndex;
    private bool hasBroken;

    // 攻撃するオブジェクト
    [SerializeField] private GameObject enemy;

    private void Awake()
    {
        cardList = new List<Card>();
        actionList = new List<Sticker>();
    }

    public void SetCard(Card addCard)
    {
        cardList.Add(addCard);
        List<Sticker> stickerDatas = addCard.GetCardInfo().stickers;
        foreach (Sticker stickerdata in stickerDatas)
        {
            actionList.Add(stickerdata);
        }
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    /// <param name="value"></param>
    public void Attack(int value)
    {
        this.enemy.GetComponent<Charactor>().SubstractHp(value);
    }

    /// <summary>
    /// 回復
    /// </summary>
    /// <param name="value"></param>
    public void AddHp(int value)
    {
        this.hp += value;
    }

    public void SubstractHp(int value)
    {
        this.hp -= value;
    }

    /// <summary>
    /// 防御
    /// </summary>
    /// <param name="value"></param>
    public void AddDefence(int value)
    {
        this.defence += value;
    }

    /// <summary>
    /// ×2
    /// </summary>
    /// <param name="value"></param>
    public void DoubleAction(int value)
    {
        this.RemoveAction();
        this.ChangeIndex();
    }

    /// <summary>
    /// 敵の行動後
    /// </summary>
    public void RestrictAfterEnemyAction()
    {

    }

    public void HasBroken()
    {
        this.hasBroken = true;
    }

    // 行動インデックスを変更
    public void ChangeIndex()
    {
        this.actionIndex = 0;
    }

    // 行った行動を消去
    public void RemoveAction()
    {
        this.actionList.RemoveAt(this.actionIndex);
    }

    private void Sticker2Action(Sticker sticker)
    {
        StickerType type = sticker.GetStickerType();
        int value = sticker.GetStickerValue();

        switch (type)
        {
            case StickerType.Attack:
                Attack(value);
                break;
            case StickerType.Defence:
                AddDefence(value);
                break;
            case StickerType.Double:
                DoubleAction(value);
                break;
            case StickerType.Draw:
                // ドロー処理をつくったら挿入
                break;
            case StickerType.AfterEnemyAction:
                // 思いつかなかった
                RestrictAfterEnemyAction();
                break;
            case StickerType.Recover:
                AddHp(value);
                break;
            default:
                break;
        }
    }

    public void Action()
    {
        actionIndex = 0;
        hasBroken = false;

        while (hasBroken == false)
        {
            Sticker2Action(actionList[actionIndex]);

            if (actionIndex < actionList.Count)
            {
                hasBroken = true;
            }

            actionIndex++;
        }
    }
}
