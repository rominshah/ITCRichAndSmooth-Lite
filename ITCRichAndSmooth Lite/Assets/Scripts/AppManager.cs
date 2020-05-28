using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AppManager : MonoBehaviour
{

    public GameObject bg;
    public GameObject videoDisplay;
    public Animator bGAnim;
    public Animator mainUIAnim;
    public Animator videoAnim;
    //public Animator SliderAnim;
    public UnityEngine.UI.Slider continueSlider;
    public UnityEngine.Video.VideoPlayer player;
    public CustomRenderTexture videoTex;
    public SliderAnimations SliderAnimationObj;

    private bool atVideo = false;
    private readonly int yesTrig = Animator.StringToHash("Yes");
    private readonly int noTrig = Animator.StringToHash("No");
    private readonly int screenTrig = Animator.StringToHash("NextScreen");
    private readonly int videoTrig = Animator.StringToHash("PlayVideo");
    private readonly int videoEndTrig = Animator.StringToHash("VideoOver");
    private readonly int videoBool = Animator.StringToHash("VideoPlaying");
   // private readonly int SliderTrig = Animator.StringToHash("EndSlider");

    private bool isResetSlider = true;

    private void OnEnable()
    {
        player.loopPointReached += VideoEnded;
        player.prepareCompleted += PlayVideo;
    }

    private void Start()
    {
        bg.SetActive(true);
        ResetTriggers();
        videoTex.Release();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }    

        if (!continueSlider.gameObject.activeInHierarchy)
        {
            continueSlider.value = 0;
        }

        if (continueSlider.value > 0.975f && !atVideo)
        {
            EndSliderTriger();
            mainUIAnim.SetTrigger(screenTrig);
            Invoke("EnableVideo", 5.0f);
        }
        else if (continueSlider.value > 0.975f && atVideo)
        {
            EndSliderTriger();
            videoTex.Initialize();
            player.Play();
        }
        else if (Input.touchCount < 1 && !Input.anyKey)
        {
            continueSlider.value -= Time.deltaTime * 2;
        }
    }

    void EndSliderTriger()
    {
        if (isResetSlider)
        {
            isResetSlider = false;
            //SliderAnim.SetTrigger(SliderTrig);
            SliderAnimationObj.StopSliderAnimation();
            Debug.Log("EndSlider...");
            Invoke("ResetSliderTriger",1f);
           
        }
    }

    void ResetSliderTriger()
    {
        //SliderAnim.ResetTrigger(SliderTrig);
        isResetSlider = true;
    }

    public void VideoEnded(UnityEngine.Video.VideoPlayer p)
    {
        player.Stop();
        videoTex.Release();
        atVideo = false;
        videoDisplay.SetActive(false);
        //videoAnim.SetTrigger(videoEndTrig);
        bGAnim.SetBool(videoBool, false);

        mainUIAnim.SetTrigger(videoEndTrig);
        Invoke("ResetTriggers", 1.0f);
    }

    public void ContinueOrStop(bool con)
    {
        if (con)
        {
            mainUIAnim.SetTrigger(yesTrig);
        }
        else
        {
            mainUIAnim.SetTrigger(noTrig);
        }
    }

    private void EnableVideo()
    {
        atVideo = true;
    }

    private void PlayVideo(UnityEngine.Video.VideoPlayer p)
    {
        bGAnim.SetBool(videoBool, true);
        mainUIAnim.SetTrigger(videoTrig);
        videoDisplay.SetActive(true);

    }

    private void ResetTriggers()
    {
        mainUIAnim.ResetTrigger(yesTrig);
        mainUIAnim.ResetTrigger(noTrig);
        mainUIAnim.ResetTrigger(screenTrig);
        mainUIAnim.ResetTrigger(videoTrig);
        mainUIAnim.ResetTrigger(videoEndTrig);
        ResetSliderTriger();
        videoDisplay.SetActive(false);
    }

    private void OnDisable()
    {
        player.loopPointReached -= VideoEnded;
        player.prepareCompleted -= PlayVideo;
    }

}
