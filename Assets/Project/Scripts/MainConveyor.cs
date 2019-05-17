using UnityEngine;
using System.Collections;

public class MainConveyor : Conveyor
{
    public GameObject conveyor1, conveyor2 = null;
    public GameObject robotArm = null;
    GameObject component = null; //要給手臂加上的零件
    float pickUpTime = 3.75f;
    bool change = false;
    float finishSite = 0f;
    bool isPickUp = false;
    protected override void Start()
    {
        base.Start();
        Transform mid = transform.Find("Mid");
        if (mid)
        {
            finishSite = mid.localPosition.x;
            Destroy(mid.gameObject);
        }
        if (work)
        {
            component = work.transform.Find("Component").gameObject;
            component.SetActive(false);
        }
    }
    public override void StartUp(Machine caller = null)
    {
        isPickUp = false;
        if (component) component.SetActive(false);
        //交換運輸帶
        change = !change;
        StartUpNewTarget(change ? conveyor1 : conveyor2);
        waitTime = change ? 2f : 0.75f;
        base.StartUp(caller);
    }
    protected override void Update()
    {
        if (robotArm && work && work.localPosition.x - speed * pickUpTime <= finishSite)
        {
            if (!isPickUp)
            {
                if (robotArm.GetComponent<Machine>())
                    robotArm.GetComponent<Machine>().StartUp(this);
                isPickUp = true;
            }
            if (component && work.localPosition.x <= finishSite)
            {
                component.SetActive(true);
                /*if(robotArm.GetComponent<MainRobotArm>() && robotArm.GetComponent<MainRobotArm>().work)
                    robotArm.GetComponent<MainRobotArm>().work.SetActive(false);*/
            }
        }
        base.Update();
    }
    void StartUpNewTarget(GameObject newTarget)
    {
        GameObject target = targetMachine;
        targetMachine = newTarget;
        StartUpTarget();
        targetMachine = target;
    }
}
