using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class UIController : MonoBehaviour
{
    public Text status;
    public static UIController singleton;
    public GameObject panel;
    public GameObject button;
    public bool start;
    void Start()
    {
        singleton = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangedText(string s,bool btn)
    {
        status.text = s;
        panel.SetActive(true);
        button.SetActive(btn);
    }
    public void StartGame()
    {
        PlayerControllerMirror.singleton.TriggerControl();
        panel.SetActive(false);

        if (NetworkClient.localPlayer != null)
        {
            // ค้นหา PlayerController ของผู้เล่นที่เป็น Local
            PlayerControllerMirror playerController = NetworkClient.localPlayer.GetComponent<PlayerControllerMirror>();
            if (playerController != null)
            {
                // เรียกใช้ Command เพื่อสลับค่า boolean
                PlayerControllerMirror.singleton.CmdGoStart();
            }
            else
            {
                Debug.LogWarning("PlayerController ไม่พบใน Local Player");
            }
        }
        else
        {
            Debug.LogWarning("ไม่มี Local Player ใน NetworkClient");
        }

    }
}
