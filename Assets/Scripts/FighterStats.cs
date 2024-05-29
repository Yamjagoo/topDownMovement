using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterStats : MonoBehaviour, IComparable
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject healthFill;

    [SerializeField]
    private GameObject healFill;

    [Header("Stats")]
    public float health;
    public float heal;
    public float attack;
    public float defense;
    public float speed;
    public float experience;

    private float startHealth;
    private float startHeal;

    [HideInInspector]
    public int nextActTurn;

    private bool dead = false;

    private Transform healthTransform;
    private Transform healTransform;

    private Vector2 healthScale;
    private Vector2 healScale;

    private float xNewHealthScale;
    private float xNewHealScale;

    private void Start()
    {
        healthTransform = healthFill.GetComponent<RectTransform>();
        healthScale = healthFill.transform.localScale;

        healTransform = healFill.GetComponent<RectTransform>();
        healScale = healFill.transform.localScale;

        startHealth = health;
        startHeal = heal;
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        animator.Play("damage");

        // Set Damage Text

        if (health <= 0)
        {
            dead = true;
            gameObject.tag = "Dead";
            Destroy(healthFill);
            Destroy(gameObject);
        }
        else
        {
            xNewHealthScale = healthScale.x * (health / startHealth);
            healthFill.transform.localScale = new Vector2(xNewHealthScale, healthScale.y);
        }
    }

    public void updateHealFill(float cost)
    {
        heal -= cost;
        xNewHealScale = healScale.x * (heal / startHeal);
        healFill.transform.localScale = new Vector2(xNewHealScale, healScale.y);
    }

    public int CompareTo(object otherStats)
    {
        int nex = nextActTurn.CompareTo(((FighterStats)otherStats).nextActTurn);
        return nex;
    }

    internal void Heal(float healAmount)
    {
        health = Mathf.Min(health + healAmount, startHealth); // Ensure health doesn't exceed startHealth
        xNewHealthScale = healthScale.x * (health / startHealth);
        healthFill.transform.localScale = new Vector2(xNewHealthScale, healthScale.y);
    }

    public bool GetDead()
    {
        return dead;
    }

    public void CalculateNextTurn(int currentTurn)
    {
        nextActTurn = currentTurn + Mathf.FloorToInt(100 / speed);
    }
}
