using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawableKTPController : MonoBehaviour
{
    public RawImage rawImage;

    private Texture2D texture;
    private Color[] colors;
    private bool isDrawing;
    private Vector2 previousPosition;

    private void Start()
    {
        int width = (int)rawImage.rectTransform.rect.width;
        int height = (int)rawImage.rectTransform.rect.height;

        texture = new Texture2D(width, height);
        colors = new Color[width * height];

        rawImage.texture = texture;
        ClearTexture();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            previousPosition = GetMousePosition();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
        }

        if (isDrawing)
        {
            Vector2 currentPosition = GetMousePosition();

            // Scale mouse positions to texture coordinates
            Vector2 scaledPreviousPosition = ScaleToTextureCoordinates(previousPosition);
            Vector2 scaledCurrentPosition = ScaleToTextureCoordinates(currentPosition);

            // Calculate the line segment between the previous and current positions
            int startX = Mathf.RoundToInt(scaledPreviousPosition.x);
            int startY = Mathf.RoundToInt(scaledPreviousPosition.y);
            int endX = Mathf.RoundToInt(scaledCurrentPosition.x);
            int endY = Mathf.RoundToInt(scaledCurrentPosition.y);

            // Draw the line segment on the texture
            DrawLine(startX, startY, endX, endY, Color.black);

            // Update the RawImage to display the modified texture
            texture.SetPixels(colors);
            texture.Apply();

            previousPosition = currentPosition;
        }
    }

    private Vector2 GetMousePosition()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rawImage.rectTransform, Input.mousePosition, null, out Vector2 localPosition);
        return localPosition;
    }

    private Vector2 ScaleToTextureCoordinates(Vector2 position)
    {
        float xScale = texture.width / rawImage.rectTransform.rect.width;
        float yScale = texture.height / rawImage.rectTransform.rect.height;

        return new Vector2(position.x * xScale, position.y * yScale);
    }

    private void DrawLine(int startX, int startY, int endX, int endY, Color color)
    {
        int dx = Mathf.Abs(endX - startX);
        int dy = Mathf.Abs(endY - startY);
        int sx = (startX < endX) ? 1 : -1;
        int sy = (startY < endY) ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            if (startX >= 0 && startX < texture.width && startY >= 0 && startY < texture.height)
            {
                int index = startY * texture.width + startX;
                colors[index] = color;
            }

            if (startX == endX && startY == endY)
                break;

            int err2 = 2 * err;
            if (err2 > -dy)
            {
                err -= dy;
                startX += sx;
            }
            if (err2 < dx)
            {
                err += dx;
                startY += sy;
            }
        }
    }

    public void ClearTexture()
    {
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.clear;
        }

        texture.SetPixels(colors);
        texture.Apply();
    }
}
