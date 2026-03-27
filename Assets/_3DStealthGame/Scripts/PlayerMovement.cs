using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    Animator m_Animator;

    public InputAction MoveAction;
    public float walkSpeed = 1.0f;
    public float turnSpeed = 20f;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        MoveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        m_Animator = GetComponent<Animator>();
        Rigidbody m_Rigidbody = GetComponent<Rigidbody> ();
        MoveAction.Enable();
    }
    void FixedUpdate()
    {
        var pos = MoveAction.ReadValue<Vector2>();
        float horizontal = pos.x;
        float vertical = pos.y;
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();
        Vector3 desiredForwad = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForwad);
        m_Rigidbody.MoveRotation(m_Rotation);
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * walkSpeed * Time.deltaTime);
        bool hashorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWaalking = hashorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWaalking);
    }
}


