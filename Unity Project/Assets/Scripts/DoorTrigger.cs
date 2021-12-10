// Sean Le wrote all of this code.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WumpusEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private RotationRoom room;

    [SerializeField]
    private Direction d;

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
    /// Once a player (sphere) makes contact with the collider,
    /// the player is reset to the starting position.
    /// And calls the room method DoorTriggers with direction of door.
    /// </summary>
    /// <param name="contact"></param>
    void OnTriggerEnter(Collider contact)
    {
        contact.gameObject.transform.position = Player.StartingLocation;
        this.room.DoorTrigger(d);
    }
}
