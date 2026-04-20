using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private float _rotationSpeed = 20f;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private Vector2 _scaleRange = new Vector2(0.5f, 1.5f);
    [SerializeField] private float _lifetime = 20f;

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
        float scale = Random.Range(_scaleRange.x, _scaleRange.y);
        this.transform.localScale = new Vector3(scale, scale, 1);

        _rb.mass = scale; // Mass proportional to size
    }

    public void SetTrajectory(Vector2 direction)
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
        if (_explosionEffect != null)
        {
            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            _spriteRenderer.enabled = false;
            Destroy(gameObject, _explosionEffect.main.duration);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
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
