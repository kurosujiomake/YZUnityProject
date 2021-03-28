using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjFire : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Direction in degrees, this can be set via script/directly/animator or by targeting")]
    public float Direction;
    public Transform Target;
    public bool UseTarget;
    public float angle;
    private bool above = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TargetConversion();
    }

    void TargetConversion()
    {
        if(UseTarget)
        {
            Vector2 tarDir = Target.position - transform.position;
            tarDir = tarDir.normalized;
            float dot = Vector2.Dot(tarDir, transform.right);
            if(Target.position.y > transform.position.y)
            {
                above = true;
            }
            if(Target.position.y < transform.position.y)
            {
                above = false;
            }
            switch(above)
            {
                case true:
                    angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
                    break;
                case false:
                    angle = -1 * (Mathf.Acos(dot) * Mathf.Rad2Deg);
                    break;
            }
            Direction = angle;
        }
    }

    IEnumerable FireProjCycle()
    {
        yield return new WaitForSeconds(0);
    }
}
