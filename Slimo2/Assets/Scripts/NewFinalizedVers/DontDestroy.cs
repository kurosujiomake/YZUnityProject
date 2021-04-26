using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//attach this to anything that you want to persist through different scenes
public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
