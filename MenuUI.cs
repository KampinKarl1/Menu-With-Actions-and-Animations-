using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Animator menuAnimator = null;
    static readonly int OPEN_MENU = Animator.StringToHash("Open Menu");
    static readonly int CLOSE_MENU = Animator.StringToHash("Close Menu");
    private float durationAnimation = -1f;

    [Header("Open Button")]
    [SerializeField] private Button openMenuButton = null;

    [Header("Menu Panel Buttons")]
    [SerializeField] private Button resumeButton = null;
    [SerializeField] private Button settingsButton = null;
    [SerializeField] private Button toMainButton = null;
    [SerializeField] private Button exitButton = null;

    [Header("Are you sure Panel")]
    [SerializeField] private Button yesButton = null;
    [SerializeField] private Button noButton = null;

    [Header("Activatable Menu Objects")]
    [SerializeField] private GameObject yesNoPanel = null;

    [SerializeField] private GameObject menuObject = null;

    void Start()
    {
        durationAnimation = menuAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

        openMenuButton.onClick.AddListener(OpenMenu);
        resumeButton.onClick.AddListener(CloseMenu);

        settingsButton.onClick.AddListener(OpenSettings);
        toMainButton.onClick.AddListener(GoToMainMenu);
        exitButton.onClick.AddListener(OpenAreYouSurePanel);

        yesButton.onClick.AddListener(ExitApplication);
        noButton.onClick.AddListener(CloseAreYouSurePanel);

        //Start with menu closed
        menuObject.SetActive(false);
    }

    private IEnumerator HandleMenuClose()
    {
        menuAnimator.SetTrigger(CLOSE_MENU);

        float countdown = durationAnimation;

        while (countdown > 0)
        {
            countdown -= Time.deltaTime;
            yield return null;
        }

        menuObject.SetActive(false);
    }

    private void OpenMenu()
    {
        if (menuObject.activeSelf)
            return;

        resumeButton.enabled = true;

        menuObject.SetActive(true);

        menuAnimator.SetTrigger(OPEN_MENU);
    }

    private void CloseMenu()
    {
        if (!menuObject.activeSelf)
            return;

        resumeButton.enabled = false;

        CloseAreYouSurePanel();

        StartCoroutine(HandleMenuClose());
    }

    private void OpenAreYouSurePanel()
    {
        yesNoPanel.SetActive(true);
    }

    private void CloseAreYouSurePanel()
    {
        yesNoPanel.SetActive(false);
    }

    private void OpenSettings() 
    {
        Debug.Log("Opened Settings");
    }

    private void GoToMainMenu() 
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void ExitApplication()
    {
        Application.Quit();
    }
}