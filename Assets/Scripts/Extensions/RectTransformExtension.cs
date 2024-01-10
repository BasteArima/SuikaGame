using UnityEngine;

public static class RectTransformExtension
{
    public static Vector4 TopLeftAnchor  = new Vector4(0, 1, 0, 1);
    public static Vector4 TopCenterAnchor= new Vector4(0.5f, 1, 0.5f, 1);
    public static Vector4 TopRightAnchor= new Vector4(1, 1, 1, 1);
    
    public static Vector4 MiddleLeftAnchor  = new Vector4(0, 0.5f, 0, 0.5f);
    public static Vector4 MiddleCenterAnchor = new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
    public static Vector4 MiddleRightAnchor  = new Vector4(1, 0.5f, 1, 0.5f);
    
    public static Vector4 BottomLeftAnchor  = new Vector4(0, 0, 0, 0);
    public static Vector4 BottomCenterAnchor  = new Vector4(0.5f, 0, 0.5f, 0);
    public static Vector4 BottomRightAnchor = new Vector4(1, 0, 1, 0);

    public static void SetAnchor(this RectTransform rectTransform, Vector4 anchor)
    {
        rectTransform.anchorMin = new Vector2(anchor.x, anchor.y);
        rectTransform.anchorMax = new Vector2(anchor.z, anchor.w);
    }

    public static void SetLocalScale(this RectTransform rectTransform, float scale)
    {
        rectTransform.localScale = new Vector3(scale, scale, scale);
    }
    
    public static void SetAnchorPositionY(this RectTransform rectTransform, float positionY)
    {
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, positionY);
    }
    
    public static void SetAnchorPositionX(this RectTransform rectTransform, float positionX)
    {
        rectTransform.anchoredPosition = new Vector2(positionX, rectTransform.anchoredPosition.y);
    }
    
    public static void SetHeight(this RectTransform rectTransform, float height)
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
    }
    
    public static void SetWidth(this RectTransform rectTransform, float width)
    {
        rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
    }
    
    public static void SetHeightAndWidth(this RectTransform rectTransform, float height, float width)
    {
        rectTransform.sizeDelta = new Vector2(width, height);
    }
}
