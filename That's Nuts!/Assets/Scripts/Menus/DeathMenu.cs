using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {
    public static bool isPlayerDead;

    public GameObject deathMenu;
    public GameObject factsbox;
    public GameObject player;
    public GameObject pollution;

    private GameObject[] platforms;
    private GameObject[] collectables;

    public GameController gc;

    void Start() {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        deathMenu = GameObject.Find("DeathMenu");
        pollution = GameObject.FindGameObjectWithTag("Pollution");
        //factsbox = GameObject.Find("Facts");

        deathMenu.SetActive(false);
        factsbox.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        if (player.GetComponent<PlayerController>().isDead) StopGame();
    }

    
    public void StopGame() {
        deathMenu.SetActive(true);
        factsbox.SetActive(true);
        Time.timeScale = 0.0f;
        isPlayerDead = true;
    }

    public void Restart() {
        // reset player
        player.transform.position = new Vector3(0, -3f, 0);
        player.SetActive(true);
        player.GetComponent<PlayerController>().isDead = false;
        isPlayerDead = false;

        // reset platforms
        platforms = GameObject.FindGameObjectsWithTag("Platform");
        collectables = GameObject.FindGameObjectsWithTag("Collectable");

        for (int i = 0; i < platforms.Length; i++) {
            platforms[i].SetActive(false);
        }
        for (int i = 0; i < collectables.Length; i++) {
            collectables[i].SetActive(false);
        }

        gc.transform.position = new Vector3(19, -4.5f, 0);

        // reset fog
        pollution.transform.position = new Vector3(-40, 0, 0);
        

        // reset score
        gc.score = 0;

        // close death menu
        deathMenu.SetActive(false);
        factsbox.SetActive(false);

        // start time 
        Time.timeScale = 1.0f;
    }

    public void Exit() {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }
}
