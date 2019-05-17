using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RobotArmAction
{
    public float pickUp = 2f;
    public float release = 5f;
    public float finish = 6f;
    public GameObject work = null;
    public GameObject target = null;
    public RobotArmAction()
    {
    }
}

public class RobotArm : Machine
{
    //public List<keyFrame> keyFrames = new List<keyFrame>();
    public RobotArmAction[] action = { new RobotArmAction() };
    //public float pickUpTime = 2f;
    //public float releaseTime = 5f;
    //public GameObject work = null;
    RobotArmAction act;
    //float stepTime = 6f;
    int step = 1;
    protected override void Start()
    {
        base.Start();
        //work = transform.Find("Work").gameObject;
        //if (work) work.SetActive(false);
        foreach (RobotArmAction key in action)
            if (key.work) key.work.SetActive(false);
    }
    // Update is called once per frame
    protected override void Update()
    {
        if (isStartUp && act.work)
        {
            time += Time.deltaTime;
            //KeyFrame key = keyFrames[step - 1];
            //if (time >= state.length) time = 0f;
            if (time >= act.finish) ShutDown(); //放開完畢
            //放開
            else if (time >= act.release)
            {
                if (act.work.activeSelf) StartUpTarget();
            }
            else if (time >= act.pickUp && !act.work.activeSelf) //夾起
                ShutDownCaller();
        }
        base.Update();
    }
    public override void StartUp(Machine caller = null)
    {
        if (isStartUp) ShutDown();
        if (action.Length > 0 && step <= action.Length)
            act = action[step - 1];
        base.StartUp(caller);
    }
    protected override void StartUpTarget()
    {
        if (act.work) act.work.SetActive(false);
        if(act.target) targetMachine = act.target;
        base.StartUpTarget();
    }
    protected override void ShutDownCaller()
    {
        if (act.work) act.work.SetActive(true);
        base.ShutDownCaller();
    }
    public override void ShutDown()
    {
        time = act.finish;
        step++;
        if (step > action.Length)
        {
            step = 1;
            time = 0f;
        }
        base.ShutDown();
    }
}
