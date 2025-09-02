using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class CardViewCreator : Singleton<CardViewCreator>
{
    [SerializeField] CardView cardViewPrefab;
    public CardView CreateCardView(Card card, Vector3 position, quaternion rotation)
    {
        CardView cardView = Instantiate(cardViewPrefab, position, rotation);
        cardView.transform.localScale = Vector3.zero;
        cardView.transform.DOScale(Vector3.one, 0.2f);
        cardView.SetupCard(card);
        return cardView;
    }
}
