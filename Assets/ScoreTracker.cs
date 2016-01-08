using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreTracker : MonoBehaviour {

    private int _Score;

	// Use this for initialization
	void Start () {
        GameObject runner = GameObject.Find("Runner");
        RunnerScript runnerScript = runner.GetComponent<RunnerScript>();
        runnerScript.LandedOnPlatform += OnSuccessfulLanding;
        runnerScript.FellIntoTheVoid += OnFellIntoVoid;

        UpdateScoreText();
	}
	
    private void OnSuccessfulLanding()
    {
        this._Score += 100;
        UpdateScoreText();
    }

    private void OnFellIntoVoid()
    {
        GameObject scoreTextObject = this.gameObject;
        scoreTextObject.GetComponent<Text>().text = string.Format("GAME OVER!");
    }

    private void UpdateScoreText()
    {
        GameObject scoreTextObject = this.gameObject;
        Text textComponent = scoreTextObject.GetComponent<Text>();
        textComponent.text = string.Format("Score: {0}", _Score);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
