using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int health = 100;


    void Start()
    {
        
    }

    void Update()
    {
        if (health < 1) {
            print("Morreu");
        }
    }

    public void TakeDamage (int damage)
    {
        health -= damage;
    }
}
