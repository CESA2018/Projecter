using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    static public void TransScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void TransPlayScene()
    {
        SceneManager.LoadScene("test");
    }
}
