using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{

    GameObject canvas;
    Text text;
    Button button;
    bool gameIsOver = false;
    

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("ScreenCanvas");
        text = canvas.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameStart()
    {
        if (!gameIsOver)
            canvas.SetActive(false);
        else
            restartGame();
    }

    public void gameOver()
    {
        restartGame();
    }

    public void victory()
    {
        canvas.SetActive(true);
        text.text = "Victoria!";
        gameIsOver = true;
        Time.timeScale = 0f;
    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
