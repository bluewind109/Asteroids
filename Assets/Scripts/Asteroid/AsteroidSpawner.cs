using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public static System.Action<int, Vector2, float> SPLIT_ASTEROID;

    [SerializeField] private GameObject asteroidContainer;
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private int spawnAmount = 1;
    [SerializeField] private float spawnAngleRange = 15f;

    void Start()
    {
        SPLIT_ASTEROID += SplitAsteroid;
        InvokeRepeating(nameof(SpawnAsteroid), 1f, spawnInterval);
    }

    public void Reset()
    {
        foreach (Transform child in asteroidContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void SpawnAsteroid()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnRadius;
            Vector3 spawnPosition = this.transform.position + spawnDirection;

            float spawnAngle = Random.Range(-spawnAngleRange, spawnAngleRange);
            Quaternion spawnRotation = Quaternion.AngleAxis(spawnAngle, Vector3.forward);

            Asteroid asteroid = Instantiate(
                asteroidPrefab, 
                spawnPosition, 
                spawnRotation, 
                asteroidContainer.transform)
                .GetComponent<Asteroid>();
            asteroid.size = Random.Range(asteroid.scaleRange.x, asteroid.scaleRange.y);
            asteroid.Init(-spawnDirection);
        }
    }

    private void SplitAsteroid(int count, Vector2 position, float scale)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized;
            Vector2 spawnPosition = position + Random.insideUnitCircle * 0.5f;

            float spawnAngle = Random.Range(0f, 360f);
            Quaternion spawnRotation = Quaternion.AngleAxis(spawnAngle, Vector3.forward);

            Asteroid asteroid = Instantiate(
                asteroidPrefab, 
                spawnPosition, 
                spawnRotation, 
                asteroidContainer.transform)
                .GetComponent<Asteroid>();
            asteroid.size = scale * 0.5f;
            asteroid.Init(-spawnDirection);
        }
    }
}
