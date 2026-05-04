using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public Transform player;
    public GameObject gameEndUI;
    public TextMeshProUGUI tryCountText;
    

    static int tryCount = 0;
    void Start()
    {
       
        tryCount++;
        tryCountText.text = "Attempt: " + tryCount;
        gameOverUI.SetActive(false);
        gameEndUI.SetActive(false);
    }

    public void GameEnd()
    {
        gameEndUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void GameOver()
    {   
      
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}

