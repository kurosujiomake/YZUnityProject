using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyScript : MonoBehaviour
{
    
    public EnemyStates State;
    public MovementType moveType;
    public WaypointType wayType;
    [Header("If using waypoints set waypoints below")]
    public Transform[] Waypoints;
    [SerializeField]
    private int curWaypoint;
    public float waypointDistBuffer; //how close the enemy needs to be to the waypoint for it to be considered reached
    public Rigidbody2D rb2d;
    public Animator anim;
    public float interruptThreshold; //the point where the enemy reacts to a hit
    public bool isVulnerable; //can the enemy get hit
    public float speed;
    void Start()
    {
        
    }

    void Update()
    {
        switch(State)
        {
            case EnemyStates.None: //no state, nothing happens here

                break;
            case EnemyStates.Transitions: //do transition stuff like stopping momentum etc here
                rb2d.velocity = Vector2.zero;
                //reset animator variables here too

                break;
            case EnemyStates.Idle: //enemy is not moving or doing anything

                break;
            case EnemyStates.Moving: //standard movement

                break;
            case EnemyStates.Moving2: //if applicable a secondary movement

                break;
            case EnemyStates.Attack: //the attack animation

                break;
            case EnemyStates.Hurt: //the enemy got hurt 

                break;
            case EnemyStates.Death: //the enemy is dying

                break;
        }
    }
    public void GotHurt(float dmg) //this is called from an outside script
    {
        if(dmg >= interruptThreshold) //if the dmg is above the threshold of interruption, it will swap to hurt state
        {
            Transitioning(EnemyStates.Hurt);
        }
    }

    public void Transitioning(EnemyStates followUp)
    {
        State = EnemyStates.Transitions;
        State = followUp;
    }

    void Moving()
    {

    }
    void WaypointMove()
    {
        float s = speed * Time.deltaTime;
        if (curWaypoint < Waypoints.Length)
        {
            if(transform.position.magnitude - Waypoints[curWaypoint].transform.position.magnitude < waypointDistBuffer)
            {
                curWaypoint++; //move to the next waypoint when close enough to current waypoint
            }
        }
        if(curWaypoint == Waypoints.Length)
        {
            curWaypoint = 0;
        }
        VelMoveToTarget(Waypoints[curWaypoint], s); //move along the waypoints
    }

    void VelMoveToTarget(Transform target, float spd) //using this instead of Vector2.movetowards to prevent physics bugs
    {
        float x = transform.position.x;
        float y = transform.position.y;
        Vector2 vel = rb2d.velocity;
        if (x > target.position.x) //object is to the right of the target
        {
            vel.x = -spd;
            rb2d.velocity = vel; //move the object to the left
        }
        if(x < target.position.x) //object is to the left of the target
        {
            vel.x = spd;
            rb2d.velocity = vel; //move the object to the right
        }
        switch(moveType)
        {
            case MovementType.Grounded:
                //do some jump detection here later
                break;
            case MovementType.Flying: //only flying enemies will move vertically towards target
                if (y > target.position.y) //object is above the target
                {
                    vel.y = -spd;
                    rb2d.velocity = vel; //move the object downwards
                }
                if (y < target.position.y) //object is below the target
                {
                    vel.y = spd;
                    rb2d.velocity = vel; //move the object upwards
                }
                break;
        }
        
    }
}

public enum EnemyStates
{
    None,
    Idle,
    Moving,
    Moving2,
    Attack,
    Hurt,
    Death,
    Transitions
}
public enum MovementType
{
    Grounded,
    Flying
}
public enum WaypointType
{
    Platform,
    Waypoint,
    Distance,
    Stationary
}
