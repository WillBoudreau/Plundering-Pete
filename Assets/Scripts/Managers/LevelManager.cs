using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string levelName;
    public CollectorManager collectorManager;
    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        levelName = SceneManager.GetActiveScene().name;
    }
    public void LoadLevel(string name)
    {
        Debug.Log("Level load requested for: " + name);
        SceneManager.LoadScene(name);
        if (name == "GameTestScene")
        {
            collectorManager.SpawnDoubloons();
        }
    }
    public void QuitRequest()
    {
        Debug.Log("Quit requested");
        Application.Quit();
    }

}
