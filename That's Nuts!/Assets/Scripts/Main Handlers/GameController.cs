using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    [SerializeField]
    private float fogSpeed;

    public int score;
    public int highScore;

    public Text scoreText;
    public Text highScoreText;

    public bool isTutorial;

    public GameObject player;

    public GameObject pollution;
    public Rigidbody2D prb;

    void Start() {
        // check if tutorial
        if (SceneManager.GetActiveScene().name == "Tutorial") isTutorial = true;
        else isTutorial = false;

        player = GameObject.FindGameObjectWithTag("Player");

        pollution = GameObject.FindGameObjectWithTag("Pollution");
        prb = pollution.GetComponent<Rigidbody2D>();

        fogSpeed = 7.0f;

        if (isTutorial) pollution.SetActive(false);
    }

    void Update() {
        UpdateScores();
        PollutionController();
    }

    void UpdateScores() {
        // high score
        if (score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        // display scores
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
    }

    void PollutionController() {
        if(!isTutorial) prb.velocity = new Vector2(fogSpeed, 0);
        else {
            if (player.transform.position.x >= 45) {
                // spawn fog
                pollution.SetActive(true);
                prb.velocity = new Vector2(fogSpeed, 0);
            }
        }

        if (player.activeSelf == false) prb.velocity = new Vector2(0, 0);
    }
}
