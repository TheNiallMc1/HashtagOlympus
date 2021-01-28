using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Tooltip : MonoBehaviour
{
    public PlayerControls playerControls;
    
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public LayoutElement layoutElement;

    private RectTransform rectTransform;

    public int characterWrapLimit;

    public void Start()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();

        rectTransform = GetComponent<RectTransform>();    
    }

    public void Update()
    {
        FollowMouse();
    }

    public void SetText(string content, string header)
    {
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

    public void FollowMouse()
    {
        Vector3 mousePosition = GUIUtility.ScreenToGUIPoint(playerControls.Mouse.MousePos.ReadValue<Vector2>());
        transform.position = new Vector3(mousePosition.x - 10, mousePosition.y, mousePosition.z);

        //transform.position = newPosition;
        
        float pivotX = transform.position.x / Screen.width;
        float pivotY = transform.position.y / Screen.height;
        
        rectTransform.pivot = new Vector2(pivotX - 0.4f, pivotY);
        
    }
}