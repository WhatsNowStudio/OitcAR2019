using UnityEngine;
using System.Collections;

public class Machine : MonoBehaviour
{
    public bool autoStartUp = false;
    protected bool isStartUp = false;
    public GameObject targetMachine = null;
    protected Machine callerMachine = null;
    protected float time = 0f;
    protected Animation anim;
    protected AnimationState state;
    protected virtual void Awake()
    {
        anim = GetComponent<Animation>();
        if (anim)
            foreach (AnimationState state in anim)
            {
                this.state = state;
                break;
            }
    }
    // Use this for initialization
    protected virtual void Start()
    {
        if (autoStartUp) StartUp();
        /*if (!anim || !state) return;
        anim.Play();*/
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!anim || !state) return;
        anim.Play();
        state.speed = isStartUp ? 1f : 0f;
        state.time = time;
    }

    public virtual void StartUp(Machine caller = null)
    {
        callerMachine = caller;
        isStartUp = true;
        //time = 0f;
    }

    protected virtual void StartUpTarget()
    {
        if (targetMachine && targetMachine.GetComponent<Machine>())
            targetMachine.GetComponent<Machine>().StartUp(this);
    }

    public virtual void ShutDown()
    {
        isStartUp = false;
    }

    protected virtual void ShutDownCaller()
    {
        if(callerMachine) callerMachine.ShutDown();
    }
}
