using UnityEngine;
using System.Collections;

public class MainRobotArm : Machine
{
    public GameObject work = null;
    public override void StartUp(Machine caller = null)
    {
        //if (work) work.SetActive(true);
        base.StartUp(caller);
    }
    // Update is called once per frame
    protected override void Update () {
        if (isStartUp)
        {
            time += Time.deltaTime;
            if (time > state.length)
            {
                time = 0f;
                ShutDown();
            }
        }
        base.Update();
    }
}
