using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float hitForce;


    private bool didStopped;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
            var direction = - (collision.GetContact(0).point - rb.position).normalized;
            float force = collision.collider.GetComponent<Character>().hitForce;
            ApplyBounceForce(direction, force);
            Debug.Log("Aplied force" + direction);
        }
        else
        {
            //rb.velocity = Vector2.zero;
            var direction = -(collision.GetContact(0).point - rb.position).normalized;
            ApplyBounceForce(direction , hitForce);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.isGameStarted)
        {
            if (!didStopped && rb.velocity.magnitude <= 0.1f)
            {
                //StopBall();
            }
        }
    }

    private void ApplyBounceForce(Vector2 direction , float forceFactor)
    {
        rb.AddForce(direction * forceFactor, ForceMode2D.Force);
    }

    private void StopBall()
    {
        if (!didStopped)
        {
            Debug.Log("Ball Stopped");
            didStopped = true;

            // Stop the ball
            rb.velocity = Vector3.zero;

            // Restart the round
            GameManager.Instance.RestartRound();
        }
    }
}
