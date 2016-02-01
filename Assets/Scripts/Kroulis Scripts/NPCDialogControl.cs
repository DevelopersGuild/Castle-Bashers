using UnityEngine;
using System.Collections;
using Kroulis.Dialog;
using System.Collections.Generic;

public class NPCDialogControl : MonoBehaviour {

    private Main_Process MainProcess;
    private int count = 0;

    public string NPC_Name = "";

    //Type of the dialogs running.
    public bool by_order = true;
    public bool by_requirement = false;
    public bool closest_requirement = false;
    public bool by_random = false;


    public List<NPCDialog> Dialogs=new List<NPCDialog>();

	void Start () {
        MainProcess = GameObject.Find("Main Process").GetComponent<Main_Process>();
	}
	
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Dialogs.Count>0)
            {
                if (by_order) //For those dialogs from a to b to c to d
                {
                    if (count == Dialogs.Count)
                        count = 0;
                    MainProcess.OpenDialog(Dialogs[count].id, NPC_Name);
                    if (Dialogs[count].action!=null)
                        Dialogs[count].action.Action();
                    count++;

                }
                else if (by_requirement)//Will only run the first one meets the requirement.
                {
                    for(int i=0;i<Dialogs.Count;i++)
                    {
                        if(Dialogs[i].requirement==null || Dialogs[i].requirement.MeetRequirement())
                        {
                            MainProcess.OpenDialog(Dialogs[i].id,NPC_Name);
                            if (Dialogs[i].action != null)
                                Dialogs[i].action.Action();
                            break;
                        }
                    }
                }
                else if(closest_requirement)//Will run the exactully the closest requirement dialog.
                {

                    int minvalue = 0x3f3f3f3f;
                    int minindex = -1;
                    for(int i=0;i<Dialogs.Count;i++)
                    {
                        if(Dialogs[i].requirement==null)
                        {
                            minindex = i;
                            break;
                        }
                        else
                        {
                            int result = Dialogs[i].requirement.MeetRequirementI();
                            if (result >= 0)
                            {
                                if (minvalue > result)
                                {
                                    minvalue = result;
                                    minindex = i;
                                }
                            }
                        } 
                    }
                    if(minindex!=-1)
                    {
                        MainProcess.OpenDialog(Dialogs[minindex].id, NPC_Name);
                        if (Dialogs[minindex].action != null)
                            Dialogs[minindex].action.Action();
                    }
                }
                else//Run in random.
                {
                    int rdm = Random.Range(0,Dialogs.Count-1);
                    MainProcess.OpenDialog(Dialogs[rdm].id, NPC_Name);
                    if (Dialogs[rdm].action != null)
                        Dialogs[rdm].action.Action();
                }
            }
        }
    }

    public Main_Process GetMainProcess()
    {
        return MainProcess;
    }
}
