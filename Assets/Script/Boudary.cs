using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boudary : Singleton<Boudary>
{
    private float maxHeight;
    private float maxWidth;


    private void Start()
    {
        maxHeight = Camera.main.pixelHeight;
        maxWidth = Camera.main.pixelWidth;
    }

    public Vector2 RandomTopPosition()
    {
        float x = UnityEngine.Random.Range(0, maxWidth);
        Vector2 screenPos = new Vector2(x, maxHeight + 20);
        return Camera.main.ScreenToWorldPoint(screenPos);
    }

    public Vector2 GetPositionOnTop(Vector2 pos)
    {
        Vector2 screenPos = new Vector2(0, maxHeight);
        Vector2 result = Camera.main.ScreenToWorldPoint(screenPos);
        result.x = pos.x;
        return result;
    }

    public bool IsInScreen(Vector2 postion)
    {
        Vector2 pos = Camera.main.WorldToScreenPoint(postion);
        bool result = true;

        if (pos.x < 0 || pos.x > maxWidth) result = false;
        if (pos.y < 0 || pos.y > maxHeight) result = false;

        return result;
    }

    public Vector2 ClampBoudary(Vector2 position)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(position);
        screenPos.x = Mathf.Clamp(screenPos.x,0, maxWidth);
        screenPos.y = Mathf.Clamp(screenPos.y,0, maxHeight);

        return Camera.main.ScreenToWorldPoint(screenPos);
    }

}
