using UnityEngine;

public class CardContext
{
    [SerializeField] private Deck deck;
    [SerializeField] private Hand hands;
    [SerializeField] private Garbage garbage;
    [SerializeField] private Act act;

    public Deck Deck => this.deck;
    public Hand Hands => this.hands;
    public Garbage Garbage => this.garbage;
    public Act Act => this.act;
}
