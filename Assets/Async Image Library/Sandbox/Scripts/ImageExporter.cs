using AsyncImageLibrary;
using SkiaSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageExporter : MonoBehaviour
{
    public Text widthText;
    public Text heightText;
    public Text formatText;

    public GameObject exportPanel;
    public RawImage imagePreview;

    public Dropdown resizeDropdown;
    public GameObject[] resizeValueFields;

    public Button resizeButton;
    public Button exportButton;

    private AsyncImage asyncImage;
    private SKImageInfo imageInfo;
    private SKEncodedImageFormat imageFormat;
    private ImageProcessor imageProcessor;
    public static ImageExporter instance;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        imageProcessor = GetComponent<ImageProcessor>();
    }

    public void OpenExportPanel(AsyncImage asyncImage)
    {
        this.asyncImage = asyncImage;
        var (info, format) = asyncImage.GetInfoFromFile();
        imageInfo = info.Value;
        imageFormat = format.Value;
        UpdateInfo();

        UpdateTexture();

        exportPanel.SetActive(true);
    }

    public void CloseExportPanel()
    {
        exportPanel.SetActive(false);
    }

    public void OnResizeDropdownValueChange()
    {
        for (int i = 0; i < resizeValueFields.Length; i++)
        {
            resizeValueFields[i].SetActive(false);
        }
        if (resizeDropdown.value != 1)
        {
            resizeValueFields[resizeDropdown.value].SetActive(true);
        }
        else // for target dimension (x, y input field)
        {
            resizeValueFields[resizeDropdown.value].SetActive(true);
            resizeValueFields[resizeDropdown.value + 1].SetActive(true);
        }

    }

    public void ResizeImage()
    {
        exportButton.interactable = false;
        resizeButton.interactable = false;

        switch (resizeDropdown.value)
        {
            case 0:
                imageProcessor.DivideByResize(asyncImage, int.Parse(resizeValueFields[0].GetComponent<InputField>().text),
                () =>
                {
                    asyncImage.GenerateTexture(UpdateTexture);
                    imageInfo.Width = asyncImage.Bitmap.Width;
                    imageInfo.Height = asyncImage.Bitmap.Height;
                    exportButton.interactable = true;
                    resizeButton.interactable = true;
                    UpdateInfo();
                });
                break;
            case 1:
                Vector2 targetDimensions = new Vector2();
                targetDimensions.x = int.Parse(resizeValueFields[1].GetComponent<InputField>().text);
                targetDimensions.y = int.Parse(resizeValueFields[2].GetComponent<InputField>().text);
                
                imageProcessor.TargetDimensionResize(asyncImage, targetDimensions,
                () =>
                {
                    asyncImage.GenerateTexture(UpdateTexture);
                    imageInfo.Width = asyncImage.Bitmap.Width;
                    imageInfo.Height = asyncImage.Bitmap.Height;
                    exportButton.interactable = true;
                    resizeButton.interactable = true;
                    UpdateInfo();
                });
                break;
        }
    }

    void UpdateInfo() 
    {
        widthText.text = imageInfo.Width + "";
        heightText.text = imageInfo.Height + "";
        formatText.text = imageFormat + "";
    }

    void UpdateTexture()
    {
        imagePreview.texture = asyncImage.Texture;
        imagePreview.GetComponent<AspectRatioFitter>().aspectRatio = (float)asyncImage.Width / (float)asyncImage.Height;
    }

    public void Export()
    {
        string extension = "";

        switch (imageFormat)
        {
            case SKEncodedImageFormat.Astc:
                extension = ".astc";
                break;
            case SKEncodedImageFormat.Bmp:
                extension = ".bmp";
                break;
            case SKEncodedImageFormat.Gif:
                extension = ".gif";
                break;
            case SKEncodedImageFormat.Ico:
                extension = ".ico";
                break;
            case SKEncodedImageFormat.Jpeg:
                extension = ".jpeg";
                break;
            case SKEncodedImageFormat.Png:
                extension = ".png";
                break;
            case SKEncodedImageFormat.Wbmp:
                extension = ".wbmp";
                break;
            case SKEncodedImageFormat.Webp:
                extension = ".webp";
                break;
            case SKEncodedImageFormat.Pkm:
                extension = ".pkm";
                break;
            case SKEncodedImageFormat.Ktx:
                extension = ".ktx";
                break;
            case SKEncodedImageFormat.Dng:
                extension = ".dng";
                break;
            case SKEncodedImageFormat.Heif:
                extension = ".heif";
                break;
        }

        var fileExplorer = GetComponent<FileExplorer>();
        string savePath = "";

#if UNITY_STANDALONE_WIN
        fileExplorer.WindowsSaveLocation((path) => asyncImage.Save(path, SKEncodedImageFormat.Png, 50));
#elif UNITY_ANDROID || UNITY_IOS
        savePath = Application.streamingAssetsPath + "/" + DateTime.Now.Millisecond.ToString() + extension;
#endif

    }

}
