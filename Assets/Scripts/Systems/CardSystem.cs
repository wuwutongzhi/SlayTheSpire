using DG.Tweening;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
    [SerializeField] HandView handView;
    [SerializeField] Transform drawPilePoint;
    [SerializeField] Transform discardPilePoint;
    private readonly List<Card> drawPile = new();
    private readonly List<Card> discardPile = new();
    private readonly List<Card> hand = new();
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardsPerformer);
        ActionSystem.AttachPerformer<DiscardAllCardsGA>(DiscardAllCardsPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);

    }
    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardsGA>();
        ActionSystem.DetachPerformer<DiscardAllCardsGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();

    }
    public void Setup(List<CardData> deckData)
    {
        foreach(var cardData in deckData)
        {
            Card card = new(cardData);
            drawPile.Add(card);
        }
    }
    private IEnumerator DrawCardsPerformer(DrawCardsGA drawCardsGA)
    {
        int acturalDrawAmount = Mathf.Min(drawCardsGA.Amount, drawPile.Count);
        int notDrawnAmount = drawCardsGA.Amount - acturalDrawAmount;
        for(int i = 0; i < acturalDrawAmount; i++)
        {
            yield return DrawCard();
        }
        if(notDrawnAmount > 0)
        {
            RefillDeck();
            for(int i = 0; i< notDrawnAmount; i++)
            {
                yield return DrawCard();
            }
        }
    }
    private IEnumerator DiscardAllCardsPerformer(DiscardAllCardsGA discardAllCardGA)
    {
        foreach(var card in hand)
        {
            CardView cardView = handView.RemoveCard(card);
            yield return DiscardCard(cardView);
        }
        hand.Clear();
    }
    private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    {
        hand.Remove(playCardGA.Card);
        CardView cardView = handView.RemoveCard(playCardGA.Card);
        yield return DiscardCard(cardView);

        SpendManaGA spendManaGA = new(playCardGA.Card.Mana);
        ActionSystem.Instance.AddReaction(spendManaGA);
        if(playCardGA.Card.ManualTargetEffect != null)
        {
            PerformEffectGA performEffectGA = new(playCardGA.Card.ManualTargetEffect, new (){ playCardGA.ManualTarget });
            ActionSystem.Instance.AddReaction(performEffectGA);
        }

        foreach (var effectWrapper in playCardGA.Card.OtherEffects)
        {
            List<CombatantView> targets = effectWrapper.TargetMode.GetTargets();
            PerformEffectGA performEffectGA = new(effectWrapper.Effect, targets);
            ActionSystem.Instance.AddReaction(performEffectGA);
        }
    }

    private IEnumerator DrawCard()
    {
        Card card = drawPile.Draw();
        hand.Add(card);
        CardView cardView = CardViewCreator.Instance.CreateCardView(card, drawPilePoint.position, drawPilePoint.rotation);
        yield return handView.AddCard(cardView);
    }
    private void RefillDeck()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear();
    }
    private IEnumerator DiscardCard(CardView cardView)
    {
        discardPile.Add(cardView.Card);
        cardView.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardView.transform.DOMove(discardPilePoint.position, 0.15f);
        yield return tween.WaitForCompletion();
        Destroy(cardView.gameObject);
    }
}
