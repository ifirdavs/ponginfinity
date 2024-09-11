using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float movementSpeedRaise;
    [SerializeField] private ParticleSystem explosionEffect;

    private float movementSpeedX;
    private float movementSpeedY;

    private float minBaseSpeed = 5f;
    private float maxBaseSpeed = 7f;

    void Update()
    {
        transform.Translate(transform.right * movementSpeedX * Time.deltaTime + transform.up * movementSpeedY * Time.deltaTime);
    }

    public void AddSpeed()
    {
        movementSpeedX = (Random.Range(0, 2) * 2 - 1) * Random.Range(minBaseSpeed, maxBaseSpeed);
        movementSpeedY = (Random.Range(0, 2) * 2 - 1) * Random.Range(minBaseSpeed, maxBaseSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Paddle")
        {
            movementSpeedX = -(movementSpeedX + Mathf.Sign(movementSpeedX) * movementSpeedRaise);
            movementSpeedY = movementSpeedY + Mathf.Sign(movementSpeedY) * movementSpeedRaise;
        }
        else if (collision.gameObject.tag == "Wall")
        {
            movementSpeedY = -movementSpeedY;
        }
        else if (collision.gameObject.tag == "LeftGoal")
        {
            movementSpeedX = 0;
            movementSpeedY = 0;
            CreateExplosion();
            GameManager.GetInstance().IncreasePlayerTwoScore();
        }
        else if (collision.gameObject.tag == "RightGoal")
        {
            movementSpeedX = 0;
            movementSpeedY = 0;
            CreateExplosion();
            GameManager.GetInstance().IncreasePlayerOneScore();
        }
    }

    private void CreateExplosion()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        explosionEffect.Play();
    }
}
