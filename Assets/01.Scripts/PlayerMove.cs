using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;
    PlayerAction actions;
    Vector2 move;

    [SerializeField] private float speed = 5.0f;

    // Start is called before the first frame update
    void Awake()
    {
        actions = new PlayerAction();
        rb = GetComponent<Rigidbody>();
        actions.Player.Move.performed += context => SendMessage(context.ReadValue<Vector2>());

        actions.Player.Move.performed += context => move = context.ReadValue<Vector2>();
        actions.Player.Move.canceled += context => move = Vector2.zero;
    }

    private void OnEnable()
    {
        actions.Player.Enable();
    }

    private void OnDisable()
    {
        actions.Player.Disable();
    }

    

    void SendMessage(Vector2 coordinates)
    {
        Debug.Log("Thumb-stick coordinates = " + coordinates);
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(move.x, 0.0f, move.y) * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }



}
