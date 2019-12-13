using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool isPaused;

    public GameObject pauseMenu;
    public GameObject textbox;
    public GameObject factsbox;

    public GameController gc;
    public PlayerController player;

    void Start() {
        pauseMenu = GameObject.Find("PauseMenu");
        //factsbox = GameObject.Find("Facts");
        pauseMenu.SetActive(false);
        factsbox.SetActive(false);

        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (gc.isTutorial) textbox = GameObject.Find("TextBox");
    }

    void Update() {
        // check for key press
        if (Input.GetKeyDown(KeyCode.Escape) && !player.isDead) {
            if (isPaused) Resume();
            else Pause();
        }
    }


    public void Pause() {
        pauseMenu.SetActive(true);
        factsbox.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;

        if (textbox != null) textbox.SetActive(false);
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        factsbox.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;

        if (textbox != null) textbox.SetActive(true);
    }

    public void ExitToMainMenu() {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }
}
