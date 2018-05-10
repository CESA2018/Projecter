using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoTextManager : MonoBehaviour {

    [SerializeField]
    private Text m_text;

    float r, g, b;
    int rparam, gparam, bparam;
    int rgb;

	// Use this for initialization
	void Start ()
    {
        r = 0.0f;
        g = 0.0f;
        b = 0.0f;
        rparam = 255;
        gparam = 255;
        bparam = 255;
        rgb = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        int a = rgb % 3;
        switch (a)
        {
            case 0:
                rparam++;
                r = Mathf.Abs(rparam);
                if (rparam >= 255)
                {
                    rparam = -255;
                    rgb++;
                }
                break;
            case 1:
                gparam++;
                g = Mathf.Abs(gparam);
                if (gparam >= 255)
                {
                    gparam = -255;
                    rgb++;
                }
                break;
            case 2:
                bparam++;
                b = Mathf.Abs(bparam);
                if (bparam >= 255)
                {
                    bparam = -255;
                    rgb++;
                }
                break;
        }
        m_text.color = new Color(r / 255.0f, g / 255.0f, b / 255.0f);
	}
}
