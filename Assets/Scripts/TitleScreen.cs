using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    private static readonly Player.PlayerClass[] ClassOptions = { new Player.GothGirl(), new Player.Devil() };
    
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Slider progressBar;
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private Button nextClassBtn;
    [SerializeField] private Button prevClassBtn;
    [SerializeField] private TextMeshProUGUI classSelectedText;
    [SerializeField] private Image classSelectedImage;
    [SerializeField] private Avatars avatars;
    private AsyncOperation loading;
    private Player.PlayerClass selectedClass = ClassOptions[0];
    private int selectedClassIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(Exit);
        nextClassBtn.onClick.AddListener(ClassSelectNext);
        prevClassBtn.onClick.AddListener(ClassSelectPrev);
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
        PlayerData.playerClass = selectedClass;
        loadingCanvas.SetActive(true);
        loading = SceneManager.LoadSceneAsync("MainScene");
    }

    private void ClassSelectNext()
    {
        selectedClassIndex += 1;
        UpdateSelectedClass();
    }

    private void ClassSelectPrev()
    {
        selectedClassIndex -= 1;
        UpdateSelectedClass();
    }

    private void UpdateSelectedClass()
    {
        nextClassBtn.interactable = selectedClassIndex < ClassOptions.Length - 1;
        prevClassBtn.interactable = selectedClassIndex > 0;
        selectedClass = ClassOptions[selectedClassIndex];
        classSelectedText.SetText(selectedClass.GetName());
        classSelectedImage.sprite = avatars.GetAvatar(selectedClass.GetAvatarFileName());
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
