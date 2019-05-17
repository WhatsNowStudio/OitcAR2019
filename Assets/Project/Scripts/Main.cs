using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class Main : MonoBehaviour
{
    public GameObject[] lookPoint;
    int lookIndex = 1;
    // Use this for initialization
    void Start()
    {
        //VRSettings.renderScale = 2f;
        foreach (GameObject point in lookPoint)
            point.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
            SwitchLook();
        if (Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Alpha1))
            SwitchLook(1);
        if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Alpha2))
            SwitchLook(2);
        if (Input.GetKey(KeyCode.Keypad3) || Input.GetKey(KeyCode.Alpha3))
            SwitchLook(3);
    }
    public void SwitchLook(int index = 0)
    {
        if (index < 1) lookIndex++;
        else lookIndex = index;
        if (lookIndex > lookPoint.Length) lookIndex = 1;
        transform.position = lookPoint[lookIndex - 1].transform.position;
    }
}
