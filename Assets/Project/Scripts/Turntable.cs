using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TurntableAction
{
    public GameObject work = null;
    public GameObject machine = null;
    public TurntableAction()
    {
    }
}

public class Turntable : Machine
{
    float rotation = 0f;
    //Quaternion defaultRotation;
    bool waitRobotArm = false;
    public TurntableAction[] action;
    GameObject lastWork = null;
    //public GameObject platform = null;
    int step = 0;
    float pushTime = 1f; //壓下到放開的時間
    float waitPushTime = 1f;
    // Use this for initialization
    protected override void Start()
    {
        ///gameObject.SetActive(false);
        //if (platform) defaultRotation = platform.transform.rotation;
        base.Start();
        waitPushTime = pushTime;
        foreach (TurntableAction act in action)
            if (act.work)
            {
                act.work.SetActive(false);
                act.work.transform.localEulerAngles = Vector3.zero;
            }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!isStartUp || waitRobotArm || action.Length == 0) return;
        //轉
        if (waitPushTime <= 0f)
        {
            rotation += Time.deltaTime * 90f;
            float currentRotation = 360f / action.Length * step;
            if (rotation >= currentRotation)
            {
                rotation = currentRotation;
                waitPushTime = pushTime;
            }
        }
        //壓
        if (waitPushTime > 0f)
        {
            if (step >= action.Length) StartUpTarget();
            else
            {
                waitPushTime -= Time.deltaTime;
                Press(Mathf.Max(waitPushTime, 0f));
                //if (waitPushTime <= pushTime / 2f) SetWork();
                if (waitPushTime <= 0f)
                {
                    step++;
                    //SetWork();
                }
            }
            /*if (step >= action.Length) StartUpTarget();
            else
            {
                if (waitPushTime > pushTime / 2f)
                {
                    waitPushTime -= Time.deltaTime;
                    if (waitPushTime <= pushTime / 2f)
                    {
                        step++;
                        SetWork();
                    }
                }else waitPushTime -= Time.deltaTime;
                Press(Mathf.Max(waitPushTime, 0f));
            }*/
        }
        /*rotation += Time.deltaTime * 90f;
        float currentRotation = 360f / action.Length * step;
        if (rotation >= currentRotation)
        {
            rotation = currentRotation;
            if (step >= action.Length) StartUpTarget();
            else
            {
                pushTime += Time.deltaTime;
                if (pushTime >= 1f)
                {
                    pushTime = 0f;
                    step++;
                    SetWork();
                }
            }
        }*/
        transform.localEulerAngles = new Vector3(0f, rotation, 0f);
        SetWork();
        /*if(platform)
        {
            platform.transform.rotation = defaultRotation;
            platform.transform.Rotate(transform.eulerAngles);
        }*/
    }
    public override void StartUp(Machine caller = null)
    {
        ///gameObject.SetActive(true);
        waitRobotArm = false;
        rotation = 0f;
        waitPushTime = 1f;
        lastWork = null;
        step = 0;
        SetWork();
        base.StartUp(caller);
    }
    protected override void StartUpTarget()
    {
        waitRobotArm = true;
        base.StartUpTarget();
    }
    public override void ShutDown()
    {
        ///gameObject.SetActive(false);
        foreach (TurntableAction act in action)
            if (act.work) act.work.SetActive(false);
        base.ShutDown();
    }
    void SetWork()
    {
        int newStep = (waitPushTime > 0f && waitPushTime <= pushTime / 2f) ? step + 1 : step;
        if (action.Length > 0 && newStep <= action.Length && lastWork != action[Mathf.Max(newStep - 1, 0)].work)
        {
            if (lastWork) lastWork.SetActive(false);
            if (lastWork = action[Mathf.Max(newStep - 1, 0)].work)
                lastWork.SetActive(true);
        }
    }
    void Press(float rate)
    {
        if (step < 1 || action.Length == 0 || step >= action.Length) return;
        if(!action[step].machine) return;
        Transform push = action[step].machine.transform.Find("Pivot/Push");
        if (!push) return;
        push.localPosition = new Vector3(0f, -0.3f * (rate > pushTime / 2f ? pushTime - rate : rate), 0f); //-0.09f
    }
}
