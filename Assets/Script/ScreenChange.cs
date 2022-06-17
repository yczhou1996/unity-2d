using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenChange : MonoBehaviour
{

    public GameObject img1;

    public GameObject img2;

    private Animator anim;

    public float time;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            anim.SetBool("ChangeToWhite", true);
            Invoke("ChangeImg", time);
        }
    }

    void ChangeImg()
    {
        img1.SetActive(false);
        img2.SetActive(true);
    }
}