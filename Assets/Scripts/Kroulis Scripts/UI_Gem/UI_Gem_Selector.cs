using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Gem_Selector : MonoBehaviour {
    public Text page;
    public Text[] Gem_D=new Text[6];
    public Image[] Gem_I = new Image[6];
    public Sprite NULL;
    public Image cur;

    private GemManager current_gemManager;
    private int eqid;
    private int current_page;
    private int pages;
    private int current_select;
    private int size;
    private Gem[] gems;

    public void StartSelecting(GemManager manager,int _eqid)
    {
        current_gemManager = manager;
        gems = manager.GetStoredGems();
        size = gems.Length;
        if (size % 6 > 0)
        {
            pages = size / 6 + 1;
        }
        else
            pages = size / 6;
        current_page = 1;
        current_select = 0;
        eqid = _eqid-1;
        if (size == 0)
            cur.gameObject.SetActive(false);
        else
            cur.gameObject.SetActive(true);
        gameObject.SetActive(true);
        Debug.Log("size " + size.ToString());
    }

    void Update()
    {
        if(current_gemManager!=null)
        {
            page.text = current_page.ToString() + "/" + pages.ToString();
            for(int i=1;i<=6;i++)
            {
                int real_position=(current_page-1)*6+i;
                if (real_position <= size && gems[real_position - 1]!=null)
                {
                    Gem_I[i - 1].sprite = gems[real_position-1].getGemIcon();
                    string descri = "";
                    switch (gems[real_position - 1].getQuality())
                    {
                        case 1:
                            descri = "<color=#ffffffff>";
                            break;
                        case 2:
                            descri = "<color=#add8e6ff>";
                            break;
                        case 3:
                            descri = "<color=#ff00ffff>";
                            break;
                        case 4:
                            descri = "<color=#ffa500ff>";
                            break;
                    }
                    descri+=gems[real_position - 1].getName()+"</color>\n";
                    switch (gems[real_position-1].getGemType())
                    {
                        case 1:
                            descri += "<color=#ff0000ff>Attack</color>\n";
                            break;
                        case 2:
                            descri += "<color=#00ffffff>Defend</color>\n";
                            break;
                        case 3:
                            descri += "<color=#ffa500ff>Support</color>\n";
                            break;
                        case 4:
                            descri += "<color=#ff00ffff>Universal</color>\n";
                            break;
                    }
                    descri += "<color=#ffffffff>" + gems[real_position - 1].GetDescription() + "</color>";
                    Gem_D[i-1].text = descri;
                }
                else
                {
                    Gem_I[i-1].sprite = NULL;
                    Gem_D[i-1].text = "";
                }
            }
            
        }
        if (current_page<pages)
        {
            if (Input.GetKeyDown(KeyCode.PageDown))
            {
                current_page++;
                current_select = 0;
            }
                
        }
        if (current_page>1)
        {
            if (Input.GetKeyDown(KeyCode.PageUp))
            {
                current_page--;
                current_select = 5;
            }
                
        }
        if (current_select < 5 && ((current_page-1)*6+current_select+1)<size && (Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.RightArrow)))
        {
            current_select++;
        }
        else if (current_select == 5 && current_page<pages && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            current_page++;
            current_select = 0;
        }
        else if (current_select > 0 && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            current_select--;
        }
        else if (current_select == 0 && current_page>1 && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            current_page--;
            current_select=5;
        }
        //cur?
        cur.transform.localPosition = Gem_I[current_select].transform.localPosition;
        //Enter -> Select
        if(Input.GetKeyDown(KeyCode.Return))
        {
            //unequip
            if (current_gemManager.GetEquippedGem(eqid) != null)
                current_gemManager.unequip(eqid);
            //equip
            current_gemManager.equip((current_page-1)*6+current_select);
        }
    }
    
}
