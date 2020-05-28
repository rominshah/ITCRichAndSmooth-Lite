using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAnimations : MonoBehaviour
{

    [Header("Slider Components")]
    public GameObject Slider;
    public Image sliderBGImage;
    public Image sliderHandleImage;
    public GameObject Signet;
    public Image SwipeText;
    public Slider SliderBar;
    // Start is called before the first frame update
    private void OnEnable()
    {
        PlaySliderAnimation();
    }

    private void OnDisable()
    {
        StopCoroutine("SliderAnimation");
        sliderBGImage.color = new Color(1f, 1f, 1f, 0f);
        sliderHandleImage.color = new Color(1f, 1f, 1f, 0f);
        SwipeText.fillAmount = 0;
        Signet.SetActive(false);
        SliderBar.interactable = false;
    }


    public void PlaySliderAnimation()
    {
        StartCoroutine("SliderAnimation");
    }

    public void StopSliderAnimation()
    {
        StopCoroutine("SliderAnimation");
        StartCoroutine("FadeSliderSnimation");
    }

    IEnumerator SliderAnimation()
    {
        float duration = 1f;
        float timer = 0f;
        SliderBar.interactable = false;

        while (timer < duration)
        {
            if (timer < duration * 0.8f)
            {
                sliderHandleImage.transform.Rotate(0f, 0f, -3f);
            }
            else
            {
                if(!Signet.activeSelf)
                {
                    Signet.SetActive(true);
                }
            }
            sliderBGImage.color = new Color(1f, 1f, 1f, timer/duration);
            sliderHandleImage.color = new Color(1f, 1f, 1f, timer/duration);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
      
        duration = 1f;
        timer = 0f;

        while (timer < duration)
        {
            SwipeText.fillAmount = timer/duration; 
             timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        SliderBar.interactable = true;
        yield return null;
    }

    IEnumerator FadeSliderSnimation()
    {

        Debug.Log("FadeSliderSnimation");
        float duration = 1f;
        float timer = 0f;

        SliderBar.interactable = false;
        Signet.SetActive(false);
        while (timer < duration)
        {
            sliderBGImage.color = new Color(1f, 1f, 1f, 1f- (timer/duration));
            sliderHandleImage.color = new Color(1f, 1f, 1f, 1f - (timer/duration));
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
      
        duration = 0.5f;
        timer = 0f;

        while (timer < duration)
        {
            SwipeText.fillAmount = 1f- (timer / duration);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        gameObject.SetActive(false);
        yield return null;
    }

}
