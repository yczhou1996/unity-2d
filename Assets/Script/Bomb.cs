using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionRange;
    public Vector2 startSpeed;
    public float delayTime;
    public float hitBoxTime;
    public float destroyBombTime;

    private Rigidbody2D rigidbody2D;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rigidbody2D.velocity = transform.right * startSpeed.x + transform.up * startSpeed.y;
        
        Invoke("Explode", delayTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenExplosionRange()
    {
        Instantiate(explosionRange, transform.position, Quaternion.identity);
    }

    void Explode()
    {
        anim.SetTrigger("Explode");
        Invoke("DestroyBomb", destroyBombTime);
        Invoke("GenExplosionRange", hitBoxTime);
    }

    void DestroyBomb()
    {
        Destroy(gameObject);
    }
}
