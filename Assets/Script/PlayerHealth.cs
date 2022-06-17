using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;

    private Renderer myRenderer;
    private Animator myAnim;
    private ScreenFlash sf;

    private Rigidbody2D rb2d;
    private PolygonCollider2D pCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.HealthMax = health;
        HealthBar.HealthCurrent = health;
        myRenderer = GetComponent<Renderer>();
        myAnim = GetComponent<Animator>();
        sf = GetComponent<ScreenFlash>();
        rb2d = GetComponent<Rigidbody2D>();
        pCollider2D = GetComponent<PolygonCollider2D>();
    }

    public void damagePlayer(int damage)
    {
        sf.FlashScreen();
        health -= damage;
        if (health < 0) health = 0;
        HealthBar.HealthCurrent = health;
        if (health <= 0)
        {
            rb2d.velocity = new Vector2(0, 0);
            GameController.isGameAlive = false;
            myAnim.SetTrigger("Death");
            Destroy(gameObject, 1f);
        }

        BlinkPlayer(2, 0.1f);
        pCollider2D.enabled = false;
        StartCoroutine(ShowPlayerHitBox(1.5f));
    }

    void BlinkPlayer(int num, float seconds)
    {
        StartCoroutine(DoBlinks(num, seconds));
    }

    IEnumerator DoBlinks(int num, float seconds)
    {
        for (int i = 0; i < num * 2; i++)
        {
            myRenderer.enabled = !myRenderer.enabled;
            yield return new WaitForSeconds(seconds);
        }

        myRenderer.enabled = true;
    }
    
    IEnumerator ShowPlayerHitBox(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if(health > 0)
        {
            pCollider2D.enabled = true;
        }
    }
}