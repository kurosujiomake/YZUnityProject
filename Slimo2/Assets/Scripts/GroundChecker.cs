using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Transform edgeGroundDetect1 = null;
    [SerializeField] private Transform edgeGroundDetect2 = null;
    [SerializeField] private float gCheckDist = 0;
    [SerializeField] private LayerMask gLayer; 
    [SerializeField] private bool debugMode = false;
    [SerializeField] private Transform[] slamAtkDetect = null;
    [SerializeField] private float slamAtkDist = 0;
    private bool IsGrounded1()
    {
        Vector2 position = edgeGroundDetect1.transform.position;
        Vector2 direction = Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, gCheckDist, gLayer);

        if (hit.collider != null)
        {
            if (debugMode)
            {
                Debug.DrawRay(position, direction, Color.green);

            }
            return true;
        }
        else
        {
            if (debugMode)
            {
                Debug.DrawRay(position, direction, Color.red);

            }
            return false;
        }
    }
    private bool IsGrounded2()
    {
        Vector2 position = edgeGroundDetect2.transform.position;
        Vector2 direction = Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, gCheckDist, gLayer);

        if (hit.collider != null)
        {
            if (debugMode)
            {
                Debug.DrawRay(position, direction, Color.green);

            }
            return true;
        }
        else
        {
            if (debugMode)
            {
                Debug.DrawRay(position, direction, Color.red);

            }
            return false;
        }
    }
    private bool IsGrounded3()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, gCheckDist, gLayer);

        if (hit.collider != null)
        {
            if (debugMode)
            {
                Debug.DrawRay(position, direction, Color.green);

            }
            return true;
        }
        else
        {
            if (debugMode)
            {
                Debug.DrawRay(position, direction, Color.red);

            }
            return false;
        }
    }
    private bool IsSlamGrounded()
    {
        Vector2 position = slamAtkDetect[0].position;
        Vector2 dir = Vector2.down;
        RaycastHit2D h = Physics2D.Raycast(position, dir, slamAtkDist, gLayer);
        if(h.collider != null)
        {
            if (debugMode)
            {
                Debug.DrawRay(position, dir, Color.green);

            }
            return true;
        }
        else
        {
            if (debugMode)
            {
                Debug.DrawRay(position, dir, Color.green);

            }
            return false;
        }
    }
    private bool IsSlamGrounded2()
    {
        Vector2 position = slamAtkDetect[1].position;
        Vector2 dir = Vector2.down;
        RaycastHit2D h = Physics2D.Raycast(position, dir, slamAtkDist, gLayer);
        if (h.collider != null)
        {
            if (debugMode)
            {
                Debug.DrawRay(position, dir, Color.green);

            }
            return true;
        }
        else
        {
            if (debugMode)
            {
                Debug.DrawRay(position, dir, Color.green);

            }
            return false;
        }
    }
    public bool ReturnGroundCheck()
    {
        if(IsGrounded1() || IsGrounded2() || IsGrounded3())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool ReturnSlamGroundDetect()
    {
        if(IsSlamGrounded() || IsSlamGrounded2())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
