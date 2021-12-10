// Sean Le wrote 100% of the code (and lost 90% of his sanity)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WumpusEngine;
using UnityEngine.SceneManagement;

public class RotationRoom : MonoBehaviour
{
    #region PrivateDeclarations
    private GameControl control = GameControl.GetMaintainedInstance();

    private bool isInteractable;

    private Room currentRoom;

    private Direction arrowDirection0;
    private Direction arrowDirection1;
    private Direction arrowDirection2;

    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject[] doors;

    [SerializeField]
    private Text roomNumberDisplay;

    [SerializeField]
    private Text[] playerStats;

    [SerializeField]
    private Text warningWumpus;

    [SerializeField]
    private Text warningPit;

    [SerializeField]
    private Text warningBat;

    [SerializeField]
    private Text hint;

    [SerializeField]
    private Text secret;

    [SerializeField]
    private GameObject secretDisplay;
    
    [SerializeField]
    private Button buyArrow;

    [SerializeField]
    private Button shootArrow;

    [SerializeField]
    private Button buySecret;

    [SerializeField]
    private GameObject shootArrowMenu;

    [SerializeField]
    private Button shootArrowDirection0;

    [SerializeField]
    private Button shootArrowDirection1;

    [SerializeField]
    private Button shootArrowDirection2;

    [SerializeField]
    private Text shootArrowDirection0Text;

    [SerializeField]
    private Text shootArrowDirection1Text;

    [SerializeField]
    private Text shootArrowDirection2Text;

    [SerializeField]
    private GameObject triviaMenu;

    [SerializeField]
    private GameObject gameFinish;

    [SerializeField]
    private Text gameFinishText;

    [SerializeField]
    private Text score;

    [SerializeField]
    private Button GoToHighscores;

    [SerializeField]
    private Text question;

    [SerializeField]
    private Text answer0;

    [SerializeField]
    private Text answer1;

    [SerializeField]
    private Text answer2;

    [SerializeField]
    private Button answer0Button;

    [SerializeField]
    private Button answer1Button;

    [SerializeField]
    private Button answer2Button;

    [SerializeField]
    private Text wumpusIndicator;

    [SerializeField]
    private Text pitIndicator;

    [SerializeField]
    private Text purchaseIndicator;

    [SerializeField]
    private GameObject TextDisplayPanel;
    #endregion

    // Use this for initialization
    /// <summary>
    /// On start of the "Room" Scene, sets the private room variable to the starting room
    /// and sets the updates the doors to be active if there is a door there.
    /// </summary>
    void Start()
    {
        currentRoom = control.StartingRoom();
        UpdateRooms();
        isInteractable = true;
    }
	
	// Update is called once per frame
    /// <summary>
    /// Two main things that are constantly updating every frame,
    /// the "rotation engine" which makes the room rotate using
    /// either keyboard (or gyro but that was never tested D:),
    /// and the "fix" to the ball suddenly falling through the 
    /// room due to a problem with rigidbody, which resets the position.
    /// </summary>
	void Update()
    {
        if (isInteractable)
        {
            Vector3 rotateBy = new Vector3(Input.GetAxis("Vertical"), 0.0f, Input.GetAxis("Horizontal"));
            Vector3 gyroRotateBy = new Vector3(Input.acceleration.x, 0.0f, Input.acceleration.z);
            if (Mathf.Approximately(rotateBy.x, 0.0f))
            {
                rotateBy.x = 0.0f;
            }

            if (Mathf.Approximately(rotateBy.z, 0.0f))
            {
                rotateBy.z = 0.0f;
            }
            this.transform.Rotate(rotateBy.x, 0.0f, -rotateBy.z);
            if (Mathf.Approximately(gyroRotateBy.x, 0.0f))
            {
                gyroRotateBy.x = 0.0f;
            }

            if (Mathf.Approximately(gyroRotateBy.z, 0.0f))
            {
                gyroRotateBy.z = 0.0f;
            }
            this.transform.Rotate(gyroRotateBy.x, 0.0f, -gyroRotateBy.z);
            Vector3 angles = this.transform.rotation.eulerAngles;
            if (angles.x < 180.0f)
            {
                angles.x = Mathf.Min(angles.x, 25.0f);
            }
            else
            {
                angles.x = Mathf.Max(angles.x, 335.0f);
            }
            if (angles.z < 180.0f)
            {
                angles.z = Mathf.Min(angles.z, 25.0f);
            }
            else
            {
                angles.z = Mathf.Max(angles.z, 335.0f);
            }
            this.transform.eulerAngles = new Vector3(angles.x, 0.0f, angles.z);
        }
        if (player.gameObject.transform.position.y < -20)
        {
            UpdateRooms();
            player.ResetPosition();
        }
    }

    /// <summary>
    /// Method that is called by "DoorTrigger.cs" to pass the direction the
    /// player went through to pass to game control to get next room.
    /// </summary>
    /// <param name="direction"></param>
    public void DoorTrigger(Direction direction)
    {
        currentRoom = control.GetRoom(direction);
        UpdateRooms();
    }

