using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    [SerializeField]
    private GameObject m_player;

    [SerializeField]
    private GameObject m_Goal;

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

            SceneTransitionManager.TransScene("Clear");

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //m_respownArea = GameObject.Find("RespownArea");

            SceneTransitionManager.TransScene("Clear");

        }

    }

}
