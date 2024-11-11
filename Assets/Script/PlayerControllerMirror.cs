using Mirror;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerControllerMirror : NetworkBehaviour
{
    public float speed = 100;
    public Rigidbody rigidbody3d;

    public GameObject self;
    public int numberPlayer;
    [SyncVar]
    public bool GoToStart;
    public static PlayerControllerMirror singleton;
    public GameObject[] playersArray;

    public bool startsite;

    [SyncVar] public bool active;
    [SyncVar] public int active2;

    [SyncVar]
    public bool timeOut;

    public bool hasTime;
    public bool touched;
    public float time;
    public float maxTime = 5;

    public void Start()
    {
        singleton=this;
        FindPlayersByTag();
        time = maxTime;
        TriggerControl();

    }
    void FixedUpdate()
    {
        if (isLocalPlayer && active)
        {
#if UNITY_6000_0_OR_NEWER
            Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * speed * Time.fixedDeltaTime;
            rigidbody3d.linearVelocity = movement;
#else
                Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * speed * Time.fixedDeltaTime;
                rigidbody3d.velocity = movement;
#endif
        }
    }
    public void FindPlayersByTag()
    {
        playersArray = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("Players found: " + playersArray.Length);

    }
    private void Update()
    {
        if (isLocalPlayer)

            if (Input.GetKeyDown(KeyCode.X))
                CmdAddScore(1,0);

        if (ScoreController.singleton.redScore >= 5 && numberPlayer == 1)
        {
            hasTime = true;
        }
        else if (ScoreController.singleton.blueScore >= 5 && numberPlayer == 2)
        {
            hasTime = true;

        }
        if (hasTime && touched == false)
        {
            time -= Time.deltaTime / 5;
        }
        if(time <= 0)
        {
            timeOut = true ;
            GoToStart = true;
        }
    }

    public override void OnStartServer()
    {

        base.OnStartServer();
        numberPlayer = 1;
        UIController.singleton.ChangedText("Start", true);

    }
    [Command]
    public void CmdAddScore(int r , int b)
    {
        
        RpcAddScore(r, b);
        // Do your own shot validation here because this runs on the server
        //self.GetComponent<PlayerControllerMirror>().GoToStart = true;
    }
    [ClientRpc]
    public void RpcAddScore(int r, int b)
    {
        ScoreController.singleton.AddScore(r, b);
        Debug.Log(r + " / " + b);
    }

    [Command]
    public void CmdTouch(int site)
    {
        // Do your own shot validation here because this runs on the server
        //self.GetComponent<PlayerControllerMirror>().GoToStart = true;
        RpcTouch(site);
        CircleManager.singleton.GenerateNewCircle("Cube(Clone)", site, timeOut);
    }
  
    [ClientRpc]
    public void RpcTouch(int site)
    {
        //CircleManager.singleton.GenerateNewCircle("Cube(Clone)", site, false);
    }
    [Command]
    public void CmdGoStart()
    {
        Debug.Log("start");
        active = true;
        RpcStart();
        active2 = 2;
    }
    public void TriggerControl()
    {

        Debug.Log("aaa");
        //GoStart();
        RpcStart();
    }
    [ClientRpc]
    public void RpcStart()
    {
        active2 = 2;

        active = true;
    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "startRed" && startsite == false)
        {
            GoToStart = false;
            numberPlayer = 1;
            startsite = true;
            CmdTouch(numberPlayer);

        }
        else if (other.gameObject.tag == "startBlue" && startsite == false)
        {
            GoToStart = false;
            numberPlayer = 2;
            startsite = true;
            CmdTouch(numberPlayer);

        }

        if (other.gameObject.tag == "cubeRed" && numberPlayer == 1 && GoToStart == false)
        {
            Destroy(other.gameObject);
            GoToStart = true;
            timeOut = false;
            touched = true;
        }
        else if (other.gameObject.tag == "cubeBlue" && numberPlayer == 2 && GoToStart == false)
        {
            Destroy(other.gameObject);
            GoToStart = true;
            timeOut = false;
            touched = true;
        }
        if (other.gameObject.tag == "startRed" && numberPlayer == 1 && GoToStart == true  )
        {
            GoToStart = false;
            if (timeOut == false)
            {
                CmdAddScore(1, 0);
            }
            CmdTouch(numberPlayer);
            time = maxTime;
            touched = false;
            //CircleManager.singleton.GenerateNewCircle("CubeRed(Clone)", 1, timeOut);
        }
        else if (other.gameObject.tag == "startBlue" && numberPlayer == 2 && GoToStart == true)
        {
            GoToStart = false;
            if (timeOut == false)
            {
                CmdAddScore(0, 1);
            }
            CmdTouch(numberPlayer);
            time = maxTime;
            touched = false;

            //CircleManager.singleton.GenerateNewCircle("CubeBlue(Clone)", 2, timeOut);

        }
    }
}

