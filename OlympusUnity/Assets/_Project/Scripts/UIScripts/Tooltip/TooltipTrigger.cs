using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string header;
    public string content;

    public bool followMouse;
    public float mouseFollowOffset = 35f;
    public Transform anchor;
    
    public Color32 backgroundColour = Color.black;
    public Color32 headerColour = Color.white;
    public Color32 contentColour = Color.white;

    public float tooltipDelay = 0.5f;
    private Coroutine delayCoroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipDelay > 0)
        {
            delayCoroutine = StartCoroutine(DelayCoroutine());
        }

        else
        {
            TriggerTooltip();
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        ResetTooltip();
    }

    private void TriggerTooltip()
    {
        if ( followMouse )
        {
            TooltipSystem.Show(this, content, header);
        }
        else
        {
            TooltipSystem.Show(this, anchor, content, header);
        }
    }

    private void ResetTooltip()
    {
        StopCoroutine(delayCoroutine);
        TooltipSystem.Hide();
    }

    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSecondsRealtime(tooltipDelay);
        TriggerTooltip();
    }
}