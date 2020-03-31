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
    public float maxHealth;
    public float health;
    public float maxMana;
    public float mana;
    public bool alive = true;
    public bool invulnerable = false;
    public GameObject wand;
    private bool facingRight = true;
    private Rigidbody2D rb;
    public GameObject manaBar, healthBar;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManager.i.AddPlayer(this);
        health = maxHealth;
        mana = maxMana;
        UpdateHealthBar();
        UpdateManaBar();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            health -= 0.1f;
            mana -= 0.1f;
            UpdateHealthBar();
            UpdateManaBar();
        }
    }

    void FixedUpdate()
    {
        if (alive && !GameManager.i.IsFrozen())
        {
            Move();
            Look();
            UpdateFacing();
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
        wand.transform.eulerAngles = new Vector3(0, 0, angle);
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

    void OnFire()
    {
        print("Firing!");
    }

    public void FlipChar()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void FaceRight()
    {
        if (facingRight)
            return;
        if(transform.localScale.x < 0)
            FlipChar();
        facingRight = true;
    }

    void FaceLeft()
    {
        if (!facingRight)
            return;
        if(transform.localScale.x > 0)
            FlipChar();
        facingRight = false;
    }

    void UpdateFacing()
    {
        if (move.x > 0 && look.x < 0)
            FaceRight();
        else if (move.x < 0 && look.x > 0)
            FaceLeft();
    }

    void UpdateManaBar()
    {
        manaBar.transform.localScale = new Vector3(mana / maxMana, 1, 1);
    }
    
    void UpdateHealthBar()
    {
        healthBar.transform.localScale = new Vector3(health / maxHealth, 1, 1);
    }
}
