using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalKBTest : MonoBehaviour
{
    public float speed = 9;
    public Vector2 vel;
    public Rigidbody2D rigid2D;
    public float angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Test();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            rigid2D.velocity = Vector2.zero;
        }
    }
    void Test()
    {
        float x = angle * Mathf.Deg2Rad;
        vel.x = (Mathf.Cos(x) / speed) * Time.deltaTime * 100f;
        vel.y = (Mathf.Sin(x) / speed) * Time.deltaTime * 100f;
        rigid2D.velocity = vel;
    }
}
