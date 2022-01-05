using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudControl : MonoBehaviour
{
    public static HudControl hControl { get; private set; }

    public GameObject keyOnOff;
    public GameObject relicOnOff;
   

    private void Awake()
    {
        if(hControl == null)
        {
            hControl = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }


    public void KeyOn()
    {
        keyOnOff.SetActive(true);
        
    }

    public void RelicOn()
    {
        relicOnOff.SetActive(true);

    }
   
}
