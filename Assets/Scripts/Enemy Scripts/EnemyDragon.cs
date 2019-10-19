using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragon : MonoBehaviour {
    public float speed, bound;
    private bool up;
    private Animator anim;
    private Transform player;

    void Awake() {
        player = GameObject.Find("Assassin").transform;
        anim = GetComponent<Animator>();    
    }

    void Start () {
        bound = transform.position.y;
    }

    void Update () {
        Movement();
    }

    void Movement () {
        if (up) {
            Vector3 pos = transform.position;
            pos.y += speed;
            transform.position = pos;
            if (transform.position.y > bound + 0.13f) {
                up = false;
            }
        } else {
            Vector3 pos = transform.position;
            pos.y -= speed;
            transform.position = pos;
            if (transform.position.y < bound - 0.13f) {
                up = true;
            }
        }
        if (transform.position.x < player.transform.x) {
            transform.localScale = new Vector3(1,1,1);
        }

        if (transform.position.x > player.transform.x) {
            transform.localScale = new Vector3(-1,1,1);
        }
    }
}