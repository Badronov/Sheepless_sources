using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPalette : MonoBehaviour
{
    private Image image;
    public Canvas canvas;
    private float imageScaleX;
    private float imageScaleY;
    public UIController uIController;
    private float paletteXPos;
    private float paletteYPos;
    private float paletteWidth;
    private float paletteHeight;

    void Start()
    {
        if (!image)
        {
            image = GetComponent<Image>();
        }
        if (!canvas)
            canvas = FindObjectOfType<Canvas>();

        // calculating image scale
        imageScaleX = image.sprite.texture.width / image.rectTransform.rect.width;
        imageScaleY = image.sprite.texture.height / image.rectTransform.rect.height;
        // calculating actual visible image size
        paletteWidth = image.rectTransform.rect.width * canvas.scaleFactor;
        paletteHeight = image.rectTransform.rect.height * canvas.scaleFactor;
        // calculating actual image position
        paletteXPos = (image.transform.position.x - (paletteWidth / 2));
        paletteYPos = (image.transform.position.y - (paletteHeight / 2));
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 clickPos = Input.mousePosition;
            // cheching if the click position is inside the palette
            if (clickPos.x > paletteXPos + paletteWidth || clickPos.x < paletteXPos || clickPos.y > paletteYPos + paletteHeight || clickPos.y < paletteYPos)
            {
                return;
            }
            // getting click position point relative to image 
            clickPos.x -= paletteXPos;
            clickPos.y -= paletteYPos;
            // applying canvas scale on calculated positions
            clickPos.x /= canvas.scaleFactor;
            clickPos.y /= canvas.scaleFactor;
            // applying image scale on calculated positions
            clickPos.x *= imageScaleX;
            clickPos.y *= imageScaleY;
            int x = (int)clickPos.x;
            int y = (int)clickPos.y;
            // getting clicked point pixel color
            Color color = image.sprite.texture.GetPixel(x, y);
            uIController.SetColor(color);
        }   
    }
}
