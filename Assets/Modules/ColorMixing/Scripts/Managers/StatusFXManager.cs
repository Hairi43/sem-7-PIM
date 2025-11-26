using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StatusFXManager : MonoBehaviour
{
    
    public Sprite[] statusSprites;
    public Image statusImage;
    public Image border;
    
    private CanvasGroup _canvasGroup;
    
    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void PlayFade(Color color, Status state)
    {
        StopAllCoroutines();
        statusImage.color = color;
        border.color = color;
        statusImage.sprite = statusSprites[(int)state];
        StartCoroutine(StartSequence());
        
    }

    private IEnumerator StartSequence()
    {   
        yield return Fade(0f,1f,.2f);
        yield return new WaitForSeconds(0.1f);
        yield return Fade(1f,0f,.2f);
        
        
    }

    private IEnumerator Fade(float alpha, float targetAlpha, float duration)
    {
        var time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(alpha, targetAlpha, time / duration);
            yield return null;
        }
        _canvasGroup.alpha = targetAlpha;
    }

}
