using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool isPlayerTwo;
    [SerializeField] private Transform ball;

    private float maximumYPosition = 4.2f;
    private Vector2 movementVector;

    private bool isVSPlayer;

    private void Start()
    {
        isVSPlayer = GameManager.GetInstance().isVSPlayer;
        if (isPlayerTwo)
        {
            if (isVSPlayer)
            {
                GetComponent<SpriteRenderer>().color = new Color(112f / 255f, 187f / 255f, 1);
            }
            else
            {
                movementSpeed = 6f;
            }
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        movementVector = context.ReadValue<Vector2>().normalized;
    }

    void Update()
    {
        float deltaY;
        if (!isVSPlayer && isPlayerTwo)
        {
            deltaY = Mathf.Sign(ball.transform.position.y - transform.position.y) * movementSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + deltaY, -maximumYPosition, maximumYPosition), transform.position.z);
        }
        else
        {
            deltaY = movementVector.y * movementSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + deltaY, -maximumYPosition, maximumYPosition), transform.position.z);
        }
    }
}
