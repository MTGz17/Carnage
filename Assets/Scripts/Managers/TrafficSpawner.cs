using UnityEngine;

public class TrafficSpawner : MonoBehaviour
{
    [Header("Car Settings")]
    public GameObject[] carPrefabs;          // Array of car prefabs to spawn
    public Vector2 speedRange = new Vector2(5f, 15f); // Random speed range

    [Header("Spawn Settings")]
    public float minSpawnDelay = 1f;  // Minimum time between spawns
    public float maxSpawnDelay = 3f;  // Maximum time between spawns

    [Header("Spawn Position Ranges")]
    public Vector2 spawnXRange = new Vector2(10f, 15f); // Min and Max X
    public float spawnY = 0.45f;                         // Fixed height
    public Vector2 spawnZRange = new Vector2(30f, 35f); // Min and Max Z

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private System.Collections.IEnumerator SpawnLoop()
    {
        while (true)
        {
            // Wait for random time before spawning next car
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

            // Pick random position within X and Z ranges, fixed Y
            Vector3 spawnPos = new Vector3(
                Random.Range(spawnXRange.x, spawnXRange.y),
                spawnY,
                Random.Range(spawnZRange.x, spawnZRange.y)
            );

            // Pick random car prefab
            GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];

            // Spawn the car
            GameObject car = Instantiate(carPrefab, spawnPos, Quaternion.identity);

            // Give it a random speed
            float carSpeed = Random.Range(speedRange.x, speedRange.y);
            car.AddComponent<TrafficCar>().Init(carSpeed);
        }
    }
}

public class TrafficCar : MonoBehaviour
{
    private float speed;

    public void Init(float carSpeed)
    {
        speed = carSpeed;
    }

    private void Update()
    {
        // Move left in world space
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

        // Destroy once far off screen
        if (transform.position.x < -50f)
        {
            Destroy(gameObject);
        }
    }
}