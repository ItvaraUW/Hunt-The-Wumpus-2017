// Sean Le wrote all of this code.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WumpusEngine;

public class ShootArrowButton : MonoBehaviour
{
    private GameControl control = GameControl.GetMaintainedInstance();
    public Direction dir;

    [SerializeField]
    private Button arrowButton;

    // Use this for initialization
    /// <summary>
    /// Nothing here.
    /// </summary>
    void Start ()
    {
		
	}
	
	// Update is called once per frame
    /// <summary>
    /// Nothing here.
    /// </summary>
	void Update ()
    {
		
	}

    /// <summary>
    /// Small method that is required because of direction dictionary system.
    /// fires arrow at corresponding direction.
    /// </summary>
    public void ShootingArrow()
    {
        control.ShootArrow(dir);
    }
}
