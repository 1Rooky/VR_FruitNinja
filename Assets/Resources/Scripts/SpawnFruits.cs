using UnityEngine;

public class SpawnFruits : MonoBehaviour
{
    [SerializeField] private float spawnInterval;
    [SerializeField] private float throwForce;

    private float nextSpawnTime;
    private GameObject fruitPrefab;
    private GameObject fruit;

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnFruit();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnFruit()
    {
        fruitPrefab = Resources.Load($"Prefabs/{Random.Range(1, 8)}") as GameObject;
        fruit = Instantiate(fruitPrefab, transform.position, Quaternion.identity) as GameObject;
        Rigidbody fruitRb = fruit.GetComponent<Rigidbody>();
        fruitRb.AddForce(Vector2.up * throwForce, ForceMode.Impulse);
    }
}