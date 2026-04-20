using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifetime = 10f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 direction, float speed)
    {
        rb.AddForce(direction.normalized * speed, ForceMode2D.Impulse);
        Destroy(this.gameObject, lifetime);
    }


}
