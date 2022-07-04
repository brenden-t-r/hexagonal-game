using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{

    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Slider progressBar;
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private TMP_InputField playerNameInput;
    private AsyncOperation loading;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
        if (loading != null)
        {
            Debug.Log(loading.progress);
            progressBar.value = Mathf.Clamp01(loading.progress / 0.9f);
        }
    }

    private void StartGame()
    {
        PlayerData.playerName = playerNameInput.text;
        PlayerData.playerClass = new Player.GothGirl();
        loadingCanvas.SetActive(true);
        loading = SceneManager.LoadSceneAsync("SampleScene");
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
}
