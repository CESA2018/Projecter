using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_gameObjects;

	// Use this for initialization
	void Start ()
    {
        foreach (var gameObject in m_gameObjects)
        {
            SceneTransitionManager.LoadGameObject(gameObject.name);
        }

    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            foreach (var gameObject in m_gameObjects)
            {
                SceneTransitionManager.SaveGameObject(gameObject.name);
            }
            SceneTransitionManager.TransScene("test2");
        }
	}
}