    /// <summary>
    /// Resets the room to "flat" and refreshes all the text displays while setting up room.
    /// Also checks the state of the game if game control passes that there are hazards.
    /// </summary>
    public void UpdateRooms()
    {
        if (!control.CheckHazards())
        {
            this.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            this.roomNumberDisplay.text = this.currentRoom.GetRoomNum().ToString();
            bool[] warnings = control.GetWarnings();
            warningWumpus.gameObject.SetActive(warnings[0]);
            warningPit.gameObject.SetActive(warnings[1]);
            warningBat.gameObject.SetActive(warnings[2]);
            this.hint.text = string.Format(this.control.GetTrivia());
            UpdateInventory();
            for (Direction dir = Direction.N; dir <= Direction.NW; dir++)
            {
                doors[(int)dir].SetActive(currentRoom.IsThereAnExit(dir));
            }
        }
        else
        {
            CheckState();
        }
    }

    /// <summary>
    /// Simple method to update the display of the player's inventory.
    /// </summary>
    public void UpdateInventory()
    {
        int[] unsorted = control.GetPlayerStats();
        int arrows = unsorted[0];
        int coins = unsorted[1];
        int turns = unsorted[2];
        playerStats[0].text = string.Format("Arrows: {0}", arrows);
        playerStats[1].text = string.Format("Coins: {0}", coins);
        playerStats[2].text = string.Format("Turn: {0}", turns);
    } 
   
    /// <summary>
    /// Checks state of game control (this only happens if there is a hazard)
    /// and starts initialization of the corresponding hazards.
    /// </summary>
    public void CheckState()
    {
        if (control.GetState() == GameState.Bats)
        {
            currentRoom = control.Bats();
            UpdateRooms();
            BatIndicator();
            Invoke("CloseBatIndicator", 5.0f);
        }
        if (control.GetState() == GameState.Pit)
        {
            player.FreezeBall();
            TriviaStart();
        }
        if (control.GetState() == GameState.Wumpus)
        {
            player.FreezeBall();
            TriviaStart();
        }
    }

    /// <summary>
    /// Purchases an arrow by setting the game state to purchase and to start a trivia match.
    /// </summary>
    public void BuyArrow()
    {
        isInteractable = false;
        control.StartPurchaseArrow();
        TriviaStart();
    }

    /// <summary>
    /// Method is called when the "Shoot Arrow" button is pressed.
    /// Checks for the current doors and sets up a text display with buttons of possible targets.
    /// </summary>
    public void ShootArrow()
    {
        isInteractable = false;
        int numberOfRooms = 0;
        shootArrowMenu.SetActive(true);
        shootArrowDirection0.interactable = false;
        shootArrowDirection1.interactable = false;
        shootArrowDirection2.interactable = false;
        for (Direction dir = Direction.N; dir <= Direction.NW; dir++)
        {
            if (doors[(int)dir].activeSelf)
            {
                numberOfRooms++;
                if (numberOfRooms == 1)
                {
                    shootArrowDirection0.interactable = true;
                    shootArrowDirection0Text.text = string.Format("Room {0}", currentRoom.WhereExitLeads(dir));
                    arrowDirection0 = dir;
                }
                if (numberOfRooms == 2)
                {
                    shootArrowDirection1.interactable = true;
                    shootArrowDirection1Text.text = string.Format("Room {0}", currentRoom.WhereExitLeads(dir));
                    arrowDirection1 = dir;
                }
                if (numberOfRooms == 3)
                {
                    shootArrowDirection2.interactable = true;
                    shootArrowDirection2Text.text = string.Format("Room {0}", currentRoom.WhereExitLeads(dir));
                    arrowDirection2 = dir;
                }
            }
        }
    }

    /// <summary>
    /// Same as BuyArrow(), sets game state and starts a trivia match.
    /// </summary>
    public void BuySecret()
    {
        isInteractable = false;
        control.StartPurchaseSecret();
        TriviaStart();
    }

    /// <summary>
    /// Super messy way to do this type of behavior but with the set dictionary
    /// there is another level of complexity.
    /// When button is pressed, arrow is fired at corresponding door.
    /// </summary>
    public void ShootArrowAtDoor0()
    {
        if (control.ShootArrow(arrowDirection0))
        {
            WinGame();
        }
        shootArrowMenu.SetActive(false);
        UpdateInventory();
    }

    /// <summary>
    /// Messy.
    /// When button is pressed, fire arrow at corresponding door.
    /// </summary>
    public void ShootArrowAtDoor1()
    {
        if (control.ShootArrow(arrowDirection1))
        {
            WinGame();
        }
        shootArrowMenu.SetActive(false);
        UpdateInventory();
    }

    /// <summary>
    /// Messy.
    /// When button is pressed, fire arrow at corresponding door.
    /// </summary>
    public void ShootArrowAtDoor2()
    {
        if (control.ShootArrow(arrowDirection2))
        {
            WinGame();
        }
        shootArrowMenu.SetActive(false);
        UpdateInventory();
    }

