using UnityEngine;
using UnityEngine.UI;

public class ViewportFollow : MonoBehaviour
{
    public RectTransform playerUI; // Player UI trong ScrollView
    public RectTransform viewport; // Viewport của ScrollView
    public ScrollRect scrollRect;  // ScrollRect của ScrollView

    void Update()
    {
        if (!IsInViewport(playerUI, viewport))
        {
            ScrollToPlayer();
        }
    }

    bool IsInViewport(RectTransform target, RectTransform viewport)
    {
        Vector3[] targetCorners = new Vector3[4];
        Vector3[] viewportCorners = new Vector3[4];

        target.GetWorldCorners(targetCorners);
        viewport.GetWorldCorners(viewportCorners);

        // Kiểm tra xem player có nằm trong viewport hay không
        return targetCorners[0].y >= viewportCorners[0].y &&
               targetCorners[1].y <= viewportCorners[1].y;
    }

    void ScrollToPlayer()
    {
        float normalizedPosition = Mathf.Clamp01(1 - (playerUI.localPosition.y / viewport.rect.height));
        scrollRect.verticalNormalizedPosition = normalizedPosition;
    }
}
