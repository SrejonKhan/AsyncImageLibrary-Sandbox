using AsyncImageLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UwrLoadTest : MonoBehaviour
{
    public string remoteImageUrl;
    public RawImage rawImage;

    private void Awake()
    {
        AsyncImage img = new AsyncImage(remoteImageUrl);
        img.OnTextureLoad += () => rawImage.texture = img.Texture;
        img.Load();
    }

    // Start is called before the first frame update
    //IEnumerator Start()
    //{
    //    using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(remoteImageUrl))
    //    {
    //        yield return uwr.SendWebRequest();

    //        if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
    //        {
    //            Debug.LogError("Error - " + uwr.error);
    //        }
    //        else
    //        {
    //            AsyncImage asyncImage = new AsyncImage(uwr.downloadHandler.data);
    //            asyncImage.Load();
    //        }
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
