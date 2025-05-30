using UnityEngine;

public class ScriptSpawner : MonoBehaviour
{
    public GameObject[] trashPrefabs;
    public float spawnInterval = 1.5f;
    public float xMin = -2.5f;
    public float xMax = 2.5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnTrash), 1f, spawnInterval);
    }

    void SpawnTrash()
    {
        int index = Random.Range(0, trashPrefabs.Length);
        float xPos = Random.Range(xMin, xMax);
        Vector3 spawnPos = new Vector3(xPos, transform.position.y, 0);
        Instantiate(trashPrefabs[index], spawnPos, Quaternion.identity);
    }
}
