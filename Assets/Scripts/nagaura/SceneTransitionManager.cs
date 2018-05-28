using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    struct StorageTransform
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;

        public StorageTransform(Transform transform)
        {
            position = transform.localPosition;
            rotation = transform.localRotation;
            scale = transform.localScale;
        }
    }

    static private Dictionary<string, StorageTransform> m_gameObjects = new Dictionary<string, StorageTransform>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    static public void SaveGameObject(string name)
    {
        StorageTransform storage = new StorageTransform(GameObject.Find(name).transform);
        m_gameObjects.Add(name, storage);
    }

    static public void LoadGameObject(string name)
    {
        Transform transform = GameObject.Find(name).transform;
        transform.localPosition = m_gameObjects[name].position;
        transform.localRotation = m_gameObjects[name].rotation;
        transform.localScale = m_gameObjects[name].scale;
        m_gameObjects.Remove(name);
    }

    static public void TransScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
