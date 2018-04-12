using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageChenge : MonoBehaviour {

    [SerializeField]
    private GameObject m_player;

    [SerializeField]
    private GameObject m_respownArea;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            // m_respownArea = GameObject.Find("RespownArea");

            other.transform.position = m_respownArea.transform.position;

            other.gameObject.GetComponent<UnityChanController>().ResetVelocity();

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //m_respownArea = GameObject.Find("RespownArea");

            other.transform.position = m_respownArea.transform.position;

            other.gameObject.GetComponent<UnityChanController>().ResetVelocity();
        }

    }
}
