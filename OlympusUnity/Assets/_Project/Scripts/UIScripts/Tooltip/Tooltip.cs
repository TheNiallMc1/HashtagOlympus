using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public bool followMouse;
    private PlayerControls playerControls;

    [HideInInspector] public Transform anchorPosition;

    public Image tooltipBackground;
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public LayoutElement layoutElement;

    public Color32 headerColour = Color.white;
    public Color32 contentColour = Color.white;
    public Color32 backgroundColour = Color.black;

    private RectTransform rectTransform;

    [Tooltip("How many characters can be displayed before the tooltip wraps to the next line")]
    public int characterWrapLimit;

    public void Start()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();

        rectTransform = GetComponent<RectTransform>();    
    }

    public void Update()
    {
        if (followMouse)
        {
            FollowMouse();
        }
        else if (anchorPosition != null)
        {
            MoveToAnchor();
        }
    }

    public void MoveToAnchor()
    {
        transform.position = new Vector3(anchorPosition.position.x, anchorPosition.position.y, anchorPosition.position.z);
        
        float pivotX = transform.position.x / Screen.width;
        float pivotY = transform.position.y / Screen.height;
        
        rectTransform.pivot = new Vector2(pivotX, pivotY);
    }
    
    public void FollowMouse()
    {
        Vector3 mousePosition = GUIUtility.ScreenToGUIPoint(playerControls.Mouse.MousePos.ReadValue<Vector2>());
        transform.position = new Vector3(mousePosition.x, mousePosition.y + 2, mousePosition.z);

        //transform.position = newPosition;
        
        float pivotX = transform.position.x / Screen.width;
        float pivotY = transform.position.y / Screen.height;
        
        rectTransform.pivot = new Vector2(pivotX, pivotY);
    }

    public void SetStyle()
    {
        headerField.color = headerColour;
        contentField.color = contentColour;
        tooltipBackground.color = backgroundColour;
    }

    public void SetText(string content, string header)
    {
        SetStyle();
        
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;
        
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit);
    }


}