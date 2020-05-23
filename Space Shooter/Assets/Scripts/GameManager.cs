using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;
    public bool isCoopMode = false;

    [SerializeField]
    private GameObject _pauseMenuPanel;

    private void Update()
    {
        _pauseMenuPanel.SetActive(false);

        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(0); // Main Menu
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            _pauseMenuPanel.SetActive(true);
        }

    }

    public void GameOver()
    {
        _isGameOver = true;
    }

}
