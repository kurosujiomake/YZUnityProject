using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageTakeCycles : MonoBehaviour
{
    public DamageReciever DR = null;
    public PlayerControllerNew pCN = null;
    // Start is called before the first frame update
    void Start()
    {
        if(DR == null)
        {
            DR = GetComponent<DamageReciever>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D (Collider2D collider)
    {
        if(collider.GetComponent<KBInfoPass>() != null)
        {
            var kb = collider.GetComponent<KBInfoPass>();
            if(kb.SourceID == 1 || kb.SourceID == 3) //player only takes damage from these sources: 1 = enemy, 3 = all
            {

            }
        }
    }
}
