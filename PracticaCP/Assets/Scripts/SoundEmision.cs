using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmision : MonoBehaviour
{

    public float noise;
    private GameObject noiseEmiter;

    // Start is called before the first frame update
    void Start()
    {
        noiseEmiter = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Mathf.Abs(noiseEmiter.GetComponent<SimpleCharacterControl>().m_currentV) > 0.34f)
            noise = 2f;
        else if (Mathf.Abs(noiseEmiter.GetComponent<SimpleCharacterControl>().m_currentV) > 0.01f)
            noise = 1f;
        else*/
        if (gameObject.tag == "Player")
            noise = Mathf.Abs(noiseEmiter.GetComponent<SimpleCharacterControl>().m_currentV);
        else
            noise = noiseEmiter.GetComponent<NoiseDoor>().noise;
    }
}
