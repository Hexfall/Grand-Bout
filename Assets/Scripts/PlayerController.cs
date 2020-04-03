﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private float moveMult = 1f;
    private Vector2 move;
    private Vector2 look;
    public int id;
    public float maxHealth;
    public float health;
    public float healthRegen = 0f;
    public float maxMana;
    public float mana;
    public float manaRegen = 0f;
    public float baseManaRegen = 0f;
    public bool alive = true;
    public bool invulnerable = false;
    public GameObject wand;
    private bool facingRight = true;
    private Rigidbody2D rb;
    public GameObject manaBar, healthBar, playerSprite, robes;
    private float DamageMult = 1f;
    public bool colorChanging;

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
            SpendMana(.1f);
        }
        
        if (alive && !GameManager.i.IsFrozen())
        {
            UpdateFacing();
        }
    }

    void FixedUpdate()
    {
        if (alive && !GameManager.i.IsFrozen())
        {
            Move();
            Look();
            if (healthRegen != 0)
                Heal(healthRegen * Time.fixedDeltaTime);
            if (manaRegen + baseManaRegen != 0)
                RegainMana((baseManaRegen + manaRegen) * Time.fixedDeltaTime);
        }
    }

    public void Damage(float amount)
    {
        if (invulnerable)
            return;
        health -= amount * DamageMult;
        if (health <= 0)
        {
            health = 0f;
            if (!GameManager.i.invulnerable)
                Kill();
        }
        UpdateHealthBar();
    }

    public void TrueDamage(float amount)
    {
        Damage(amount / DamageMult);
    }

    public void Heal(float amount)
    {
        health += amount;
        health = (health <= maxHealth? health : maxHealth);
        UpdateHealthBar();
    }

    public void SpendMana(float amount)
    {
        if (amount < 0)
        {
            RegainMana(-amount);
            return;
        }
        mana -= amount;
        if (mana < 0)
        {
            TrueDamage(-mana);
            mana = 0;
        }
        UpdateManaBar();
    }

    public void RegainMana(float amount)
    {
        if (amount < 0)
        {
            SpendMana(-amount);
            return;
        }
        mana += amount;
        mana = (mana <= maxMana? mana : maxMana);
        UpdateManaBar();
    }

    public bool CanCast(float cost)
    {
        return cost < mana + health;
    }

    public void Kill()
    {
        //Death Mechanics here.
        //if (GameManager.i.mainMenu)
            Destroy(gameObject);
    }

    void Look() //Face wand in aiming direction.
    {
        if (look == Vector2.zero)
            return;
        float angle = Mathf.Atan2(look.x, look.y) * Mathf.Rad2Deg;
        wand.transform.eulerAngles = new Vector3(0, 0, angle);
        if (colorChanging)
            ColorChange();
    }

    void Move()
    {
        rb.velocity = move * moveSpeed * moveMult;
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

    public void FlipChar()
    {
        playerSprite.transform.localScale = new Vector3(-playerSprite.transform.localScale.x, playerSprite.transform.localScale.y, playerSprite.transform.localScale.z);
    }

    void FaceRight()
    {
        if (facingRight)
            return;
        if(playerSprite.transform.localScale.x < 0)
            FlipChar();
        facingRight = true;
    }

    void FaceLeft()
    {
        if (!facingRight)
            return;
        if(playerSprite.transform.localScale.x > 0)
            FlipChar();
        facingRight = false;
    }

    void UpdateFacing()
    {
        if (move.x > 0 && look.x <= 0)
            FaceRight();
        else if (move.x < 0 && look.x >= 0)
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

    public void SetPlayerColor(Color color)
    {
        robes.GetComponent<SpriteRenderer>().color = color;
    }

    public void MulitplyMovement(float multiplier)
    {
        moveMult *= multiplier;
    }

    public void MultiplyDamage(float multiplier)
    {
        DamageMult *= multiplier;
    }

    public void PassiveSpendMana(float amount)
    {
        manaRegen -= amount;
    }

    public void PassiveRegainMana(float amount)
    {
        manaRegen += amount;
    }

    public void ColorChange()
    {
        float dir = wand.transform.eulerAngles.z;
    }
}
