using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DollRotateScript : MonoBehaviour
{
    private float degrees;
    private float totalDegrees;
    private float rotation;
    public Image fadeImage;
    private float fadeDuration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        degrees = 0.5f;
        rotation = 10;
        totalDegrees = 0;
        fadeDuration = 0.12f;
        RotateDoll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotateDoll()
    {
        totalDegrees = 0;
        StartCoroutine(Fade(0,1));
        StartCoroutine(Rotate(1));
        //StartCoroutine(Fade(1, 0));
    }

    public void RotateDollLeft()
    {
        totalDegrees = 0;
        StartCoroutine(Fade(0, 1));
        StartCoroutine(Rotate(-1));
        //StartCoroutine(Fade(1, 0));
    }

    IEnumerator Rotate(int multiplier)
    {
        while (totalDegrees < rotation)
        {
            transform.Rotate(0, degrees * multiplier, 0);
            totalDegrees += degrees;

            yield return new WaitForSeconds(0.01f);
            
        }
        this.gameObject.transform.Rotate(0, 80 * multiplier, 0);
        StartCoroutine(Fade(1, 0));
    }


    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        Color color = fadeImage.color;
        float timer = 0f;

        while (timer <= fadeDuration)
        {
            float t = timer / fadeDuration;
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            fadeImage.color = color;

            timer += Time.deltaTime;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;
    }
}
