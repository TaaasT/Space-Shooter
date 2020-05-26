using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText, _highScore;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartLevelText;
    
    private int _bestScore = 0;

    private GameManager _gameManager;
   
    void Start()
    {
        _bestScore = PlayerPrefs.GetInt("HighScore", 0);
        _scoreText.text = "Score: " + 0;
        _highScore.text = "Best: " + _bestScore;
        _gameOverText.gameObject.SetActive(false);
        _restartLevelText.gameObject.SetActive(false);


        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        
        if (_gameManager == null)
        {
            Debug.Log("GameManager is NULL");
        }

    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void CheckHighScore(int playerScore)
    {
        if(_bestScore < playerScore)
        {
            _bestScore = playerScore;
            PlayerPrefs.SetInt("HighScore", _bestScore);
            ScoreBoard.Instance.highScore = _bestScore;
            _highScore.text = "Best: " + _bestScore;
        }
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _liveSprites[currentLives];

        if(currentLives < 1)
        {
            GameOverScreen();
        }
    }

    public void GameOverScreen()
    {
        _gameManager.GameOver();
        StartCoroutine(BlinkingText());
        _restartLevelText.gameObject.SetActive(true);
    }

    IEnumerator BlinkingText()
    {
        while(true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ResumePlay()
    {
        _gameManager.ResumeGame();
        
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
