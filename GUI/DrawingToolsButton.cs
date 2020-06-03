using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawingToolsButton : MonoBehaviour
{
    public GameObject drawingTools;
    public UIController uIController;
    private Button drawingToolsButton;
    private Image drawingToolsImage;
    private Vector2 bottomLeft;
    private Vector2 topRight;
    public Sprite brushSprite;
    public Sprite eraserSprite;
    public Sprite colorPickerSprite;
    // Start is called before the first frame update
    void Start()
    {   
        drawingTools.gameObject.SetActive(false);
        drawingToolsButton = GetComponent<Button>();
        drawingToolsImage = GetComponent<Image>();
        // calculating left bottom position of button
        bottomLeft = new Vector2(transform.position.x - drawingToolsButton.image.rectTransform.rect.width / 2,
            transform.position.y - drawingToolsButton.image.rectTransform.rect.height / 2);
        // calculating top right position of button
        topRight = new Vector2(transform.position.x + drawingToolsButton.image.rectTransform.rect.width / 2,
        transform.position.y + drawingToolsButton.image.rectTransform.rect.height / 2);
    }

    // enabling/disabling drawing tools depending on this button click
    public void Switch() {
        drawingTools.gameObject.SetActive(!drawingTools.gameObject.activeSelf);
        uIController.SetPalette(drawingTools.gameObject.activeSelf);
    }

    public bool IsButtonClicked(Vector2 clickPos)
    {
        return clickPos.x > bottomLeft.x && clickPos.x < topRight.x && clickPos.y > bottomLeft.y && clickPos.y < topRight.y;
    }

    public void SetBrushImage()
    {
        drawingToolsImage.sprite = brushSprite;
    }

    public void SetEraserImage()
    {
        drawingToolsImage.sprite = eraserSprite;
    }

    public void SetColorPickerImage()
    {
        drawingToolsImage.sprite = colorPickerSprite;
    }
}
