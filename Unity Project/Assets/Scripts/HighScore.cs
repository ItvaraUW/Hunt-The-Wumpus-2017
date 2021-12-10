// Sean Le wrote all of this code.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WumpusEngine;

public class HighScore : MonoBehaviour
{
    private GameControl control = GameControl.GetMaintainedInstance();

    [SerializeField]
    private Text[] highScores;

    [SerializeField]
    private Button toMainMenu;

	// Use this for initialization
    /// <summary>
    /// Clean method to set the text displays to highscores 
    /// (wasn't tested because highscore was kind of broken...)
    /// </summary>
	void Start ()
    {
        Score[] scores = control.GetHighScores();
        for (int i = 0; i < scores.Length; i++)
        {
            highScores[i].text = scores[i].ToString();
        }
	}

    // Update is called once per frame
    /// <summary>
    /// Nothing here.
    /// </summary>
    void Update ()
    {
		
	}

    /// <summary>
    /// Simple method to allow a button to return player to main menu screen.
    /// </summary>
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
