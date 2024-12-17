using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChekPoint : MonoBehaviour
{
    public GameObject[] points;
    public GameObject Player;
    // Start is called before the first frame update

    private void Awake()
    {
        Progress.Instance.DownloadProgress();
        //FirstPersonMovement s_g = Player.GetComponent<FirstPersonMovement>();
        //s_g.enabled = false;
        //Player.SetActive(false);
        Player.transform.position = points[Progress.Instance.PlayerInfo.LevelProgress].transform.position;
        Debug.Log("Progress.Instance.PlayerInfo.LevelProgress " + Progress.Instance.PlayerInfo.LevelProgress);
        //Player.SetActive(true);
        //s_g.enabled = true;
    }

    void Start()
    {
        //FirstPersonMovement s_g = Player.GetComponent<FirstPersonMovement>();
        //s_g.enabled = false;
        //Player.transform.position = points[Progress.Instance.PlayerInfo.LevelProgress].transform.position;
        //s_g.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
