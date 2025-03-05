using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class economyBase : MonoBehaviour
{
    [Header("Money ammount")]
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
        currentMoney += Time.deltaTime;
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
}
