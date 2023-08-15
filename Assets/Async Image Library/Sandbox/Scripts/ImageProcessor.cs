using AsyncImageLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageProcessor : MonoBehaviour
{
    public void DivideByResize(AsyncImage asyncImage, int divideBy, Action onComplete)
    {
        asyncImage.Resize(divideBy, ResizeQuality.Medium, onComplete);
    }

    public void TargetDimensionResize(AsyncImage asyncImage, Vector2 targetDimensions, Action onComplete)
    {
        asyncImage.Resize(targetDimensions, ResizeQuality.Medium, onComplete);
    }
}
