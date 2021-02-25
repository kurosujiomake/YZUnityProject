using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyScript : MonoBehaviour
{
    
    public EnemyStates State;
    public MovementType moveType;
    public float baseGravityScale; //the grav scale for non-flying enemies
    public WaypointType wayType;
    [Header("If using waypoints set waypoints below")]
    public Transform[] Waypoints;
    [SerializeField]
    private int curWaypoint;
    public float waypointDistBuffer; //how close the enemy needs to be to the waypoint for it to be considered reached
    public float waypointIncrementBuffer; //how far away before the cur waypoints is allowed to increment again
    private bool hasIncremented;
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
                Moving();
                switch(moveType)
                {
                    case MovementType.Grounded:
                        rb2d.gravityScale = baseGravityScale;
                        break;
                    case MovementType.Flying:
                        rb2d.gravityScale = 0; //flying enemies are unaffected by gravity unless hurt
                        break;
                }
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
        switch(wayType)
        {
            case WaypointType.Distance:

                break;
            case WaypointType.Platform:

                break;
            case WaypointType.Stationary:

                break;
            case WaypointType.Waypoint:
                WaypointMove();
                break;
        }
    }
    void WaypointMove()
    {
        float s = speed * Time.deltaTime;
        
        if(Mathf.Abs(transform.position.magnitude - Waypoints[curWaypoint].transform.position.magnitude) < waypointDistBuffer)
        {
            curWaypoint++; //move to the next waypoint when close enough to current waypoint
            hasIncremented = true;
        }
        if(curWaypoint == Waypoints.Length)
        {
            curWaypoint = 0;
        }
        ForceMoveToTarget(Waypoints[curWaypoint], s);
        
        
    }
    void ForceMoveToTarget(Transform target, float spd)
    {
        var dir = Vector2.zero;
        dir = target.position - transform.position;
        rb2d.velocity = dir.normalized * spd;
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
