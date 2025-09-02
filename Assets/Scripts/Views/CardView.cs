using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text description;
    [SerializeField] TMP_Text mana;
    [SerializeField] SpriteRenderer imageSR;
    [SerializeField] GameObject wrapper;
    [SerializeField] LayerMask dropLayer;
    public Card Card { get; private set; }

    private Vector3 dragStartPosition;
    private Quaternion dragStartRotation;
    public void SetupCard(Card card)
    {
        Card = card;
        title.text = card.Title;
        description.text = card.Description;
        mana.text = card.Mana.ToString();
        imageSR.sprite = card.Image;
    }
    private void OnMouseEnter()
    {
        if (!InterActionSystem.Instance.PlayerCanHover()) return;
        wrapper.SetActive(false);
        Vector3 pos = new(transform.position.x, -2, 0);
        CardViewHoverSystem.Instance.Show(Card, pos);
    }
    private void OnMouseExit()
    {
        if (!InterActionSystem.Instance.PlayerCanHover()) return;
        CardViewHoverSystem.Instance.Hide();
        wrapper.SetActive(true);
    }
    private void OnMouseDown()
    {
        if (!InterActionSystem.Instance.PlayerCanInteract()) return;
        if(Card.ManualTargetEffect != null)
        {
            ManualTargetSystem.Instance.StartTargeting(transform.position);
        }
        else
        {
            InterActionSystem.Instance.PlayerIsDragging = true;
            wrapper.SetActive(true);
            CardViewHoverSystem.Instance.Hide();
            dragStartPosition = transform.position;
            dragStartRotation = transform.rotation;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
        }

    }
    private void OnMouseDrag()
    {
        if (!InterActionSystem.Instance.PlayerCanInteract()) return;
        if (Card.ManualTargetEffect != null) { return; }
        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);

    }
    private void OnMouseUp()
    {
        if (!InterActionSystem.Instance.PlayerCanInteract()) return;
        if (Card.ManualTargetEffect != null)
        {
            EnemyView target = ManualTargetSystem.Instance.EndTargeting(MouseUtil.GetMousePositionInWorldSpace(-1));
            if(target != null && ManaSystem.Instance.HasEnoughMana(Card.Mana))
            {
                PlayCardGA playCardGA = new(Card, target);
                ActionSystem.Instance.Perform(playCardGA);
            }
        }
        else
        {
            if (ManaSystem.Instance.HasEnoughMana(Card.Mana)
                && Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, 10f, dropLayer))
            {
                PlayCardGA playCardGA = new(Card);
                ActionSystem.Instance.Perform(playCardGA);
            }
            else
            {
                transform.position = dragStartPosition;
                transform.rotation = dragStartRotation;
            }
            InterActionSystem.Instance.PlayerIsDragging = false;
        }

    }
}
