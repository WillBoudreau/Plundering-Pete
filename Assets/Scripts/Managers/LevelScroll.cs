using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScroll : MonoBehaviour
{
    public GameObject levelPrefab;
    public Transform levelSpawnPoint;
    public Transform levelSpawnInitialPoint;
    public float levelScrollSpeed = 5.0f;
    public float spawnThreshold = -1000f;

    private GameObject lastSpawnedLevel;
    // Start is called before the first frame update
    void Start()
    {
        SpawnInitialLevel();
    }

    void Update()
    {
        if (lastSpawnedLevel.transform.position.y < spawnThreshold)
        {
            GameObject newLevel = SpawnLevel();
            Destroy(lastSpawnedLevel);
            lastSpawnedLevel = newLevel;
        }
        lastSpawnedLevel.transform.Translate(Vector2.down * levelScrollSpeed * Time.deltaTime);
    }
    void SpawnInitialLevel()
    {
        lastSpawnedLevel = Instantiate(levelPrefab, levelSpawnInitialPoint.position, levelSpawnPoint.rotation);
    }
    GameObject SpawnLevel()
    {
        return Instantiate(levelPrefab, levelSpawnPoint.position, levelSpawnPoint.rotation);
    }
}
