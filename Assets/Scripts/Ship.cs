using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), 
    typeof(CircleCollider2D))]
public class Ship : MonoBehaviour
{
    [SerializeField] private float thrustForce = 3;
    [SerializeField] private float rotateDegreesPerSecond = 45;

    [Header("Control buttons")]
    [SerializeField] private KeyCode moveForwardButton = KeyCode.Space;

    [SerializeField] private KeyCode rotateLeftButton = KeyCode.LeftArrow;
    [SerializeField] private KeyCode rotateRightButton = KeyCode.RightArrow;
    
    private Rigidbody2D rb;
    
    private Vector2 _thrustDirection;
    private float _rotateDirection;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _thrustDirection.x = Input.GetKey(moveForwardButton) ? 1 : 0;
        if (Input.GetKey(rotateLeftButton))
            _rotateDirection = 1;
        else if (Input.GetKey(rotateRightButton))
            _rotateDirection = -1;
        else
            _rotateDirection = 0;
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, _rotateDirection * rotateDegreesPerSecond * Time.fixedDeltaTime);
        
        var z = transform.eulerAngles.z;
        var direction = new Vector2(
            _thrustDirection.x * Mathf.Cos(Mathf.Deg2Rad * z), 
            _thrustDirection.x * Mathf.Sin(Mathf.Deg2Rad * z)
            );
        rb.AddForce(direction * thrustForce);
    }

    private void OnBecameInvisible()
    {
        var newPosition = 2 * ScreenUtils.ScreenCenter - transform.position;
        rb.MovePosition(newPosition);
    }
}
