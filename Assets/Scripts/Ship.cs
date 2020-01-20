using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), 
    typeof(CircleCollider2D))]
public class Ship : MonoBehaviour
{
    [SerializeField] private float thrustForce = 3;
    [SerializeField] private float rotateDegreesPerSecond = 45;

    [SerializeField] private GameObject bulletPrefab;

    [Header("Control buttons")]
    [SerializeField] private KeyCode moveForwardButton = KeyCode.Space;

    [SerializeField] private KeyCode rotateLeftButton = KeyCode.LeftArrow;
    [SerializeField] private KeyCode rotateRightButton = KeyCode.RightArrow;
    
    [SerializeField] private KeyCode shootButton = KeyCode.LeftControl;

    private Transform t;
    private Rigidbody2D rb;
    
    private float _thrustDirection;
    private float _rotateDirection;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        t = transform;
    }

    private void Update()
    {
        _thrustDirection = Input.GetKey(moveForwardButton) ? 1 : 0;
        if (Input.GetKey(rotateLeftButton))
            _rotateDirection = 1;
        else if (Input.GetKey(rotateRightButton))
            _rotateDirection = -1;
        else
            _rotateDirection = 0;

        if (Input.GetKeyDown(shootButton))
            Shoot();
    }

    private void FixedUpdate()
    {
        t.Rotate(Vector3.forward, _rotateDirection * rotateDegreesPerSecond * Time.fixedDeltaTime);
        rb.AddForce(GetLookDirection() * thrustForce);
    }

    private Vector2 GetLookDirection()
    {
        var z = t.eulerAngles.z;
        return new Vector2(
            _thrustDirection * Mathf.Cos(Mathf.Deg2Rad * z), 
            _thrustDirection * Mathf.Sin(Mathf.Deg2Rad * z)
        );
    }

    private void Shoot()
    {
        GameManager.Instance.PlaySound(AudioEvent.Shot);
        Instantiate(bulletPrefab, t.position, t.rotation);
    }
}
