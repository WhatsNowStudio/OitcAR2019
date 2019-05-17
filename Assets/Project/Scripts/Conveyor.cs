using UnityEngine;
using System.Collections;

public class Conveyor : Machine
{
    bool waitRobotArm = false;
    float finishSite = 0f;
    public float waitTime = 2f;
    // Use this for initialization
    protected Transform work;
    Vector3 originalPosition;
    protected float speed = 0.48f;
    protected override void Start()
    {
        work = transform.Find("Start");
        Transform finish = transform.Find("Finish");
        if (!work || !finish) return;
        finishSite = finish.localPosition.x;
        Destroy(finish.gameObject);
        originalPosition = work.position;
        if (!isStartUp) gameObject.SetActive(false);
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        work.localPosition -= new Vector3(speed * Time.deltaTime, 0f, 0f);
        if (!waitRobotArm && work.localPosition.x - (targetMachine ? speed * waitTime : 0f) <= finishSite) StartUpTarget();
    }
    protected override void StartUpTarget()
    {
        waitRobotArm = true;
        base.StartUpTarget();
        if (!targetMachine) ShutDown();
    }
    public override void StartUp(Machine caller = null)
    {
        gameObject.SetActive(true);
        waitRobotArm = false;
        work.position = originalPosition;
        base.StartUp(caller);
    }
    public override void ShutDown()
    {
        gameObject.SetActive(false);
        base.ShutDown();
    }
}
