using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    public float speed;
    public float startWaitTime;
    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;
    
    private float waitTime;


    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
        waitTime = startWaitTime;
        movePos.position = GetRandomPos();
    }

    // Update is called once per frame
    public new void Update()
    {
        base.Update();
        transform.position = Vector3.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePos.position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                movePos.position = GetRandomPos();
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    Vector3 GetRandomPos()
    {
        var leftPosition = leftDownPos.position;
        var rightPosition = rightUpPos.position;
        Vector3 randomPos = new Vector3(Random.Range(leftPosition.x, rightPosition.x),
            Random.Range(leftPosition.y, rightPosition.y));
        return randomPos;
    }
}