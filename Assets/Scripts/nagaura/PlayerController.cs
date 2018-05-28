using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Vector4 m_color;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetColor(Vector4 color)
    {
        m_color = color;
    }

    public Vector4 GetColor()
    {
        return m_color;
    }
}
