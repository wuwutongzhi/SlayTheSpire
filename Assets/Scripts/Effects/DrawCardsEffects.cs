using System.Collections.Generic;
using UnityEngine;

public class DrawCardsEffects : Effect
{
    [SerializeField] private int drawAmount;
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        DrawCardsGA drawCardsGA = new DrawCardsGA(drawAmount);
        return drawCardsGA;
    }
}
