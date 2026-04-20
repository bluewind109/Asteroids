using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Player player;
    [SerializeField] private AsteroidSpawner asteroidSpawner;
    [SerializeField] private GameObject particleContainer;

    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private float respawnDelay = 3f;
    [SerializeField] private float invulnerableDuration = 3f;

    private bool isGameOver = false;
    private int _score = 0;
    private int _lives = 3;

    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }

    void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ResetartGame();
            }
        }
    }

    void ResetartGame()
    {
        isGameOver = false;
        _score = 0;
        _lives = 3;
        Respawn();
        asteroidSpawner.Reset();
    }

    public void SpawnExplosion(Vector2 position)
    {
        if (_explosionEffect == null) return;
        Instantiate(
            _explosionEffect, 
            position, 
            Quaternion.identity, 
            particleContainer.transform
        );
    }

    public void OnAsteroidDestroyed(float size, Vector2 position)
    {
        int points = Mathf.RoundToInt(size * 10);
        _score += points;
        SpawnExplosion(position);
        Debug.Log("Score: " + _score);
    }

    public void OnPlayerDied()
    {
        _lives--;
        SpawnExplosion(player.transform.position);

        if (_lives <= 0)
        {
            GameOver();
            return;
        }

        Invoke(nameof(Respawn), respawnDelay);
    }

    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.transform.rotation = Quaternion.identity;
        this.player.UpdateColor(Color.red);
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollision), invulnerableDuration);
    }

    private void TurnOnCollision()
    {
        this.player.UpdateColor(Color.white);
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        isGameOver = true;
    }
}
