using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerCloc : MonoBehaviour
{
    //public TextMeshProUGUI text;
    public int moshrum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        moshrums mushroomsScript = other.GetComponent<moshrums>();
        if (mushroomsScript != null)
        {
            // Обновляем текстовое поле значением touchingObjectsCount
            //text.text = mushroomsScript.touchingObjectsCount.ToString();
            moshrum = mushroomsScript.touchingObjectsCount;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        moshrum = 0;
        //text.text = "0";
    }
    
    private void OnTriggerStay(Collider other)
    {
        moshrums mushroomsScript = other.GetComponent<moshrums>();
        if (mushroomsScript != null)
        {
            // Обновляем текстовое поле значением touchingObjectsCount
            //text.text = mushroomsScript.touchingObjectsCount.ToString();
            moshrum = mushroomsScript.touchingObjectsCount;
        }
    }
    

}
