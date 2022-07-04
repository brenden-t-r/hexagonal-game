using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button resumeButton;

    void Start()
    {
        exitButton.onClick.AddListener(Exit);
        resumeButton.onClick.AddListener(Resume);
    }

    private void Exit()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
 
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void Resume()
    {
        Play.PlayEnabled = true;
        SceneManager.UnloadSceneAsync("MenuScene");
    }
}
