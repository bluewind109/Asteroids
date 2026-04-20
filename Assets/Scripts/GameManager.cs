using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Player player;
    [SerializeField] private float respawnDelay = 3f;

    private int _lives = 3;

    void Awake()
    {
        Instance = this;
    }

	void OnDestroy()
	{
		Instance = null;
	}

	public void PlayerDied()
    {
        _lives--;

        if (_lives <= 0)
        {
            GameOver();
            return;
        }

        this.player.transform.position = Vector3.zero;
        this.player.transform.rotation = Quaternion.identity;
        Invoke(nameof(Respawn), respawnDelay);
    }

    private void Respawn()
    {
        this.player.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
    }
}
