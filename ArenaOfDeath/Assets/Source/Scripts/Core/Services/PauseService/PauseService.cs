using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseService : MonoBehaviour,IPause
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private bool _isPause = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _isPause == false)
            Pause();
        else if (Input.GetKeyDown(KeyCode.Escape) && _isPause)
        {
            Resume();
        }
    }

    public void Pause()
    {
        _isPause = true;
        Time.timeScale = 0;
        _pausePanel.SetActive(true);
    }

    public void Resume()
    {
        _isPause = false;
        Time.timeScale = 1;
        _pausePanel.SetActive(false);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
