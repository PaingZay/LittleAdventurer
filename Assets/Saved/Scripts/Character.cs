using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController _cc;

    [SerializeField]
    public float moveSpeed = 5f;

    private Vector3 movementVelocity;

    private PlayerInput plyerInput;

    private float _verticalVelocity;

    [SerializeField]
    public float gravity = -9.8f;

    private Animator _animator;

    public bool IsPlayer = true;
    private UnityEngine.AI.NavMeshAgent _navMeshAgent;
    private Transform TargetPlayer;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
        //plyerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();

        if(!IsPlayer)
        {
            _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            TargetPlayer = GameObject.FindWithTag("Player").transform;
            _navMeshAgent.speed = moveSpeed;
        }
        else
        {
            plyerInput = GetComponent<PlayerInput>();
            /*Move to else block because We only want to get the PlayerInput script if this is a player.
             And we don't have a PlayerInput script attached to an enemy */
        }
    }

    /*  The basic logic here is if the distance between the enemy and player is greater than the stopping distance,
        we want to keep moving the enemy toward the player */
    private void CalculateEnemyMovement()
    {
        if(Vector3.Distance(TargetPlayer.position, transform.position) >= _navMeshAgent.stoppingDistance)
        {
            _navMeshAgent.SetDestination(TargetPlayer.position);
            _animator.SetFloat("Speed", 0.2f);
        }
        else
        {
            _navMeshAgent.SetDestination(transform.position);
            _animator.SetFloat("Speed", 0f);
        }
    }

    private void CalculatePlayerMovement()
    {

        movementVelocity.Set(plyerInput.horizontalInput, 0f, plyerInput.verticalInput);//set parameters to Vector3 instance in order to move and rotate
        movementVelocity.Normalize();//Normalize speed preventing the user pressing 2 movement keys at the same time
        movementVelocity = Quaternion.Euler(0, -45f, 0) * movementVelocity;//45 degree as forward

        _animator.SetFloat("Speed", movementVelocity.magnitude);

        movementVelocity *= moveSpeed * Time.deltaTime;//Standardize Speed

        if (movementVelocity != Vector3.zero)//Prevent player object resetting default direction
            transform.rotation = Quaternion.LookRotation(movementVelocity);


        _animator.SetBool("AirBorne", !_cc.isGrounded);
        /*
         The code is tracking whether the character is in the air or on the ground,
         and when the character is not on the ground (in the air), it sets the "AirBorne" parameter to true.
         Since !_cc.isGrounded is true when the character is in the air, setting "AirBorne" to true accurately reflects
         the character's state.
         */


        //Another way of moving the player object
        //Vector3 inputDirection = new Vector3(plyerInput.horizontalInput, 0, plyerInput.verticalInput);
        //if (inputDirection != Vector3.zero)
        //{
        //    Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
        //    transform.rotation = targetRotation;
        //}
    }

    private void FixedUpdate() 
    {
        if(IsPlayer)
        {
            CalculatePlayerMovement();

            if (_cc.isGrounded == false)
            {
                _verticalVelocity = gravity;
            }
            else
            {
                _verticalVelocity = gravity * 0.3f;
            }

            movementVelocity += _verticalVelocity * Vector3.up * Time.deltaTime;

            _cc.Move(movementVelocity);
        }
        else
        {
            CalculateEnemyMovement();
        }
    }
}
