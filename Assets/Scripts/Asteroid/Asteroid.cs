using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _rotationSpeed = 20f;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _lifetime = 20f;

    public Vector2 scaleRange = new Vector2(0.5f, 1.5f);

    public float size = 1f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
        this.transform.eulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
        this.transform.localScale = Vector3.one * this.size;

        _rb.mass = size; // Mass proportional to size
    }

    public void Init(Vector2 direction)
    {
        _rb.AddForce(direction.normalized * _moveSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, _lifetime);
    }

    void Update()
    {
        transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
    }

    private void Explode()
    {
        // Explode into smaller asteroids if large enough
        if (this.transform.localScale.x > scaleRange.x * 1.5f)
        {
            int splitAmount = 2;
            AsteroidSpawner.SPLIT_ASTEROID?.Invoke(
                splitAmount,
                this.transform.position,
                this.size
            );
        }

        GameManager.Instance.SpawnExplosion(transform.position);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Explode();
            Destroy(collision.gameObject);
        }
    }
}
