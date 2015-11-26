using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Kroulis.Dialog;

public class Dialog_FullControl : MonoBehaviour {

    public Text speaker;
    public Text content;
    public Text tips;
    private NormalDialog dialog = new NormalDialog();
    private Main_Process mainprocess;
    private bool couldnext = false;
    private string npcname="";
    // Use this for initialization
    void Start()
    {
        mainprocess = GameObject.Find("Main Process").GetComponent<Main_Process>();
        dialog.Init();
        gameObject.SetActive(false);
    }
	
    public void OpenDialog(string id,string name)
    {
        if(dialog.SetDialogTo(id))
        {
            npcname=name;
            string spk=dialog.GetCurrentSpeaker();
            if (spk == "#player1")
                speaker.text = mainprocess.GetPlayerScript(0).Player_Name + ":";
            else if (spk == "#player2")
                speaker.text = mainprocess.GetPlayerScript(1).Player_Name + ":";
            else if (spk == "#npc")
                speaker.text = npcname + ":";
            else
                speaker.text = spk + ":";
            content.text = dialog.GetCurrentDialog();
            couldnext = dialog.CouldNext();
            if(couldnext)
            {
                tips.text = "Press [Space] to continue.";
            }
            else
            {
                tips.text = " Press [Space] to finish.";
            }
            gameObject.SetActive(true);
        }
        else
        {
            mainprocess.OtherWindows_Close();
        }
    }

	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Space))
        {
            if(couldnext)
            {
                dialog.SetNext();
                string spk = dialog.GetCurrentSpeaker();
                if (spk == "#player1")
                    speaker.text = mainprocess.GetPlayerScript(0).Player_Name + ":";
                else if (spk == "#player2")
                    speaker.text = mainprocess.GetPlayerScript(1).Player_Name + ":";
                else if (spk == "#npc")
                    speaker.text = npcname + ":";
                else
                    speaker.text = spk + ":";
                content.text = dialog.GetCurrentDialog();
                couldnext = dialog.CouldNext();
                if (couldnext)
                {
                    tips.text = "Press [Space] to continue.";
                }
                else
                {
                    tips.text = " Press [Space] to finish.";
                }
            }
            else
            {
                gameObject.SetActive(false);
                mainprocess.OtherWindows_Close();
            }
        }
	}
}
