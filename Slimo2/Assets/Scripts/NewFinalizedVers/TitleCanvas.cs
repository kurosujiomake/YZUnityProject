using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TitleCanvas : MonoBehaviour
{
    public GameManager GM;
    public GameObject[] textSets;
    public GameObject select;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(GM.titleStage)
        {
            case -1:
                textSets[0].SetActive(true);
                textSets[1].SetActive(false);
                break;
            case 0:
                textSets[0].SetActive(false);
                textSets[1].SetActive(true);
                switch(GM.cType)
                {
                    case 0:
                        select.GetComponent<TextMeshProUGUI>().text = "Keyboard";
                        break;
                    case 1:
                        select.GetComponent<TextMeshProUGUI>().text = "Controller";
                        break;
                }
                break;
            case 1:

                break;
        }
    }
}
