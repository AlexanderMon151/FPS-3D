using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject SettingsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
        
    }

    public void ToggleSettings()
    {
        SettingsPanel.SetActive(!SettingsPanel.activeInHierarchy);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
