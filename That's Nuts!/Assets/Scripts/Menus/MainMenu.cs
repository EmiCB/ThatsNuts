using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void StartTutorial() {
        SceneManager.LoadScene("Tutorial");
    }

    public void StartGame() {
        SceneManager.LoadScene("MainScene");
    }

    public void Exit() {
        Application.Quit();
    }
}
