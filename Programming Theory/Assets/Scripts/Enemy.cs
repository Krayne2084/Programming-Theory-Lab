using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int startingHealth = 2;
    [SerializeField] float speed = 3.0f;
    [SerializeField] float speedIncrement = 0.1f;
    public Sprite[] enemySprite;
    [SerializeField] float stunTime = 1.5f;
    private int stuns = 0;
    private int m_health;
    public int health
    {
        get
        {
            return m_health;
        }
        private set
        {
            if (value >= 0)
            {
                m_health = value;
            }
        }
    }
    private Transform player;
    private Transform _transform;
    private MainManager main;
    private bool canMove = true;

    private void Awake()
    {
        startingHealth = MainManager.waveHealth;
        health = startingHealth;
        _transform = transform;
        main = FindObjectOfType<MainManager>();
        player = FindObjectOfType<PlayerController>().transform;
        UpdateSprite();
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }
        SeekPlayer();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            main.GameOver();
        }
    }

    private void SeekPlayer()
    {
        _transform.Translate((player.position - _transform.position).normalized * Time.deltaTime * speed);
    }

    public void Damage()
    {
        health--;
        if (!CheckDeath() && health > 0)
        {
            UpdateSprite();
        }
    }

    public void IncreaseSpeed()
    {
        speed += speedIncrement;
    }

    public void Stun()
    {
        canMove = false;
        stuns++;
        StartCoroutine(CO_StunTimer());
        _transform.rotation = Quaternion.identity;
    }

    IEnumerator CO_StunTimer()
    {
        yield return new WaitForSeconds(stunTime);
        if (stuns <= 1)
        {
            canMove = true;
        }
        stuns--;
    }

    void UpdateSprite()
    {
        GetComponent<SpriteRenderer>().sprite = enemySprite[Mathf.Clamp(health - 1, 0, enemySprite.Length - 1)];
    }

    private bool CheckDeath()
    {
        if (health <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        print("COLLISION");
        PlayerController player;
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            player.Kill();
        }
    }
}
