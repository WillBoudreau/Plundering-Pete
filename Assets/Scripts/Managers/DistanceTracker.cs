using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DistanceTracker : MonoBehaviour
{
    [Header("Class calls")]
    public PlayerBehaviour playerBehaviour;
    [Header("Variables")]
    public float Distance;
    public float GameTimer;
    [Header("UI elements")]
    public Slider ditanceTracker;
    // Start is called before the first frame update
    void Start()
    {
        SetValues();
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        TrackDist();
    }
    void SetValues()
    {
        //Set all starting values
        Distance = 100f;
        GameTimer = 0f;
        ditanceTracker.maxValue = Distance;
    }
    void Timer()
    {
        //Overall Game timer
        GameTimer += Time.deltaTime;
    }
    void TrackDist()
    {
        //Tracks the time and translates into distance
        ditanceTracker.value = GameTimer;
    }
}