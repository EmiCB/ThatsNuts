using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathFacts : MonoBehaviour {
	public GameObject textBox;
	public Text content;

	public TextAsset textFile;
	public string[] textLines;

	public int lineIndex;
	public int endIndex;

	void Start() {
		if (textFile != null) textLines = (textFile.text.Split('\n'));

		lineIndex = 0;
		endIndex = textLines.Length - 1;
	}

	void FixedUpdate() {
		content.text = textLines[lineIndex];
		PickLine();
	}

	public void PickLine() {
		lineIndex = Random.Range(0, textLines.Length - 1);
	}
}
