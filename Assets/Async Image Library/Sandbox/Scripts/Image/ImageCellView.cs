using AsyncImageLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCellView : MonoBehaviour
{
    public AsyncImage asyncImage;
    public int spinSpeed = 3;
    public Transform spinnerTransform;
    public Text msgText;
    public RawImage image;
    public string info;

    private bool doSpin = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    Vector3 rotateAngle = new Vector3(0, 0, -45);

    // Update is called once per frame
    void Update()
    {
        if(doSpin) spinnerTransform.Rotate(rotateAngle * Time.deltaTime * spinSpeed);
    }

    public void ToggleSpinner() 
    {
        doSpin = !doSpin;
        spinnerTransform.gameObject.SetActive(doSpin);
    }

    public void OpenExportPanel()
    {
        if (asyncImage == null) return;

        ImageExporter.instance.OpenExportPanel(asyncImage);
    }
}
