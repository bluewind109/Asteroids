using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 10f;

    private PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation).GetComponent<Bullet>();
        bullet.Init(transform.up, bulletSpeed);
    }

	void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            playerMovement.Deactivate();
            GameManager.Instance.PlayerDied();
        }
	}
}
