using UnityEngine;

public class TrapPlatform : MonoBehaviour
{
    private Animator anim;

    private BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }


    void DisableBoxCollider2D()
    {
        boxCollider2D.enabled = false;
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            anim.SetTrigger("Collapse");
        }
    }
}