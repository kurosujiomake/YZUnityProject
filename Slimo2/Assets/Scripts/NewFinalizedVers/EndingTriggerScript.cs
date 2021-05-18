using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTriggerScript : MonoBehaviour
{
    public GameObject WhiteOutFade;
    public float delayBeforeText = 2f;
    public GameObject EndingTextGroup;
    public GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        WhiteOutFade.SetActive(false);
        EndingTextGroup.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.GetComponent<Parameters>() != null)
        {
            StartCoroutine(Ending());
        }
    }

    IEnumerator Ending()
    {
        GM.SwapGameState(1);
        WhiteOutFade.SetActive(true);
        yield return new WaitForSeconds(delayBeforeText);
        EndingTextGroup.SetActive(true);
        GM.SetReturnToTitle(true);
    }
}
