using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public int health;

    public int damage;

    public float flashTime;

    public GameObject bloodEffect;
    public GameObject dropCoin;
    public GameObject floatPoint;

    private SpriteRenderer sr;

    private Color originColor;

    private PlayerHealth playerHealth;


    // Start is called before the first frame update
    public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Instantiate(dropCoin, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        GameObject gb = Instantiate(floatPoint, transform.position, Quaternion.identity);
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        FlashColor(flashTime);
        health -= damage;
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>().Shake();
    }

    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        sr.color = originColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth != null)
            {
                playerHealth.damagePlayer(damage);
            }
        }
    }
}