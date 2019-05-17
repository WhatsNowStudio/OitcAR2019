using UnityEngine;
using System.Collections;

public class Conveyor2 : Machine
{
    public float finishTime = 1f;
    bool waitRobotArm = false;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        if(!isStartUp) gameObject.SetActive(false);
    }

    // Update is called once per frame
    protected override void Update () {
        time += Time.deltaTime;
        /*if (time >= state.length)
        {
            time = 0;
            //anim.Play();
        }*/
        if (!waitRobotArm && time > finishTime) StartUpTarget();
        base.Update();
    }

    protected override void StartUpTarget()
    {
        waitRobotArm = true;
        base.StartUpTarget();
    }

    public override void StartUp(Machine caller = null)
    {
        gameObject.SetActive(true);
        waitRobotArm = false;
        base.StartUp(caller);
    }
    public override void ShutDown()
    {
        gameObject.SetActive(false);
        base.ShutDown();
    }
}
