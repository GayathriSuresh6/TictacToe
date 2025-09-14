using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    private UIManager uiManager;
    private TurnManager turnManager;
    private AudioManager audioManager;

    private void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        uiManager = FindObjectOfType<UIManager>();
        audioManager = FindObjectOfType<AudioManager>();    
    }

    public void ResetGame()
    {
        turnManager.isGameStart = false;
        audioManager.PlayButtonTap(gameObject.transform.position);
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
    }

    public void QuitApp()
    {
        audioManager.PlayButtonTap(gameObject.transform.position);
        Application.Quit();
    }
}
