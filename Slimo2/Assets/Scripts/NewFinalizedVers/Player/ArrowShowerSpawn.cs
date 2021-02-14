using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShowerSpawn : MonoBehaviour
{
    GameObject proj;
    public DmgBloc sBloc;
    int projKB, projCount;
    float fireDelay, projSpd, projDur, randx, randy;
    int[] HitIDs = new int[2];
    int IDRandomizer()
    {
        int i = 0;
        bool hasSetID = false;
        while (!hasSetID)
        {
            int j = Random.Range(0, 100);
            if (j != HitIDs[0] && j != HitIDs[1])
            {
                HitIDs[0] = HitIDs[1];
                HitIDs[1] = j;
                i = j;
                hasSetID = true;
            }
        }
        return i;
    }
    Vector3 RandomizeProjSpawn(Vector3 _origin)
    {
        Vector3 v = new Vector3(0, 0, 0);
        v.x = Random.Range(-randx, randx) + _origin.x;
        v.y = Random.Range(-randy, randy) + _origin.y;
        return v;
    }
    public void GetParameters(GameObject _proj, int _projKB, int _projCount, 
        float _randx, float _randy, float _fireDelay, float _projSpd, float _projDur, DmgBloc _bloc) //theres gotta be a easier way to pass the parameters
    {
        proj = _proj;
        projKB = _projKB;
        projCount = _projCount;
        randx = _randx;
        randy = _randy;
        fireDelay = _fireDelay;
        projSpd = _projSpd;
        projDur = _projDur;
        sBloc = _bloc;
        StartCoroutine(SpawnArrows());
    }

    IEnumerator SpawnArrows()
    {
        bool b = true;
        int i = 0, im = projCount;
        while(b)
        {
            float spd = projSpd, d = 0, pd = 0; //d is traveling direction, pd is the direction the proj faces
            pd = (270 + (Random.Range(-13, 13)));
            d = pd * Mathf.Deg2Rad;
            GameObject clone = Instantiate(proj, RandomizeProjSpawn(transform.position), Quaternion.Euler(0, 0, pd));
            var c = clone.GetComponent<KBInfoPass>();
            c.Hit_ID = IDRandomizer();
            c.curKBNum = projKB;
            c.StartProjTimer(projDur);
            clone.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(d) * projSpd, Mathf.Sin(d) * projSpd);
            clone.GetComponent<DamageTransfer>().dmgData.SetValues(sBloc.dmgToPass, sBloc.isCrit, sBloc.eleMod, sBloc.hitCount);
            yield return new WaitForSeconds(fireDelay);
            i++;
            if(i > im)
            {
                b = false;
                Destroy(gameObject);
            }
        }
    }
    

}
