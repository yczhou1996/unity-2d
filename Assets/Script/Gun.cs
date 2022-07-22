using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Camera cam;
    public GameObject bulletPrefab;
    public Transform muzzleTransform;
    
    private Vector3 mousePos;

    private Vector2 gunDirection;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        gunDirection = (mousePos - transform.position).normalized;
        float angel = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angel);

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPrefab, muzzleTransform.position, Quaternion.Euler(transform.eulerAngles));
        }
    }
}