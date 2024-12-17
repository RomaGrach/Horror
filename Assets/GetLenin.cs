using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLenin : MonoBehaviour
{
    public GameObject emp;
    public GameObject linin;
    public bool lenin = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetLeninFunc()
    {
        emp.SetActive(false);
        linin.SetActive(true);
        lenin = true;
    }

    public void DropLeninFunc()
    {
        emp.SetActive(true);
        linin.SetActive(false);
    }
}
