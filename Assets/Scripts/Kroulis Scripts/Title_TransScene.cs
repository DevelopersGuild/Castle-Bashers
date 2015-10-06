using UnityEngine;
using System.Collections;

public class Title_TransScene : MonoBehaviour {

    public int guiDepth = 0;

    public string levelToLoad = "";
    //public int levelToLoadInt;
    public Texture2D splashLogo;
    float fadeSpeed = 0.8f;
    float waitTime = 0f;

    public bool waitForInput = false;
    public bool startAutomatically = false;
    public bool IsDealPlayer = false;
    private float timeFadingInFinished = 0.0f;
    //Event that will begin to do after transform
    public delegate void EventHandler();


    public event EventHandler trigger;
    public delegate void EventHandler2nd();

    public event EventHandler2nd triggerAtLoading;
    //way of transform
    public enum SplashType
    {
        LoadNextLevelThenFadeOut,
        FadeOutThenLoadNextLevel
    }
    public SplashType splashType;

    private float alpha = 0.0f;

    private enum FadeStatus
    {
        Paused,
        FadeIn,
        FadeWaiting,
        FadeOut
    }
    private FadeStatus status = FadeStatus.Paused;

    private Rect splashLogoPos = new Rect();
    //FillScreen
    public enum LogoPositioning
    {
        Centered,
        Stretched
    }
    public LogoPositioning logoPositioning;

    private bool loadingNextLevel = false;

    void Start()
    {
        if (logoPositioning == LogoPositioning.Centered)
        {
            splashLogoPos.x = (Screen.width * 0.5f) - (splashLogo.width * 0.5f);
            splashLogoPos.y = (Screen.height * 0.5f) - (splashLogo.height * 0.5f);

            splashLogoPos.width = splashLogo.width;
            splashLogoPos.height = splashLogo.height;
        }
        else
        {
            splashLogoPos.x = 0;
            splashLogoPos.y = 0;

            splashLogoPos.width = Screen.width;
            splashLogoPos.height = Screen.height;
        }

        if (splashType == SplashType.LoadNextLevelThenFadeOut)
        {
            DontDestroyOnLoad(this);

        }
    }

    //Begin to switch
    public void StartSplash(int i)
    {
        status = FadeStatus.FadeIn;
        //levelToLoadInt = level;
        SetValue(i);

    }
    public void setIsDealPlayer(bool isDealPlayer)
    {
        IsDealPlayer = isDealPlayer;
    }
    public void SetValue(int i)
    {
        switch (i)
        {
            case 1:
                fadeSpeed = 0.8f;
                waitTime = 1.0f;
                break;
            case 2:
                fadeSpeed = 0.8f;
                waitTime = 1.0f;
                break;
            case 3:
                fadeSpeed = 0.8f;
                waitTime = 1.0f;
                break;
        }
    }
    void Update()
    {

        switch (status)
        {
            case FadeStatus.FadeIn:

                alpha += fadeSpeed * Time.deltaTime;
                break;
            case FadeStatus.FadeWaiting:
                if ((!waitForInput && Time.time >= timeFadingInFinished + waitTime) || (waitForInput && Input.anyKey))
                {
                    status = FadeStatus.FadeOut;

                }
                break;
            case FadeStatus.FadeOut:
                alpha += -fadeSpeed * Time.deltaTime * 2;
                if (alpha <= 0.0f)
                {
                    if (trigger != null)
                    {
                        trigger();
                    }

                    status = FadeStatus.Paused;

                }
                break;
        }
    }

    void OnGUI()
    {

        GUI.depth = guiDepth;
        if (splashLogo != null)
        {
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, Mathf.Clamp01(alpha));
            GUI.DrawTexture(splashLogoPos, splashLogo);
        }
        if (alpha > 1.0f)
        {

            status = FadeStatus.FadeWaiting;
            timeFadingInFinished = Time.time;
            alpha = 1.0f;
            if (splashType == SplashType.LoadNextLevelThenFadeOut)
            {
                                loadingNextLevel = true;
                if ((Application.levelCount) >= 1)
                {
                    //print(levelToLoadInt);
                    //Application.LoadLevel(levelToLoadInt);
                    Application.LoadLevel(levelToLoad);
                    if (triggerAtLoading != null)
                        triggerAtLoading();
                }
            }
        }
        if (alpha < 0.0f)
        {
            if (splashType == SplashType.FadeOutThenLoadNextLevel)
            {
                if (Application.levelCount >= 1)
                {
                    //Application.LoadLevel(levelToLoadInt);
                    Application.LoadLevel(levelToLoad);
                }
            }
            else
            {


            }
        }
    }
}
