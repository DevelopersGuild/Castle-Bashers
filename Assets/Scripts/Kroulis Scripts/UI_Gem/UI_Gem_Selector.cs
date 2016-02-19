using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Gem_Selector : MonoBehaviour {
    public Text page;
    public Text[] Gem_D=new Text[6];
    public Image[] Gem_I = new Image[6];
    public Sprite NULL;

    private GemManager current_gemManager;
    private int current_page;
    private int pages;
    private int current_select;
    private int size;
    private Gem[] gems;

    public void StartSelecting(GemManager manager)
    {
        current_gemManager = manager;
        gems = manager.GetStoredGems();
        size = gems.Length;
        if( size%6 > 0 )
        {
            pages = size / 6 + 1;
        }
        current_page = 1;
        current_select = 0;
    }

    void Updata()
    {
        if(current_gemManager!=null)
        {
            page.text = current_page.ToString() + "//" + pages.ToString();
            for(int i=1;i<=6;i++)
            {
                int real_position=(current_page-1)*6+i;
                if(real_position<=size)
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
                    Gem_D[i].text = descri;
                }
                else
                {
                    Gem_I[i].sprite = NULL;
                    Gem_D[i].text = "";
                }
            }
            
        }
        if (current_page<pages)
        {
            if (Input.GetKeyDown(KeyCode.PageDown))
                current_page++;
        }
        if (current_page>1)
        {
            if (Input.GetKeyDown(KeyCode.PageUp))
                current_page--;
        }
        if (current_select < 5 && ((current_page-1)*6+current_select+1)<size && (Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.RightArrow)))
        {
            current_select++;
        }
        if (current_select == 5 && current_page<pages && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            current_page++;
            current_select = 0;
        }
        if (current_select > 0 && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            current_select--;
        }
        if (current_select == 0 && current_page>1 && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            current_page--;
            current_select=5;
        }
        //cur?

        //Enter -> Select
        if(Input.GetKeyDown(KeyCode.Return))
        {
            //unequip

            //equip

        }
    }
}
