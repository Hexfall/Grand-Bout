using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 move;
    private Vector2 look;
    public int id;
    public float health;
    public float mana;
    public bool alive = true;
    public bool invulnerable = false;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManager.i.AddPlayer(this);
    }

    void OnDestroy()
    {
        print("thing");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (alive)
        {
            Move();
            Look();
        }
    }

    public void Damage(float amount)
    {
        if (invulnerable)
            return;
        health -= amount;
        if (health <= 0)
        {
            health = 0f;
            if (!GameManager.i.invulnerable)
                Kill();
        }
    }

    public void Kill()
    {
        //Death Mechanics here.
        if (GameManager.i.mainMenu)
            Destroy(gameObject);
    }

    void Look() //Face player in looking direction.
    {
        if (look == Vector2.zero)
            return;
        float angle = Mathf.Atan2(look.x, look.y) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    void Move()
    {
        rb.velocity = move * moveSpeed;
    }

    void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        look = value.Get<Vector2>();
        look.x = -look.x;
    }

    void OnPlayerJoined()
    {
        print("Player Joined!");
    }
}
