using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed = 10f;

    private SpriteRenderer _spriteRenderer;
    private PlayerMovement _playerMovement;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    public void UpdateColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation).GetComponent<Bullet>();
        bullet.Init(transform.up, _bulletSpeed);
    }

	void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            _playerMovement.Deactivate();
            GameManager.Instance.OnPlayerDied();
        }
	}
}
