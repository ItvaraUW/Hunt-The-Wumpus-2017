// Sean Le wrote all of this code.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WumpusEngine;

public class MainMenu : MonoBehaviour
{
    private GameControl control = GameControl.GetMaintainedInstance();

    [SerializeField]
    private Button startGame;

    [SerializeField]
    private Button highScores;

    [SerializeField]
    private InputField playerName;

    [SerializeField]
    private InputField caveNumber;

    private bool nameInputted;
    private bool caveNumberInputted;

	// Use this for initialization
    /// <summary>
    /// Initializes the start game button as uninteractable.
    /// </summary>
	void Start ()
    {
        startGame.interactable = false;
	}
	
	// Update is called once per frame
    /// <summary>
    /// Only after the name and cave number is inputted,
    /// the button is able to be pressed.
    /// </summary>
	void Update ()
    {
		if (nameInputted && caveNumberInputted)
        {
            startGame.interactable = true;
        }
	}

    /// <summary>
    /// Calls GameStart method with the player name and the cave number,
    /// from the input text boxes.
    /// </summary>
    public void StartGame()
    {
        control.GameStart(playerName.text.ToString(), int.Parse(caveNumber.text.ToString()));
        SceneManager.LoadScene("Room");
    }

    /// <summary>
    /// Method that allows a button to send the player to highscore screen.
    /// </summary>
    public void HighScores()
    {
        SceneManager.LoadScene("HighScore");
    }

    /// <summary>
    /// If name is inputted, set variable to true.
    /// Boolean zen problem here somewhere.
    /// </summary>
    public void NameInputted()
    {
        nameInputted = true;
    }

    /// <summary>
    /// If cave number is inputted, set variable to true.
    /// Boolean zen problem here somewhere.
    /// </summary>
    public void CaveNumberInputted()
    {
        caveNumberInputted = true;
    }
}
