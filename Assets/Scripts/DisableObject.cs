using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public GameObject Obj;
    public float activeTime;


    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (Obj.active == true)
        {
            StartCoroutine(Disableobj());
        }
    }

    IEnumerator Disableobj()
    {
        yield return new WaitForSeconds(activeTime);
        Obj.SetActive(false);
    }
}
