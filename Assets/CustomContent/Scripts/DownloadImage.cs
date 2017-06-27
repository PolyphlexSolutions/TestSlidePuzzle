using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownloadImage : MonoBehaviour {

    //public RawImage imageToDisplay;
    public string url;
    private PuzzleBoard board;

	// Use this for initialization
	void Start ()
    {
        board = FindObjectOfType<PuzzleBoard>();
        StartCoroutine(isDownloading(url));
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator isDownloading(string url)
    {
        
        var www = new WWW(url);
        
        yield return www;
        
        Texture2D texture = new Texture2D(www.texture.width, www.texture.height, TextureFormat.DXT1, false);

        
        www.LoadImageIntoTexture(texture);
        

        board.downloadedImg = texture;
        board.isImageDownloaded = true;

        www.Dispose();
        www = null;
    }

    public void RunDownload()
    {
        StartCoroutine(isDownloading(url));
    }
}
