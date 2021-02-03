using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string header;
    public string content;
    public Transform anchor;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (anchor != null)
        {
            TooltipSystem.Show(anchor, content, header);
        }
        
        else
        {
            TooltipSystem.Show(content, header);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
