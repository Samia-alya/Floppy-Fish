using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class GameManager : MonoBehaviour
{
    public Player player;
    public TMP_Text scoreText;
    public GameObject playButton;
    public GameObject game;
    public GameObject over;


    private int score;

    public void Awake()
    {
        Application.targetFrameRate = 60;
        Pause();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        game.SetActive(false);
        over.SetActive(false);


        Time.timeScale = 1f;
        player.enabled = true;     

        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }    
    }

    public void Pause()
    {
        Time.timeScale = 0f; 
        player.enabled = false;       
    }

    public void GameOver()
    {
        game.SetActive(true);
        over.SetActive(true);
        playButton.SetActive(true);

        Pause();
    }
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
    
}
