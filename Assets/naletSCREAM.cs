using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class naletSCREAM : MonoBehaviour
{
    
    public float timeAtakNow = 0f;
    public float timeAtak = 2f;

    void Update()
    {

        if (timeAtak > timeAtakNow)
        {
            timeAtakNow += 1* Time.deltaTime;
        }else
        {
            timeAtakNow = 0f;
            gameObject.SetActive(false);
        }

    }

    // Start is called before the first frame update
    public void scream()
    {
        
    }

    


}
