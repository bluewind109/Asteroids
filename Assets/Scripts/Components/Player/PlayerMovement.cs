using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float turnSpeed = 1.0f;

    private bool isThrusting = false;
    private float turnDirection;

    public void Deactivate()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = 0f;

        this.gameObject.SetActive(false);
    }

    void Update()
    {
        if (this.gameObject.activeSelf == false)
            return;

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
