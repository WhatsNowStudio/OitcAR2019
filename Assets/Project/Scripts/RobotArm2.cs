using UnityEngine;
using System.Collections;

public class keyFrame2
{
    public float pickUp = 0f;
    public float release = 0f;
    public float finish = 0f;
}

public class RobotArm2 : Machine
{
    public float pickUpTime = 2f;
    public float releaseTime = 5f;
    public GameObject work = null;
    float stepTime = 6f;
    int step = 1;
    protected override void Start()
    {
        base.Start();
        //work = transform.Find("Work").gameObject;
        if (work) work.SetActive(false);
    }
    // Update is called once per frame
    protected override void Update()
    {
        if (isStartUp && work)
        {
            time += Time.deltaTime;
            //if (time >= state.length) time = 0f;
            //放開
            if (time >= releaseTime)
            {
                if (work.activeSelf) StartUpTarget();
            }
            else if (time >= pickUpTime && !work.activeSelf) //夾起
                ShutDownCaller();
        }
        base.Update();
    }
    protected override void StartUpTarget()
    {
        if (work) work.SetActive(false);
        base.StartUpTarget();
    }
    protected override void ShutDownCaller()
    {
        if (work) work.SetActive(true);
        base.ShutDownCaller();
    }
    public override void ShutDown()
    {
        base.ShutDown();
    }
}
