using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : SingletonBehaviour<MoneyManager>
{
    int currentWealth;
    public void AddMoney(int money)
    {
        currentWealth += money;
    }

    public int GetWealth()
    {
        return currentWealth;
    } 

    public bool SpendMoney(int price)
    {
        if(price > currentWealth)
        {
            return false;
        }

        currentWealth -= price;
        return true;
    }
}
