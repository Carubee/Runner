using Mirror;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Cude : NetworkBehaviour
{
    public Image timeImage;

    public bool hasTime;
    public float maxTime;
    public float timeAm;

    public int site;
    public void Start()
    {
        if (ScoreController.singleton.redScore >= 5 && site == 1)
        {
            hasTime = true;
        }
        else if (ScoreController.singleton.blueScore >= 5 && site == 2)
        {
            hasTime = true;

        }
    }
    public void Update()
    {
        if (hasTime)
        {
            timeImage.fillAmount -= Time.deltaTime /maxTime;
        }
        if (timeImage.fillAmount == 0)
        {
            Debug.Log("TimeOut");
            Destroy(this.gameObject);
            if (PlayerControllerMirror.singleton.numberPlayer == site)
            {
               // PlayerControllerMirror.singleton.timeOut = true;
               // PlayerControllerMirror.singleton.RpcTimeOut(1);
               // Debug.Log("TimeOut");
            }
            //CircleManager.singleton.GenerateNewCircle("Cube(Clone)", site, true);
        }
    }
   
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(other.gameObject.name);
            //CircleManager.singleton.GenerateNewCircleRed(gameObject.name);
            if (other.gameObject.name == "Player1" && site == 1)
            {
                Destroy(this.gameObject);

                //ScoreController.singleton.AddScore(1, 0);
            }
            else if (other.gameObject.name == "Player2" && site == 2)
            {
                Destroy(this.gameObject);

                //ScoreController.singleton.AddScore(0, 1);

            }
        }
    }
}

