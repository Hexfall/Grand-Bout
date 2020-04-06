using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyScript : MonoBehaviour
{
    public int players = 0;
    public int minimumPlayers = 2;
    public GameObject circle;
    private Animator anim;
    public float accel, decel;
    private bool accelBool, decelBool;

    private void OnEnable()
    {
        players = 0;
        anim = circle.GetComponent<Animator>();
        anim.speed = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
            players += 1;
        if (players >= minimumPlayers && players == GameManager.i.PlayerCount())
            GameManager.i.StartGame();
    }

    private void FixedUpdate()
    {
        AdjustSpeed();
    }

    private void AdjustSpeed()
    {
        float targetSpeed = (float) players / (float) GameManager.i.PlayerCount() * 3;
        if (anim.speed < targetSpeed)
            Accelerate(targetSpeed);
        else if (anim.speed > targetSpeed)
            Decelerate(targetSpeed);
    }

    private void Accelerate(float target)
    {
        if (anim.speed + accel * Time.fixedDeltaTime * players > target)
            anim.speed = target;
        else
            anim.speed += accel * Time.fixedDeltaTime * players;
    }

    private void Decelerate(float target)
    {
        if (anim.speed - decel * Time.fixedDeltaTime < target)
            anim.speed = target;
        else
            anim.speed -= accel * Time.fixedDeltaTime;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
            players -= 1;
    }
}
