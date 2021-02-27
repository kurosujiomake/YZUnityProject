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
    public bool facingRight = false;
    public float hurtDuration;
    public bool gotHurt;
    private Coroutine hurtLoop;
    private delegate void CallBack();
    void Start()
    {
        anim = GetComponent<Animator>();
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
                anim.SetBool("IsMoving", true);
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
                anim.SetTrigger("GotHurt");
                hurtLoop = StartCoroutine(Timer(hurtDuration, () => gotHurt = false));
                if(!gotHurt)
                {
                    Transitioning(EnemyStates.Idle);
                    anim.SetTrigger("ReturnToMain");
                }
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
            gotHurt = true;
            if(hurtLoop != null)
            {
                StopCoroutine(hurtLoop);
            }
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
        FlipSpriteDirection(target);
        switch(moveType)
        {
            case MovementType.Flying:
                var dir = Vector2.zero;
                dir = target.position - transform.position;
                rb2d.velocity = dir.normalized * spd;
                break;
            case MovementType.Grounded:
                var a = target.transform.position;
                var b = transform.position;
                Vector2 v = rb2d.velocity;
                if (a.x - b.x > 0) //target is on the right side
                {
                    v.x = spd;
                    rb2d.velocity = v;
                }
                if(a.x - b.x < 0) //target is on the left side
                {
                    v.x = -spd;
                    rb2d.velocity = v;
                }
                break;
        }
        
    }
    void FlipSpriteDirection(Transform target) //flips the sprite around when enemy is supposed to be facing something else
    {
        float a = target.transform.position.x;
        float b = transform.position.x;
        switch(facingRight)
        {
            case true:
                if(a-b < 0 && facingRight)
                {
                    facingRight = !facingRight;
                    GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                }
                break;
            case false:
                if(a-b > 0 && !facingRight)
                {
                    facingRight = !facingRight;
                    GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                }
                break;
        }
    }
    IEnumerator Timer (float dur, CallBack callBack) //a basic timer to switch a bool
    {
        Debug.Log("Started timer");
        yield return new WaitForSeconds(dur);
        Debug.Log("Timer Complete");
        if(callBack != null)
        {
            callBack();
        }
        yield return null;
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
