using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    private bool hit = true;
    public GameObject flash;
    public SpriteRenderer playerSpr;
    public Color colliderColor, colliderColor2;
    public AudioClip axeHit;

    void Update()
    {
        if (health < 1) {
            print("Morreu");
        }
    }

    public void TakeDamage (int damage)
    {
        if (hit) {
        StartCoroutine(PlayerHit());
        health -= damage;
        }
    }

    void OnTriggerEnter2D(Collider2D target) {

        if (target.tag == "FireBall") {
            TakeDamage(15);
            Destroy(target.gameObject);
        }
        
    }

    IEnumerator PlayerFlash() {
        for (int i = 0; i < 2; i++) {
            playerSpr.color = colliderColor;
            yield return new WaitForSeconds(.1f);
        }

        for (int i = 0; i < 4; i++) {
            playerSpr.color = colliderColor2;
            yield return new WaitForSeconds(.1f);
            playerSpr.color = Color.white;
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator PlayerHit() {
        SoundManager.instance.PlaySoundFx(axeHit, .2f);
        FindObjectOfType<CameraShake>().ShakeItMedium();
        flash.SetActive(true);
        hit = false;
        StartCoroutine(PlayerFlash());
        yield return new  WaitForSeconds(1.3f);
        hit = true;
        flash.SetActive(false);
    }

}
