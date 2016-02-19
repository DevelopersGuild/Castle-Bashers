using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_GEM_FullControl : MonoBehaviour {

    public Image[] Gem = new Image[3];
    public Image[] Gem_Upper = new Image[3];
    public Image Cur;
    public Sprite NULL;

    [HideInInspector]
    public int change=0;
    [HideInInspector]
    public bool changing = false;
    [HideInInspector]
    public int playerid = 0;
    [HideInInspector]
    public Main_Process mainp = null;

    private UI_Gem_Selector selector;
    [HideInInspector]
    public int selecting;
    [HideInInspector]
    public bool subselecting;
    [HideInInspector]
    public int subindex;
    [HideInInspector]
    public GemManager manager;

    private Gem[] EquipedGems=new Gem[3];
	// Use this for initialization
	void Start () {
        selector = GetComponentInChildren<UI_Gem_Selector>();
        selecting = 0;
        subselecting = false;
	}

    public void Change()
    {
        if (manager != null)
        {
            EquipedGems = manager.GetEquippedGems();
            for(int i=0;i<3;i++)
            {
                if(EquipedGems[i]!=null)
                {
                    Gem[i].sprite = EquipedGems[i].getGemIcon();
                    Gem_Upper[i].sprite = GetComponent<UI_Gem_Upper_Library>().Gem_Type[EquipedGems[i].getGemType()];
                }
                else
                {
                    Gem[i].sprite = NULL;
                    Gem_Upper[i].sprite = NULL;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if(changing==true)
        {
            Cur.gameObject.SetActive(true);
            Cur.transform.localPosition = Gem[selecting - 1].transform.localPosition;
            if(subselecting==true)
            {
                //selector.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    subselecting = false;
                }
            }
            else
            {
                selector.gameObject.SetActive(false);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    GetComponentInParent<Character_Menu_FullControl>().gem_selecting = false;
                    changing = false;
                }
                if(Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    selecting = selecting == 1 ? 3 : selecting - 1;
                }
                if(Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.RightArrow))
                {
                    selecting = selecting == 3 ? 1 : selecting + 1;
                }
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    subindex= 0;
                    selector.StartSelecting(manager,selecting);
                    subselecting = true;
                }
            }
            
        }
        else
        {
            Cur.gameObject.SetActive(false);
            selector.gameObject.SetActive(false);
        }

        
	}
}
