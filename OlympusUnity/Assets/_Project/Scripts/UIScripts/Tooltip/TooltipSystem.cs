using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem current;

    public Tooltip tooltip;
    
    public void Awake()
    {
        current = this;
    }

    public static void Show(TooltipTrigger trigger, string content, string header = "")
    {
        current.tooltip.SetText(content, header);
        
        current.tooltip.backgroundColour = trigger.backgroundColour;
        current.tooltip.contentColour = trigger.contentColour;
        current.tooltip.headerColour = trigger.headerColour;
        current.tooltip.SetStyle();

        current.tooltip.FollowMouse();
        current.tooltip.gameObject.SetActive(true);
    }
    
    public static void Show(TooltipTrigger trigger, Transform anchor, string content, string header = "")
    {
        current.tooltip.SetText(content, header);
        
        current.tooltip.backgroundColour = trigger.backgroundColour;
        current.tooltip.contentColour = trigger.contentColour;
        current.tooltip.headerColour = trigger.headerColour;
        current.tooltip.SetStyle();
        
        current.tooltip.anchorPosition = anchor;
        
        current.tooltip.MoveToAnchor();
        current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
    }
}
