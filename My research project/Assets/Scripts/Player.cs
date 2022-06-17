/* 16/06/2022
 * NOTES: Initially tried to declare horizontal at class level - not able to use Input.GetAxis there, tried in start but this is not usable in update?
 * referred back to the course, declared the variable as locally as possible, used var as this is short and infers the type.
 * 17/06/2022
 * Want to build in jump system and work on improving the movement system (velocity dampening, desired velocity, max jumps, ground detection etc . . .
 *  . . . Implemented jump counter and max jumps, now need to figure out ground detection - using child object on player's base, intend to use GetComponentInChildren to take collision to determing if
 *  grounded . . . 
 * Going to try use raycast (casts a ray against colliders in the scene) to check if it's hitting the ground layer . . . 
 *  . . . Raycast casts a ray, seems to be used more for scenarios in which you check for objects at a distance, using Physics2D.OverlapCirlce will be rooted to the player's foot and can have a small
 * radius . . .
 *  . . . Tried to use GetComponentInChildren<Collision> but Collision is not a unity component . . .
 *  . . . Got the jump counter working with the ground detector, but if jump is pressed quickly then the first jump isn't registered and an extra jump is bugged in.
 *  Need to add a vertical motion check to prevent extra jump being allowed.
 *  
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _playerSpeed = 5;    // Declaring float value used in player speed calcs.

    [SerializeField] float _jumpMagnitude = 5;
    [SerializeField] float _maxJumpCount = 3;
    [SerializeField] float _jumpCount;
    

    [SerializeField] Transform _feet;           // Declaring the transform to be used as the start point for overlap circle - can be set in the editor.

    Collision _groundDetection; 

    Rigidbody2D _rigidbody2D;                   // Declaring the RigidBody2D with variable name - allows us to reference it at class level (anywhere in this script)

    [SerializeField] bool _grounded;
    int _layerMask;                             // Declare _layerMask variable name to cache the LayerMask (filter to check objects only on specific layers)



    void Start()
    {
        
        _rigidbody2D = GetComponent<Rigidbody2D>();     // Caches the Component of RigidBody into reference - can now be used to alter the components of object's body.
        //_groundDetection = GetComponentInChildren<Collision>();
        _layerMask = LayerMask.GetMask("Default");      // Caches the LayerMask filter into variable
    }

    void Update()
    {
        //Debug.Log($"Grounded check: {_grounded}");
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f);

        checkIfGrounded();

        if (Input.GetButtonDown("Jump") && _jumpCount < _maxJumpCount)
        {
            Debug.Log("Jump button pressed");
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpMagnitude);
            _jumpCount++;
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

    private void checkIfGrounded()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, 0.1f, _layerMask);    // Caches the OverlapCircle (Checks whether a collider falls within a certain radius), using a start position, radius, layerMask (filter).
        //Debug.Log($"{ hit.CompareTag("Player")}");                                                                        // . . . this checks whether a collider of layer "Default" comes into contact with the overlap circle.
        if (hit == null || _rigidbody2D.velocity.y > 0)
        {
            _grounded = false;
            return;
        }
        _grounded = true;
        _jumpCount = 0;
    }
}
