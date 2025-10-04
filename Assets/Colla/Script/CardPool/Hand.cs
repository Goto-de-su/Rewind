using UnityEngine;
using System.Collections.Generic;

public class Hand
{
    [SerializeField] private List<Card> hands;
    [SerializeField] private CardContext context;
    /// <summary>
    /// 手札の最大枚数
    /// </summary>
    [SerializeField] private int maxCardsNum = 7;
    /// <summary>
    /// ドローターンで配られるカードの枚数
    /// </summary>
    [SerializeField] private int baseCardsNum = 5;

    /// <summary>
    /// カードの使用
    /// </summary>
    public void UseCard(int index)
    {
        Card usedCard = hands[index];
        // 行動リストにカードを登録
        this.context.Act.RegisterAction(usedCard);
        // 選択したカードをトラッシュ
        this.context.Garbage.GetGarbage(usedCard);
    }

    /// <summary>
    /// カードを捨て札に送る処理
    /// </summary>
    public void Trash(int num)
    {
        int indexer = 0;
        bool canTrash = this.CanTrash();

        while (indexer < num && canTrash)
        {
            context.Garbage.GetGarbage(this.hands[0]);
            indexer++;
            canTrash = this.CanTrash();
        }
    }

    /// <summary>
    /// カードが捨てられるか
    /// </summary>
    /// <returns></returns>
    private bool CanTrash()
    {
        if (this.hands.Count <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// カードのドロー処理
    /// </summary>
    /// <param name="num"></param>
    public void DrawCard(int num)
    {
        int indexer = 0;
        bool canDraw= this.CanDraw();

        while (indexer < num && canDraw)
        {
            hands.Add(this.context.Deck.Pop());
            indexer++;
            canDraw = this.CanDraw();
        }

        if(canDraw == false)
        {
            Debug.Log("カード枚数上限のため、ドロー出来ませんでした。");
        }
    }

    /// <summary>
    /// ドロー直前の枚数確認
    /// </summary>
    /// <returns></returns>
    private bool CanDraw()
    {
        if (this.hands.Count >= this.maxCardsNum)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 手札の全入れ替え
    /// </summary>
    public void Reflesh()
    {
        this.Trash(this.hands.Count);
        this.DrawCard(this.baseCardsNum);
    }
}
