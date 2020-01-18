﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D),
    typeof(CircleCollider2D),
    typeof(SpriteRenderer))]
public class Asteroid : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private Sprite[] sprites;
    
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SetupMovement();
        SetupRenderer();
    }

    private void SetupMovement()
    {
        var randomAngle = Random.Range(0, 2 * Mathf.PI);
        var direction = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
        rb.AddForce(direction * moveSpeed, ForceMode2D.Impulse);
    }

    private void SetupRenderer()
    {
        sr.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    private void OnBecameInvisible()
    {
        var position = transform.position;
        if (position.x <= ScreenUtils.ScreenLeft.x || position.x >= ScreenUtils.ScreenRight.x)
            position.x = 2 * ScreenUtils.ScreenCenter.x - position.x;
        if (position.y <= ScreenUtils.ScreenBottom.y || position.y >= ScreenUtils.ScreenTop.y)
            position.y = 2 * ScreenUtils.ScreenCenter.y - position.y;
        rb.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(other.gameObject);
    }
}
