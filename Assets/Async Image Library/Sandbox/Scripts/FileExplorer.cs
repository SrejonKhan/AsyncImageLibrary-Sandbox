using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFileBrowser;
using System.IO;
using System;

public class FileExplorer : MonoBehaviour
{
    public void OpenExplorer(Action<string[]> onFullfillment)
    {

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        FileBrowser.SetFilters(false, new FileBrowser.Filter("Images", ".jpg", ".png", ".heic"));
        StartCoroutine(ShowLoadDialogCoroutine(onFullfillment));
#elif UNITY_ANDROID || UNITY_IOS && !UNITY_EDITOR
        AndroidIosPicker(onFullfillment);
#endif
    }


    IEnumerator ShowLoadDialogCoroutine(Action<string[]> onFullfillment)
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: both, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, true, null, null, "Load Files and Folders", "Select");

        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);
        onFullfillment?.Invoke(FileBrowser.Result);
    }

    void AndroidIosPicker(Action<string[]> onFullfillment)
    {
#if UNITY_ANDROID
        // Use MIMEs on Android
        string[] fileTypes = new string[] { "image/*", "video/*" };
#else
        // Use UTIs on iOS
        string[] fileTypes = new string[] { "public.image", "public.movie" };
#endif
        string[] paths = new string[0];

        // Pick image(s) and/or video(s)
        NativeFilePicker.Permission permission = NativeFilePicker.PickMultipleFiles((selectedPaths) =>
        {
            if (selectedPaths == null)
                Debug.Log("Operation cancelled");
            else
            {
                paths = selectedPaths;
            }
            onFullfillment?.Invoke(paths);
        }, fileTypes);
    }

    public void WindowsSaveLocation(Action<string> onPathSelection)
    {
        StartCoroutine(ShowSaveDialogCoroutine(onPathSelection));
    }

    IEnumerator ShowSaveDialogCoroutine(Action<string> onPathSelection)
    {
        FileBrowser.SetFilters(false, new FileBrowser.Filter("Images", ".png"));
        yield return FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.Files, false, null, "Export.png", "Save Location", "Save");
        onPathSelection?.Invoke(FileBrowser.Result[0]);
    }
}
