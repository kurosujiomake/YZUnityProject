using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyScript : MonoBehaviour
{
    //default states are set in the inspector
    public DefaultStates defaultStates;

    //basic vars and vars related to movement
    [Header("Basic Vars and Movement/States")]
    public EnemyStates State;
    public MovementType moveType;
    public float baseGravityScale; //the grav scale for non-flying enemies
    public WaypointType wayType; //using wat navigation
    public float speed;
    public bool facingRight = false;

    //vars for ground check and edge/wall detection
    [Header("Groundcheck and wall/edge detect")]
    public LayerMask gLayer;
    public float gCheckDist, edgeCheckDist, wallCheckDist; //distance for ground checking
    public bool isGrounded, nearEdge, nearWall; //grounded bool, is near the edge for debugging only
    public Transform[] gCheckPoints; //raycast targets for ground checking
    public Transform[] edgeCheckers; //raycast origins for edge checking
    public Transform[] wallCheckers; //raycast origins for wall checking
    

    //waypoint specific vars
    [Header("If using waypoints set waypoints below")]
    public Transform[] Waypoints;
    [SerializeField]
    private int curWaypoint;
    public float waypointDistBuffer; //how close the enemy needs to be to the waypoint for it to be considered reached
    public float waypointIncrementBuffer; //how far away before the cur waypoints is allowed to increment again
    
    //animation related vars
    public Rigidbody2D rb2d;
    public Animator anim;
    
    //this is the variables that is used in detection of getting hit by the player
    [Header("Related to getting hurt")]
    public float interruptThreshold; //the point where the enemy reacts to a hit
    public bool isVulnerable; //can the enemy get hit
    public float hurtDuration;
    public bool gotHurt;
    private Coroutine hurtLoop;
    private delegate void CallBack();
    private float t2;

    //targetting and attacking vars below
    [Header("Targeting and attacking")]
    public bool hasTarget;
    public Transform targetTrans;

    void Start()
    {
        anim = GetComponent<Animator>();
        State = defaultStates.defaultMain;
        moveType = defaultStates.defaultMove;
        wayType = defaultStates.defaultPathfind;
        rb2d.velocity = Vector2.zero;
    }

    void Update()
    {
        switch(State)
        {
            case EnemyStates.None: //no state, nothing happens here, but useful for debugging/exceptions

                break;
            case EnemyStates.Transitions: //do transition stuff like stopping momentum etc here
                rb2d.velocity = Vector2.zero;
                //reset animator variables here too

                break;
            case EnemyStates.Idle: //enemy is not moving or doing anything
                switch (hasTarget)
                {
                    case true:
                        //do targeting stuff here
                        break;
                    case false:
                        Transitioning(defaultStates.defaultMain); //transition back to watever default you have
                        break;
                }
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
                if (!GroundCheck())
                    Transitioning(EnemyStates.Falling);
                t2 -= Time.deltaTime; //cant use coroutines here due to strange things
                if(t2 <= 0)
                {
                    gotHurt = false;
                    Transitioning(EnemyStates.Idle);
                    anim.SetTrigger("ReturnToMain");
                }
                break;
            case EnemyStates.Falling:
                anim.SetTrigger("GotHurt"); //may or may not need this
                if(GroundCheck()) //has hit the ground
                {
                    Transitioning(EnemyStates.Hurt);
                }
                break;
            case EnemyStates.Death: //the enemy is dying

                break;
        }
        TimerReset();
    }
    private bool GroundCheck()
    {
        bool b = false;
        Vector2 pos1, pos2, pos3; //uses 3 points of detection to reduce edge cases causing problems, a bit overkill
        Vector2 dir = Vector2.down;
        RaycastHit2D hit1, hit2, hit3;
        pos1 = gCheckPoints[0].position;
        pos2 = gCheckPoints[1].position;
        pos3 = gCheckPoints[2].position;
        hit1 = Physics2D.Raycast(pos1, dir, gCheckDist, gLayer);
        hit2 = Physics2D.Raycast(pos2, dir, gCheckDist, gLayer);
        hit3 = Physics2D.Raycast(pos3, dir, gCheckDist, gLayer);
        if(hit1.collider != null || hit2.collider != null || hit3.collider != null)
        {
            b = true;
        }
        return b;
    }
    private bool EdgeCheck()
    {
        bool b = false;
        Vector2 pos1, pos2;
        Vector2 dir1 = Vector2.right;
        Vector2 dir2 = Vector2.left;
        RaycastHit2D hit1, hit2;
        pos1 = edgeCheckers[0].position;
        pos2 = edgeCheckers[1].position;
        hit1 = Physics2D.Raycast(pos1, dir1, edgeCheckDist, gLayer);
        hit2 = Physics2D.Raycast(pos2, dir2, edgeCheckDist, gLayer);
        switch(facingRight)
        {
            case true:
                if(hit1.collider == null)
                {
                    b = true;
                }
                break;
            case false:
                if(hit2.collider == null)
                {
                    b = true;
                }
                break;
        }

        return b;
    }
    private bool WallCheck()
    {
        bool b = false;
        Vector2 pos1, pos2;
        Vector2 dir1 = Vector2.right;
        Vector2 dir2 = Vector2.left;
        RaycastHit2D hit1, hit2;
        pos1 = wallCheckers[0].position;
        pos2 = wallCheckers[1].position;
        hit1 = Physics2D.Raycast(pos1, dir1, wallCheckDist, gLayer);
        hit2 = Physics2D.Raycast(pos2, dir2, wallCheckDist, gLayer);
        switch(facingRight)
        {
            case true:
                if(hit1.collider != null)
                {
                    b = true;
                }
                break;
            case false:
                if(hit2.collider != null)
                {
                    b = true;
                }
                break;
        }
        return b;
    }

    public void GotHurt(float dmg) //this is called from an outside script
    {
        if(dmg >= interruptThreshold) //if the dmg is above the threshold of interruption, it will swap to hurt state
        {
            Transitioning(EnemyStates.Falling);
            gotHurt = true;
        }
    }

    public void Transitioning(EnemyStates followUp) //do not set states directly, use this to prevent issues
    {
        State = EnemyStates.Transitions;
        State = followUp;
    }
    void TimerReset() //cant use coroutines due to now unity stops coroutines, so old fashioned timer is here
    {
        if (State != EnemyStates.Hurt)
            t2 = hurtDuration;
    }

    void Moving() //put the different ways of moving here
    {
        switch(wayType)
        {
            case WaypointType.Distance:

                break;
            case WaypointType.Platform:
                PlatformMove();
                break;
            case WaypointType.Stationary:

                break;
            case WaypointType.Waypoint:
                WaypointMove();
                break;
        }
    }

    void PlatformMove() //this one makes enemies just patrol a platform, and will turn around when encountereing an edge or wall
    {
        Vector2 v = rb2d.velocity;
        float spd = speed * Time.deltaTime;
        switch(facingRight)
        {
            case true:
                v.x = spd;
                rb2d.velocity = v;
                break;
            case false:
                v.x = -spd;
                rb2d.velocity = v;
                break;
        }
        if(EdgeCheck() || WallCheck()) //if the enemy finds a wall/edge, turn around
        {
            facingRight = !facingRight;
            FlipSpriteDirection();
        }
    }

    void WaypointMove() //moving between 2 or more waypoints
    {
        float s = speed * Time.deltaTime;
        
        if(Mathf.Abs(transform.position.magnitude - Waypoints[curWaypoint].transform.position.magnitude) < waypointDistBuffer)
        {
            curWaypoint++; //move to the next waypoint when close enough to current waypoint
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
            case MovementType.Flying: //will just move in a straight line towards the waypoint
                var dir = Vector2.zero;
                dir = target.position - transform.position;
                rb2d.velocity = dir.normalized * spd;
                break;
            case MovementType.Grounded: //will move towards the waypoint but along the ground
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
    void FlipSpriteDirection() //used for non targeted flip
    {
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
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
    Falling,
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

[System.Serializable]
public class DefaultStates
{
    public EnemyStates defaultMain;
    public MovementType defaultMove;
    public WaypointType defaultPathfind;
}