    /// <summary>
    /// Starts the trivia match by making menu active and calling setup method.
    /// </summary>
    public void TriviaStart()
    {
        triviaMenu.SetActive(true);
        control.StartMatch();
        TriviaSetUp();
    }

    /// <summary>
    /// When trivia is finished, checks the gamestate and calls methods accordingly.
    /// </summary>
    public void CloseTrivia()
    {
        player.UnfreezeBall();
        if (control.GetState() == GameState.GameLoss)
        {
            LoseGame();
        }
        else
        {
            if (control.MatchWin())
            {
                triviaMenu.SetActive(false);
                if (control.GetState() == GameState.PurchaseArrow)
                {
                    control.PurchaseArrow();
                }
                if (control.GetState() == GameState.PurchaseSecret)
                {
                    secretDisplay.SetActive(true);
                    secret.text = control.PurchaseSecret();
                }
                if (control.GetState() == GameState.Pit)
                {
                    player.UnfreezeBall();
                    UpdateRooms();
                }
                if (control.GetState() == GameState.Wumpus)
                {
                    player.UnfreezeBall();
                    UpdateRooms();
                }
                UpdateInventory();
                isInteractable = true;
            }
            else
            {
                if ((control.GetState() == GameState.PurchaseArrow) || (control.GetState() == GameState.PurchaseSecret))
                {
                    UpdateInventory();
                }
                isInteractable = true;
            }
        }
    }

    /// <summary>
    /// Called when gamestate is set to a loss.
    /// Displays score and pulls up gamefinish screen.
    /// </summary>
    public void LoseGame()
    {
        gameFinish.SetActive(true);
        gameFinishText.text = "You lose!";
        score.text = string.Format("Your score was {0}", control.GameOver());
    }

    /// <summary>
    /// Called when the player wins the game.
    /// Displays score and pulls up gamefinish screen.
    /// </summary>
    public void WinGame()
    {
        gameFinish.SetActive(true);
        gameFinishText.text = "You win!";
        score.text = string.Format("Your score was {0}", control.GameOver());
    }

    /// <summary>
    /// Very simple method to allow for a button to send player to highscore screen.
    /// </summary>
    public void GoToHighscore()
    {
        SceneManager.LoadScene("HighScore");
    }

    /// <summary>
    /// Method to set up trivia by calling methods from game control.
    /// </summary>
    public void TriviaSetUp()
    {
        question.text = control.GetQuestion();
        string[] choices = control.GetChoices();
        answer0.text = choices[0];
        answer1.text = choices[1];
        answer2.text = choices[2];
        wumpusIndicator.gameObject.SetActive(control.GetState() == GameState.Wumpus);
        pitIndicator.gameObject.SetActive(control.GetState() == GameState.Pit);
        purchaseIndicator.gameObject.SetActive((control.GetState() == GameState.PurchaseArrow) || (control.GetState() == GameState.PurchaseSecret));
    }

    /// <summary>
    /// Same as firing an arrow at certain door, this is very messy.
    /// A fun mess of if statements to check if answer is correct or not.
    /// </summary>
    public void PickAnswer0()
    {
        if (control.CheckAnswer(0))
        {
            if (control.MatchState())
            {
                TriviaSetUp();
            }
            else
            {
                CloseTrivia();
            }
        }
        else
        {
            if (control.MatchState())
            {
                TriviaSetUp();
            }
            else
            {
                CloseTrivia();
            }
        }
    }

    /// <summary>
    /// Same as firing an arrow at certain door, this is very messy.
    /// A fun mess of if statements to check if answer is correct or not.
    /// </summary>
    public void PickAnswer1()
    {
        if (control.CheckAnswer(1))
        {
            if (control.MatchState())
            {
                TriviaSetUp();
            }
            else
            {
                CloseTrivia();
            }
        }
        else
        {
            if (control.MatchState())
            {
                TriviaSetUp();
            }
            else
            {
                CloseTrivia();
            }
        }
    }

    /// <summary>
    /// Same as firing an arrow at certain door, this is very messy.
    /// A fun mess of if statements to check if answer is correct or not.
    /// </summary>
    public void PickAnswer2()
    {
        if (control.CheckAnswer(2))
        {
            if (control.MatchState())
            {
                TriviaSetUp();
            }
            else
            {
                CloseTrivia();
            }
        }
        else
        {
            if (control.MatchState())
            {
                TriviaSetUp();
            }
            else
            {
                CloseTrivia();
            }
        }
    }

    /// <summary>
    /// Quick method that was added in later to allow for a
    /// indicator to be shown if you were moved by a bat.
    /// </summary>
    public void BatIndicator()
    {
        TextDisplayPanel.SetActive(true);
    }
    
    /// <summary>
    /// Quick method that is used with Invoke to only keep
    /// message on screen for a certain amount of time.
    /// </summary>
    public void CloseBatIndicator()
    {
        TextDisplayPanel.SetActive(false);
    }
}
