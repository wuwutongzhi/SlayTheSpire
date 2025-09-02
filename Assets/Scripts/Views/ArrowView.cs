using UnityEngine;

public class ArrowView : MonoBehaviour
{
    [SerializeField] GameObject arrowHead;
    [SerializeField] LineRenderer lineRenderer;
    private Vector3 startPosition;
    private void Update()
    {
        Vector3 endPosition = MouseUtil.GetMousePositionInWorldSpace();
        Vector3 direction = -(startPosition - arrowHead.transform.position).normalized;
        lineRenderer.SetPosition(1, endPosition - direction * 0.5f);
        arrowHead.transform.position = endPosition;
        arrowHead.transform.right = direction;
    }
    public void SetupArrow(Vector3 startPositon)
    {
        this.startPosition = startPositon;
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, MouseUtil.GetMousePositionInWorldSpace());
    }
}
