/* 16/06/2022
 * NOTES: Initially tried to declare horizontal at class level - not able to use Input.GetAxis there, tried in start but this is not usable in update?
 * referred back to the course, declared the variable as locally as possible, used var as this is short and infers the type.
 * 17/06/2022
 * Want to build in jump system and work on improving the movement system (velocity dampening, desired velocity, max jumps, ground detection etc . . .
 * 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _playerSpeed = 5;    // Declaring float value used in player speed calcs.
    [SerializeField] float _jumpMagnitude = 5;

    Rigidbody2D _rigidbody2D;                   // Declaring the RigidBody2D with variable name - allows us to reference it at class level (anywhere in this script).


    void Start()
    {

        _rigidbody2D = GetComponent<Rigidbody2D>();     // Caches the GetComponent of RigidBody into reference - can now be used to alter the components of object's body.

    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump button pressed");
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpMagnitude);
        }


        //float horizontal = Input.GetAxis("Horizontal");
        var horizontal = Input.GetAxis("Horizontal") * _playerSpeed;               // var allows the compiler to choose which type of value is used.

        if (horizontal < 0)
        {
            _rigidbody2D.velocity = new Vector2(horizontal, _rigidbody2D.velocity.y);       // Sets the velocity of the object's rigidbody to a new 2d Vector.
            Debug.Log($"Horizontal negative value detected. horizontal: {horizontal}");     // Sends to console that negative horizontal motion detected.
        }
        if (horizontal > 0)
        {
            _rigidbody2D.velocity = new Vector2(horizontal, _rigidbody2D.velocity.y);       // Sets the velocity of the object's rigidbody to a new 2d Vector.
            Debug.Log($"Horizontal positive value detected. horizontal: {horizontal},");    // Sends to console that positive horizontal motion detected.
        }
    }
}
