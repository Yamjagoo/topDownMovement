using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScript : MonoBehaviour
{
    public GameObject owner;

    [SerializeField]
    private string animationName;

    [SerializeField]
    private bool heal;

    [SerializeField]
    private float healCost;

    [SerializeField]
    private float minAttackMultiplier;

    [SerializeField]
    private float maxAttackMultiplier;

    [SerializeField]
    private float minDefenseMultiplier;

    [SerializeField]
    private float maxDefenseMultiplier;

    private FighterStats attackerStats;
    private FighterStats targetStats;
    private float damage = 0.0f;

    // Method to perform attack
    public void Attack(GameObject victim)
    {
        attackerStats = owner.GetComponent<FighterStats>();
        targetStats = victim.GetComponent<FighterStats>();

        if (attackerStats.heal >= healCost)
        {
            float multiplier = UnityEngine.Random.Range(minAttackMultiplier, maxAttackMultiplier);
            damage = multiplier * attackerStats.attack;

            float defenseMultiplier = UnityEngine.Random.Range(minDefenseMultiplier, maxDefenseMultiplier);
            damage = Mathf.Max(0, damage - (defenseMultiplier * targetStats.defense));

            owner.GetComponent<Animator>().Play(animationName);
            targetStats.ReceiveDamage(damage);
        }
    }

    // Method to perform healing
    public void Heal(GameObject hero)
    {
        attackerStats = owner.GetComponent<FighterStats>();

        // Check if the owner has enough resources (heal points) to perform healing
        if (attackerStats.heal > healCost)
        {
            // Calculate the amount of healing based on various factors
            float healAmount = CalculateHealAmount();

            // Apply healing to the owner's health
            attackerStats.Heal(healAmount);

            // Subtract the heal cost from the owner's heal points
            attackerStats.updateHealFill(healCost);

            // Log the healing action
            Debug.Log("Character Healed for " + healAmount + " HP.");
        }
        else
        {
            // Log a message if the owner does not have enough resources to heal
            Debug.Log("Not enough resources to perform healing.");
        }
    }

    internal void SelectAttack(string attackType)
    {
        throw new System.NotImplementedException();
    }

    // Method to calculate the amount of healing
    private float CalculateHealAmount()
    {
        // Here you can implement your logic to calculate the amount of healing based on various factors
        // For demonstration purposes, let's assume a fixed heal amount for now
        return Random.Range(10f, 20f); // Random heal amount between 10 and 20 HP
    }
}
