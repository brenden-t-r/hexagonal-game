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
        Debug.Log("exit");
        //If we are running in a standalone build of the game
#if UNITY_STANDALONE
        //Quit the application
        Application.Quit();
#endif
 
        //If we are running in the editor
#if UNITY_EDITOR
        //Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void Resume()
    {
        Play.PlayEnabled = true;
        SceneManager.UnloadSceneAsync("MenuScene");
    }
}
