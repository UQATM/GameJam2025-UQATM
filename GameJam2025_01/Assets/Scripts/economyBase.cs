using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class economyBase : MonoBehaviour
{
    [Header("Money amount")]
    public float currentMoney;
    public float startingMoney;

    [Header("Turret cost")]
    public int numberOfTower = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentMoney = startingMoney;
        currentMoney += numberOfTower;

    }

    // Update is called once per frame
    void Update()
    {
        // Removed time-based money increase.
    }

    public bool checkPrice(int cost)
    {
        Debug.Log(cost);
        Debug.Log(currentMoney);
        if (cost > currentMoney)
        {
            return false;
        }

        currentMoney -= cost;
        return true;
    }

    // Call this method when a regular enemy is killed.
    public void OnEnemyKilled()
    {
        currentMoney += 2;
        Debug.Log("Enemy killed. Money increased by 2. Total money: " + currentMoney);
    }

    // Call this method when a boss is killed.
    public void OnBossKilled()
    {
        currentMoney += 15;
        Debug.Log("Boss killed. Money increased by 15. Total money: " + currentMoney);
    }
}
