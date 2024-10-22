using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScroll : MonoBehaviour
{
    public GameObject LevelTile;
    public GameObject Player;
    Vector3 PlayerPos;
    public float maxDistToSpawn;
    public float LevelLength;

    // Start is called before the first frame update
    void Start()
    {
        maxDistToSpawn = 20;
    }

    // Update is called once per frame
    void Update()
    {
        TrackPlayer();
    }
    void TrackPlayer()
    {
        PlayerPos = Player.transform.position;
        if(PlayerPos.z > LevelLength - maxDistToSpawn)
        {
            SpawnTile();
        }
    }
    void SpawnTile()
    {
        Instantiate(LevelTile, new Vector3(0, 0, LevelLength), Quaternion.identity);
        LevelLength += 20;
    }
}
