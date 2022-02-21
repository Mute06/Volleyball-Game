using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float hitForce;
    [SerializeField] private Transform startPosition;
    [SerializeField] private float waitToRestartRound = 2f;

    private bool didStopped;
    private Collider2D colliderComponent;
    private FlyTowards flyTowards;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        colliderComponent = GetComponent<Collider2D>();
        flyTowards = GetComponent<FlyTowards>();
        flyTowards.target = startPosition;
        flyTowards.OnReachedTarget += OnReachedStartPoint;


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
        else if (collision.gameObject.CompareTag("Ground"))
        {
            StopBall();
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

            colliderComponent.enabled = false;
            flyTowards.isUsing = true;
            rb.Sleep();
            rb.bodyType = RigidbodyType2D.Kinematic;

            
        }
    }

    private void OnReachedStartPoint()
    {
        colliderComponent.enabled = true;
        didStopped = false;
        StartCoroutine(StartAfterWait());
        GameManager.Instance.characterSwitcher.SwitchToInitialCharacter();
    }
    private IEnumerator StartAfterWait()
    {
        yield return new WaitForSeconds(waitToRestartRound);
        GameManager.Instance.StartRound();
        rb.WakeUp();
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

}
