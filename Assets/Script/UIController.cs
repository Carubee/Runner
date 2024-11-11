using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class UIController : MonoBehaviour
{
    public Text status;
    public static UIController singleton;
    public GameObject panel;
    public GameObject button;
    public GameObject scorePanel;
    public GameObject scoreBtn;
    public Text redScoreText;
    public Text blueScoreText;
    public bool start;
    void Start()
    {
        singleton = this;
        panel.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangedText(string s)
    {
        status.text = s;
    }
    public void Close()
    {
        panel.SetActive(false);
        start = true;
    }
    public void ScoreBoard()
    {
        scorePanel.SetActive(true);
        redScoreText.text = ScoreController.singleton.redScore.ToString();
        blueScoreText.text = ScoreController.singleton.blueScore.ToString();
    }
    public void StartGame()
    {
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
