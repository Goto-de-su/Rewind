using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] private int baseHp;
    [SerializeField] private int totalHp;
    [SerializeField] private Sticker head;
    [SerializeField] private Sticker rightHand;
    [SerializeField] private Sticker leftHand;
    [SerializeField] private List<int> attack;
    [SerializeField] private int defence;
    // エフェクトのリストも必要

    // 以下の内容は、ロジックに書くべきな気も...
    [SerializeField] GameObject enemy;

    public void Attack()
    {

    }
}
