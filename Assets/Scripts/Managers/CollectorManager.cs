using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectorManager : MonoBehaviour
{
    [Header("Class calls")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Camera mainCamera;
    [Header("Variables")]
    [SerializeField] private GameObject Doubloon;
    [SerializeField] private List<GameObject> Doubloons = new List<GameObject>();

    [SerializeField] private int  numofDoubloons;
    public bool hasSpawnedDoubloons; 
    [SerializeField] private Vector2 mapMinBounds;
    [SerializeField] private Vector2 mapMaxBounds;

    // Start is called before the first frame update
    void Start()
    {
        numofDoubloons = 10;
        AddDoubloonsToList();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    void Update()
    {
        AddDoubloonsToList();
    }
    //Add doubloons to list
    void AddDoubloonsToList()
    {
        if (Doubloons.Count <= 0)
        {
            for (int i = 0; i < numofDoubloons; i++)
            {
                Doubloons.Add(Doubloon);
            }
        }
    }
    //Spawn Doubloons from list
    public void SpawnDoubloons(Transform playerTransform, float safeDistance)
    {
        Debug.Log("Spawning Doubloons");
        if (levelManager.levelName == "GameTestScene" && !hasSpawnedDoubloons)
        {
            for (int i = 0; i < numofDoubloons; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                if (Vector3.Distance(spawnPosition, playerTransform.position) < safeDistance || IsInCameraView(spawnPosition))
                {
                    spawnPosition = GetRandomSpawnPosition();
                }
                else if (spawnPosition == Doubloons[i].transform.position)
                {
                    spawnPosition = GetRandomSpawnPosition();
                }
                else
                {
                    Instantiate(Doubloons[i], spawnPosition, Quaternion.identity);
                }
            }
            hasSpawnedDoubloons = true; 
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(mapMinBounds.x, mapMaxBounds.x);
        float y = Random.Range(mapMinBounds.y, mapMaxBounds.y);
        return new Vector3(x, y, -2);
    }

    //Check if the position is within the camera's view
    private bool IsInCameraView(Vector3 position)
    {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(position);
        return viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }  
}
