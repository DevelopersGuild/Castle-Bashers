using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu_BGMovie_Control : MonoBehaviour {

    RawImage BGMOV;
    MovieTexture MOV;
    public bool temp_close = false;
	// Use this for initialization
	void Start () {
        BGMOV = GetComponent<RawImage>();
        //Debug.Log(BGMOV.mainTexture.GetType());
        if (!temp_close)
        {
            MOV = (MovieTexture)BGMOV.mainTexture;
            MOV.loop = true;
        }
        else
            MOV = null;
        //MOV.Play();
	}
	
	public void resume()
    {
        if(MOV!=null)
            MOV.Play();
    }

    public void pause()
    {
        if (MOV != null)
            MOV.Pause();
    }
}
