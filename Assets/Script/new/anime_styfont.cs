using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;

public class anime_styfont : MonoBehaviour
{
    public float shrinkRatio = 0.8f;
    public float yOffset = 5f;
    public Color textColor = Color.white;
    public float animationDuration = 0.2f;
    public TMP_InputField fill;

    private TextMeshProUGUI textComponent;
    private Vector3 originalPosition;
    private float originalFontSize;
    private Color originalColor;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        originalPosition = transform.position;
        originalFontSize = textComponent.fontSize;
        originalColor = textComponent.color;
    }

    public void ShrinkAndMove()
    {
        LeanTween.value(gameObject, originalFontSize, originalFontSize * shrinkRatio, animationDuration)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnUpdate((float value) => {
                textComponent.fontSize = (int)value;
            });

        LeanTween.moveY(gameObject, originalPosition.y + yOffset, animationDuration)
            .setEase(LeanTweenType.easeOutQuad);

        LeanTween.color(gameObject, textColor, animationDuration)
            .setEase(LeanTweenType.easeOutQuad);
    }

    public void ResetText()
    {
        if(fill.text =="")
        {
            LeanTween.value(gameObject, textComponent.fontSize, originalFontSize, animationDuration)
                .setEase(LeanTweenType.easeOutQuad)
                .setOnUpdate((float value) => {
                    textComponent.fontSize = (int)value;
                });

            LeanTween.moveY(gameObject, originalPosition.y, animationDuration)
                .setEase(LeanTweenType.easeOutQuad);

            LeanTween.color(gameObject, originalColor, animationDuration)
                .setEase(LeanTweenType.easeOutQuad);
        }

    }
}