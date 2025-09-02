using UnityEngine;

public class PlayCardGA : GameAction
{
    public EnemyView ManualTarget { get; private set; }
    public Card Card { get;set; }
    public PlayCardGA(Card card)
    {
        Card = card;
        ManualTarget = null;
    }
    public PlayCardGA(Card card, EnemyView manualTarget) : this(card)
    {
        ManualTarget = manualTarget;
    }
}
