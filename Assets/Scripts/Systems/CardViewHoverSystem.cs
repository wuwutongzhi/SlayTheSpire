using UnityEngine;

public class CardViewHoverSystem : Singleton<CardViewHoverSystem>
{
    [SerializeField] CardView cardViewHover;
    public void Show(Card card, Vector3 position)
    {
        cardViewHover.gameObject.SetActive(true);
        cardViewHover.SetupCard(card);
        cardViewHover.transform.position = position;
    }
    public void Hide()
    {
        cardViewHover.gameObject.SetActive(false);
    }
}
