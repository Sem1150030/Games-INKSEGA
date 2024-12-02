using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public PlayerController2D playerStats;
    public BulletHit bulletStats;
    public ShootScript shootScript;
    [SerializeField] private TMP_Text SpeedQuantity;
    [SerializeField] private TMP_Text DamageQuantity;
    [SerializeField] private TMP_Text HealthQuantity;
    [SerializeField] private TMP_Text FireRateQuantity;

    public void BuyHealthUpgrade()
    {
        if (playerStats.goldAmount >= playerStats.healthUpgradeCost && 
            playerStats.currentHealthUpgrades < playerStats.maxHealthUpgrades)
        {
            playerStats.goldAmount -= playerStats.healthUpgradeCost;
            playerStats.maxHealth += 20; // Increase max health
            playerStats.health += 20; // Increase health
            playerStats.currentHealthUpgrades++;
            HealthQuantity.text = playerStats.currentHealthUpgrades.ToString() + "/" + playerStats.maxHealthUpgrades.ToString();
            Debug.Log("Health upgraded!");
        }
        else
        {
            Debug.Log("Cannot buy health upgrade.");
        }
    }

    public void BuySpeedUpgrade()
    {
        if (playerStats.goldAmount >= playerStats.speedUpgradeCost && 
            playerStats.currentSpeedUpgrades < playerStats.maxSpeedUpgrades)
        {
            playerStats.goldAmount -= playerStats.speedUpgradeCost;
            playerStats.speed += 1f; // Increase speed
            playerStats.currentSpeedUpgrades++;
            SpeedQuantity.text = playerStats.currentSpeedUpgrades.ToString() + "/" + playerStats.maxSpeedUpgrades.ToString();
            Debug.Log("Speed upgraded!");
        }
        else
        {
            Debug.Log("Cannot buy speed upgrade.");
        }
    }

    public void BuyDamageUpgrade()
    {
        if (playerStats.goldAmount >= playerStats.damageUpgradeCost && 
            playerStats.currentDamageUpgrades < playerStats.maxDamageUpgrades)
        {
            playerStats.goldAmount -= playerStats.damageUpgradeCost;
            bulletStats.damage += 10; // Increase damage
            playerStats.currentDamageUpgrades++;
            DamageQuantity.text = playerStats.currentDamageUpgrades.ToString() + "/" + playerStats.maxDamageUpgrades.ToString();
            Debug.Log("Damage upgraded!");
        }
        else
        {
            Debug.Log("Cannot buy damage upgrade.");
        }
    }
    
    public void BuyFireRateUpgrade()
    {
        if (playerStats.goldAmount >= playerStats.damageUpgradeCost && 
            playerStats.currentDamageUpgrades < playerStats.maxDamageUpgrades)
        {
            playerStats.goldAmount -= playerStats.fireRateUpgradeCost;
            shootScript.fireRate += 1; // Increase damage
            playerStats.currentFireRateUpgrades++;
            // FireRateQuantity.text = playerStats.currentFireRateUpgrades.ToString() + "/" + playerStats.maxFireRateUpgrades.ToString();
            Debug.Log("Damage upgraded!");
        }
        else
        {
            Debug.Log("Cannot buy damage upgrade.");
        }
    }
}
