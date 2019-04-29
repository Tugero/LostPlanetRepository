using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loadlevel : MonoBehaviour
{
    public GameObject Pause;
    public GameObject player;
    private Scene scene;
    private bool isPaused = false;

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }
    public void OpenCotnrols()
    {
        SceneManager.LoadScene(1);
    }
    public void Win()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SceneManager.LoadScene(4);
    }
    public void Lose()
    {
        SceneManager.LoadScene(3);
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exiting Game");
    }
    public void Respwan() {
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
     }

    public void Level2()
    {
        SceneManager.LoadScene(5);
    }
    public void Level3()
    {
        SceneManager.LoadScene(6);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && isPaused == false || Input.GetButtonDown("Start") && isPaused == false)
        {
            Pause.SetActive(true);
            Time.timeScale = 0f;
            Screen.lockCursor = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            isPaused = true;
        }
        else if(Input.GetKeyDown(KeyCode.P) && isPaused == true || Input.GetButtonDown("Start") && isPaused == true)
        {
            Pause.SetActive(false);
            Time.timeScale = 1f;
            Screen.lockCursor = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isPaused = false;
        }
    }

    public void Resume()
    {
        Pause.SetActive(false);
        Time.timeScale = 1f;
       //Screen.lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
