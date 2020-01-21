using System;
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
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject) return;

        switch (other.gameObject.tag)
        {
            case "Player":
                GameManager.Instance.PlaySound(AudioEvent.PlayerDestroyed);
                Destroy(other.gameObject);
                break;
            case "Bullet":
                GameManager.Instance.PlaySound(AudioEvent.AsteroidDestroyed, 1f / transform.localScale.x);
                Destroy(other.gameObject);
                if (transform.localScale.x > .45f)
                {
                    var rock = GameManager.Instance.SpawnRock(transform.position);
                    rock.transform.localScale = transform.localScale / 2;
                    rock = GameManager.Instance.SpawnRock(transform.position);
                    rock.transform.localScale = transform.localScale / 2;
                }
                Destroy(gameObject);
                break;
        }
    }
}
