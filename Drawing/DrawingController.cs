using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class DrawingController : MonoBehaviour
{
    public Camera mainCamera;
    public SpriteRenderer renderer;
    // used for keeping object bottom left position
    public GameObject pivot;
    public bool fillTextureOnStart;

    private string textureName;
    // pixels per unit
    private float ppu; 
    private float scaleX;
    private float scaleY;
    private int textureWidth;
    private int textureHeight;
    private Vector2 clickPosOld;
    private DrawingPalette drawingPalette;
    public UIController uIController;
    public DrawingToolsButton toolsButton;
    
    public void Start()
    {
        if (!renderer)
            renderer = GetComponent<SpriteRenderer>();
        if (!mainCamera)
            mainCamera = GameObject.FindObjectOfType<Camera>();
        drawingPalette = GameObject.FindObjectOfType<DrawingPalette>();
        Init();
    }

    private void Init()
    {
        // copying old texture
        Texture2D buf = GetInitImageFromTexture(renderer.sprite.texture);
        // copying texture info
        textureWidth = buf.width;
        textureHeight = buf.height;
        ppu = renderer.sprite.pixelsPerUnit;
        textureName = renderer.sprite.name;
        // getting sprite pivot (translating from pixels to world units)
        float pivotX = renderer.sprite.pivot.x / renderer.sprite.texture.width;
        float pivotY = renderer.sprite.pivot.y / renderer.sprite.texture.height;
        // creating new sprite
        renderer.sprite = Sprite.Create(buf, renderer.sprite.rect, new Vector2(pivotX, pivotY), ppu);
        renderer.sprite.name = textureName;
        renderer.material.mainTexture = buf;
        // applying changes that we made
        renderer.sprite.texture.Apply();
        // getting object scale
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
    }

    public void OnMouseDrag()
    {
        if (drawingPalette.paletteEnabled)
        {
            return;
        }

        // if there is no left mouse click
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        // checking if we clicked on drawing tools button
        if (toolsButton.IsButtonClicked(Input.mousePosition))
        {
            return;
        }

        // checking if the new mouse position is not same as the last
        if (clickPosOld.x == Input.mousePosition.x && clickPosOld.y == Input.mousePosition.y)
        {
            return;
        }

        clickPosOld.x = Input.mousePosition.x;
        clickPosOld.y = Input.mousePosition.y;

        Vector3 clickPos = -Vector3.one;
        
        // raycasting mouse click position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;

        // if we've shot some collider
        if (Physics.Raycast(ray, out hitData, Mathf.Infinity))
        {
            // if it's not collider with layer Drawing (id = 8) or it's not trigger
            if (hitData.collider.gameObject.layer != 8 || !hitData.collider.isTrigger)
            {
                return;
            }
            // calculating coords of the pixel on which the player had clicked
            // calculating distance from left bottom point to the point where the player clicked (we're using x and z axes)
            clickPos.x = GetDistance(new Vector2(hitData.point.x, hitData.point.z), new Vector2(pivot.transform.position.x, pivot.transform.position.z));
            // calculating distance between left bottom point y axis and click position point y axis
            clickPos.y = Mathf.Abs(hitData.point.y - pivot.transform.position.y);
            // applying scale and translating it into pixels 
            int x = (int)((clickPos.x / scaleX) * ppu);
            int y = (int)((clickPos.y / scaleY) * ppu);
            if (drawingPalette.colorPickerEnabled)
            {
                // if clicked pixel alpha is not zero
                if (renderer.sprite.texture.GetPixel(x, y).a > 0.001)
                    uIController.SetColor(renderer.sprite.texture.GetPixel(x, y));
            } else
            {
                DrawOnTexture(x, y, renderer.sprite.texture);
            }
        }
    }

    private void DrawOnTexture(int x, int y, Texture2D texture)
    {
        // if the click is not inside the texture
        if (x < 0 || x >= textureWidth || y < 0 || y >= textureHeight)
        {
            return;
        }
        Color color;
        // getting color for drawing
        if (drawingPalette.eraserEnabled)
        {
            color = new Color(0f, 0f, 0f, 0f);
        }
        else
        {
            color = drawingPalette.GetColor();
        }
        // getting brush for drawing
        Brush brush = drawingPalette.GetBrush();
        // drawing all pixels of brush
        for (int i = x - brush.size/2, n = 0; i <= x + brush.size/2 && n < brush.size; i++, n++)
        {
            for (int j = y - brush.size/2, k = brush.size - 1; j <= y + brush.size/2 && k >= 0; j++, k--)
            {
                if (!(i < 0 || i >= textureWidth || j < 0 || j >= textureHeight))
                {
                    if (brush.pixels[n][k])
                    {
                        texture.SetPixel(i, j, color);
                    }
                }
            }
        }
        texture.Apply();
    }

    public Texture2D GetInitImageFromTexture(Texture2D copiedTexture)
    {
        // creating a new texture(a copy)
        Texture2D texture = new Texture2D(copiedTexture.width, copiedTexture.height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        int y = 0;
        // setting all texture pixels
        while (y < texture.height)
        {
            int x = 0;
            while (x < texture.width)
            {
                if (fillTextureOnStart)
                {
                    texture.SetPixel(x, y, copiedTexture.GetPixel(x, y));
                } else
                {
                    texture.SetPixel(x, y, new Color(1, 1, 1, 0));
                }
                ++x;
            }
            ++y;
        }
        //setting texture name
        texture.name = (textureName + "_SpriteSheet_" + IdGenerator.GetId());

        //applying all changed that we've made in texture
        texture.Apply();

        return texture;
    }

    // this method will calculate the distance between two points
    private float GetDistance(Vector3 point1, Vector3 point2)
    {
        return Mathf.Sqrt(((point2.x - point1.x) * (point2.x - point1.x)) + ((point2.y - point1.y) * (point2.y - point1.y)));
    }

}
