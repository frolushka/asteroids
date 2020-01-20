using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),
    typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float destroyTime = 2;

    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameManager.Instance.PlaySound(AudioEvent.AsteroidDestroyed);
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
