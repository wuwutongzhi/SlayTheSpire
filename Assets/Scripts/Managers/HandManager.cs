using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class HandManager : MonoBehaviour
{
    [SerializeField] int maxHandSize;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform cardSpawnPoint;
    [SerializeField] SplineContainer splineContainer;
    private List<GameObject> handCards = new List<GameObject>();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawCard();
        }

    }
    private void DrawCard()
    {
        if (handCards.Count >= maxHandSize) return;
        GameObject newCard = Instantiate(cardPrefab, cardSpawnPoint.position, Quaternion.identity);
        handCards.Add(newCard);
        UpdateCardPosition();
    }
    private void UpdateCardPosition()
    {
        if (handCards.Count == 0) return;
        float cardSpacing = 1.0f; // Adjust this value for spacing between cards
        float firstCardPosition = 0.5f - (handCards.Count - 1) / 2f* cardSpacing;
        Spline spline = splineContainer.Spline;
        for (int i = 0; i < handCards.Count; i++)
        {
            float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluatePosition(p);
            Vector3 up = spline.EvaluatePosition(p);
            Quaternion Rotation = Quaternion.LookRotation(up,Vector3.Cross(up,forward).normalized);
            handCards[i].transform.DOMove(splinePosition, 0.25f);
            handCards[i].transform.DORotateQuaternion(Rotation, 0.25f);
        }
    }
}
