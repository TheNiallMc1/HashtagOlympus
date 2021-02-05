using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string header;
    public string content;
    public Transform anchor;

    public Color32 backgroundColour = Color.black;
    public Color32 headerColour = Color.white;
    public Color32 contentColour = Color.white;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (anchor != null)
        {
            TooltipSystem.Show(this, anchor, content, header);
        }
        
        else
        {
            TooltipSystem.Show(this, content, header);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
