using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float timer = 0;
    public bool isPaused;
    public GameObject PauseMenue;

    public void Start()
    {
        Time.timeScale = 0;
        PauseMenue.SetActive(true);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckIfPaused();
        }
    }

    public void RemoveCursor(bool lockState)
    {
        if (lockState)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

    public void AddToTimer(float addedTime)
    {
        timer += addedTime;
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        isPaused = false;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void CheckIfPaused()
    {
        if (isPaused)
        {
            RemoveCursor(true);
            Time.timeScale = 1;
            PauseMenue.SetActive(false);
            isPaused = false;
        }
        else
        {
            RemoveCursor(false);
            Time.timeScale = 0;
            PauseMenue.SetActive(true);
            isPaused = true;
        }
    }
}
