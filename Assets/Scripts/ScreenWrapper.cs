using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ScreenWrapper : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnBecameInvisible()
    {
        var position = transform.position;
        if (position.x <= ScreenUtils.ScreenLeft.x || position.x >= ScreenUtils.ScreenRight.x)
            position.x = 2 * ScreenUtils.ScreenCenter.x - position.x;
        if (position.y <= ScreenUtils.ScreenBottom.y || position.y >= ScreenUtils.ScreenTop.y)
            position.y = 2 * ScreenUtils.ScreenCenter.y - position.y;
        transform.position = position;
    }
}
