using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {
    public GameObject textBox;
    public Text content;

    public TextAsset textFile;
    public string[] textLines;

    public int lineIndex;
    public int endIndex;

    public PlayerController player;

    private bool firstMove;
    private bool firstJump;
    private bool firstClimb;

    void Start() {
        player = FindObjectOfType<PlayerController>();

        if (textFile != null) textLines = (textFile.text.Split('\n'));

        lineIndex = 0;
        endIndex = textLines.Length - 1;

        firstMove = false;
        firstJump = false;
        firstClimb = false;
    }

    void Update() {
        content.text = textLines[lineIndex];
        CheckProgress();
    }

    public void CheckProgress() {
        // after movement
        if (player.isRunning && !firstMove) {
            lineIndex = 1;
            firstMove = true;
        }
        // after jump
        if (player.isJumping && !firstJump) {
            lineIndex = 2;
            firstJump = true;
        }
        // after climb
        if (player.isClimbing && !firstClimb) {
            lineIndex = 3;
            firstClimb = true;
        }
        // after french fry
        if (player.isSlow) {
            lineIndex = 4;
        }
        // pollution
        if (player.transform.position.x > 45 && player.transform.position.x < 80) {
            lineIndex = 5;
        }
        // pause menu
        if (player.transform.position.x > 80) {
            lineIndex = 6;
        }
    }
}
