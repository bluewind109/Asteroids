using NUnit.Framework;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float turnSpeed = 1.0f;

    private Rigidbody2D rb;

    private bool isThrusting = false;
    private float turnDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isThrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1f;
        }
        else
        {
            turnDirection = 0f;
        }
    }

	void FixedUpdate()
	{
        if (isThrusting)
        {
            rb.AddForce(transform.up * moveSpeed);
        }

        if (turnDirection != 0f)
        {
            rb.AddTorque(turnDirection * turnSpeed);
        }
	}
}
