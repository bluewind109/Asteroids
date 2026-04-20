using Unity.VisualScripting;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int spawnAmount = 1;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float spawnAngleRange = 15f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), 1f, spawnInterval);
    }

    private void SpawnAsteroid()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnRadius;
            Vector3 spawnPosition = this.transform.position + spawnDirection;

            float spawnAngle = Random.Range(-spawnAngleRange, spawnAngleRange);
            Quaternion spawnRotation = Quaternion.AngleAxis(spawnAngle, Vector3.forward);

            Asteroid asteroid = Instantiate(asteroidPrefab, spawnPosition, spawnRotation).GetComponent<Asteroid>();
            asteroid.SetTrajectory(-spawnDirection);}
    }
}
