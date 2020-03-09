using UnityEngine;
using UnityEngine.UI;

public class ChoicePanelController : MonoBehaviour
{

    [SerializeField]
    //The panel UI object that holds the text of the Choice Panel
    //Should be filled in inspector
    private RectTransform containerRectTransform;

    [SerializeField]
    //The text UI object contained by the panel UI object above
    //Should be filled in inspector
    private RectTransform textRectTransform;

    [SerializeField]
    //The text component of the text UI object
    //Should be filled in inspector
    private Text text;


    //Sets the container width and inner text width accordingly
    public void SetWidth(int newContainerWidth) {
        containerRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newContainerWidth);
        textRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newContainerWidth-10);
    }

    //Sets the text of this choice panel
    public void SetText(string displayText)
    {
        text.text = displayText;
    }

    //Sets the font size of this choice panel
    public void SetTextSize(int size)
    {
        text.fontSize = size;
    }

    //Changes if this panel is active in hierarchy or not
    public void SetVisible(bool visibility)
    {
        gameObject.SetActive(visibility);
    }
}
