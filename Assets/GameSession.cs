using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] Text lives;
    [SerializeField] Text scoreText;
    [SerializeField] int score = 0;
    public string level;
    // Start is called before the first frame update
    private void Start()
    {
        lives.text = playerLives.ToString();
        scoreText.text = score.ToString(); 
    }
    public void AddToScore(int scoretoAdd)
    {
        score += scoretoAdd;
        scoreText.text = score.ToString(); 
    }
    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length; 
        if(numGameSessions > 1)
        {
            Destroy(gameObject); 
        }
        else
        {
            DontDestroyOnLoad(gameObject); 
        }
    }
    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession(); 
        }
    }

    private void TakeLife()
    {
        playerLives--; 
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        lives.text = playerLives.ToString();
        



    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0); // Replace with the level String later on
        playerLives = 3; 
        lives.text = playerLives.ToString();
    }
}
