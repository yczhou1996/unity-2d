using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float startTime;
    public float endTime;
    private Animator anim;
    private PolygonCollider2D polygonCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            //polygonCollider2D.enabled = true;
            anim.SetTrigger("Attack");
            //anim.SetFloat("Blend", GameController.Anim.Attack);
            StartCoroutine(startAttack());
        }
    }

    IEnumerator startAttack()
    {
        yield return new WaitForSeconds(startTime);
        polygonCollider2D.enabled = true;
        StartCoroutine(disableHixBox());
    }

    IEnumerator disableHixBox()
    {
        yield return new WaitForSeconds(endTime);
        polygonCollider2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}