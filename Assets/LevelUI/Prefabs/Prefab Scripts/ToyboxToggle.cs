using UnityEngine;
using UnityEngine.UI;

public class ToyboxToggle : MonoBehaviour
{
    private bool isVisible = false;
    private Vector3 startPos;

    public RectTransform buttonTransform;
    public RectTransform toyboxPanelTransform;

    public Sprite leftArrow;
    public Sprite rightArrow;
    private Image myIMGComponent;

    private void Awake()
    {
        if (buttonTransform == null)
        {
            buttonTransform = GetComponent<RectTransform>();
        }
        if (toyboxPanelTransform == null)
        {
            toyboxPanelTransform = GameObject.Find("ToySelectionPanel").GetComponent<RectTransform>();
        }
    }

    private void Start()
    {
        startPos = new Vector3(toyboxPanelTransform.rect.width - buttonTransform.rect.width + buttonTransform.rect.x, 0);
        Debug.Log(startPos);
        if (toyboxPanelTransform)
        {
            toyboxPanelTransform.localPosition += startPos;
        }
    }

    public void ToggleVisibility()
    {
        if (isVisible)
        {
            toyboxPanelTransform.localPosition += startPos;
            myIMGComponent = this.GetComponent<Image>();
            myIMGComponent.sprite = leftArrow;
        }
        else
        {
            toyboxPanelTransform.localPosition -= startPos;
            myIMGComponent = this.GetComponent<Image>();
            myIMGComponent.sprite = rightArrow;
        }
        isVisible = !isVisible;
    }

    /// <summary>
    /// Call one of these when the player should not be allowed to interact with the UI;
    /// </summary>
    public void Hide()
    {
        toyboxPanelTransform.position = new Vector3(toyboxPanelTransform.rect.width, 0);
    }

    public void Disable()
    {
        toyboxPanelTransform.gameObject.SetActive(false);
    }
}
