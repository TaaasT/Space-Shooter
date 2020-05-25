using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private static ScoreBoard _instance;
    public static ScoreBoard Instance { get { return _instance; } }

    public int highScore;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this);
        }


        DontDestroyOnLoad(this);
    }

}