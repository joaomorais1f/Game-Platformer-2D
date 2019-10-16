﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    private Animator anim;
    private bool activeTimeToReset;
    private float defaultComboTimer = .2f, currentComboTimer;
    private int combo;
    private float coolDown;
    private int arrowCount;

    public Transform attackPos;
    public LayerMask enemyLayer;

    public float attackRange;
    public int damage;

    public GameObject arrow;
    public Transform arrowPos;

    private bool canShoot;

    void Awake () {
        anim = GetComponent<Animator>();
        arrowCount = 10; //definir quantas flechas pode-se atirar
    }

    void Update () {
        CoolDownTimer();
        ResetComboState();
        SwordAttack();
        BowAttack();
    }

    void BowAttack() {
        if (Input.GetKeyDown(KeyCode.K) && canShoot) {
            if (arrowCount > 0) {
                anim.SetTrigger("Shoot");
            canShoot = false;
            arrowCount--;
            }
        }
    }

    public void ArrowSpawn() {
        Instantiate(arrow, arrowPos.position, Quaternion.identity);
    }

    void SwordAttack () {
        if (Input.GetKeyDown(KeyCode.J) && !anim.GetBool("Jump")) {
            if (combo < 3) {
                anim.SetBool("SwordAttack", true);
                activeTimeToReset = true;
                currentComboTimer = defaultComboTimer;

                Collider2D[] attackEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);

                for (int i = 0; i < attackEnemies.Length; i++) {
                    if (attackEnemies[i].GetComponent<EnemyHealth>().health > 0) {
                        attackEnemies[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                    }
                }

            } else {
                anim.SetBool("SwordAttack", false);
            }
        } else if (Input.GetKeyDown(KeyCode.J) && anim.GetBool("Jump") && coolDown == 0) {
            anim.SetBool("AirAttack", true);
            coolDown = 1;

            Collider2D[] attackEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);

            for (int i = 0; i < attackEnemies.Length; i++) {
                if (attackEnemies[i].GetComponent<EnemyHealth>().health > 0) {
                    attackEnemies[i].GetComponent<EnemyHealth>().TakeDamage(damage + 10);
                }
            }
        }
    }

    void CoolDownTimer() {
        if (coolDown > 0) {
            anim.SetBool("AirAttack", false);
            coolDown -= Time.deltaTime;
        }
        if (coolDown < 0) {
            coolDown = 0;
        }
    }

    public void IncreaseComboNumber () {
        combo++;
    }

    public void ResetCombo () {
        combo = 0;
        canShoot = true;
    }

    void ResetComboState () {
        if (activeTimeToReset) {
            currentComboTimer -= Time.deltaTime;
            if (currentComboTimer <= 0) {
                anim.SetBool("SwordAttack", false);
                activeTimeToReset = false;
                currentComboTimer = defaultComboTimer;
            }
        }
    }
}