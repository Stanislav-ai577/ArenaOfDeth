using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour,IMenu
{
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
