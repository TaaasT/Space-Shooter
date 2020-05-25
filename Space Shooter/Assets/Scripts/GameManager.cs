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
    private Animator _pauseAnimator = null;

    private void Start()
    {
        _pauseAnimator = GameObject.Find("Pause_Menu").GetComponent<Animator>();
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void Update()
    {
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
            _pauseAnimator.SetBool("isPaused", true);
        }

    }

    public void GameOver()
    {
        _isGameOver = true;
    }

   public void ResumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

}
