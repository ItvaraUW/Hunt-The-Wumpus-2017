// Sean Le wrote all of this code.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static readonly Vector3 StartingLocation = new Vector3(0f, 1.5f, 0f);

    public RigidbodyConstraints movementConstraints;

    [SerializeField]
    private Rigidbody body;

    // Use this for initialization
    /// <summary>
    /// Nothing here.
    /// </summary>
    void Start()
    {

    }

    // Update is called once per frame
    /// <summary>
    /// Nothing here.
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// Method to reset the starting postion of the player object.
    /// </summary>
    public void ResetPosition()
    {
        this.transform.position = Player.StartingLocation;
    }

    /// <summary>
    /// Sets the RigidbodyConstraints to FreezePosition
    /// to stop ball from rolling during menus.
    /// </summary>
    public void FreezeBall()
    {
        movementConstraints = RigidbodyConstraints.FreezePosition;
        body.constraints = movementConstraints;
    }

    /// <summary>
    /// Sets the RigidbodyConstraints to None
    /// to unfreeze the ball after menus are over.
    /// </summary>
    public void UnfreezeBall()
    {
        movementConstraints = RigidbodyConstraints.None;
        body.constraints = movementConstraints;
    }
}
