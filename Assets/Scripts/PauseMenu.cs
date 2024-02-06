using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Rigidbody rb;


    public void pauseGame()
    {
        pausePanel.SetActive(true);
        rb.isKinematic = true;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void resumeGame()
    {
        pausePanel.SetActive(false);
        rb.isKinematic = false;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void resetGame()
    {
        resumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}