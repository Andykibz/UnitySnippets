using UnityEngine;
using NReco.VideoConverter;
using System.Threading;
using System.IO;

public class VideoCapture : MonoBehaviour
{
    private string streamUrl = "https://zoneminder.clarkson.edu/cgi-bin-zm/zms?width=1920px&height=1080px&mode=jpeg&maxfps=30&monitor=15&user=viewer1&pass=media4u";
    private FFMpegConverter ffmpeg;
    Texture2D tex;
    [SerializeField] MeshRenderer _renderer;
    MemoryStream stream;
    Thread HLSthread;

    void Start()
    {
        stream = new MemoryStream();        
        ffmpeg = new FFMpegConverter();
        /*
        // Set Licence, executable amd toolpath
        ffmpeg.FFMpegToolPath
        ffmpeg.FFMpegExeName
        //ffmpef.
        */
        HLSthread = new Thread(GetFrames);
        HLSthread.Start();
        tex = new Texture2D(1, 1);

    }
    
    private void OnDisable() {
        HLSthread.Abort();
    }

    // Has a continuous loop that gets the last frame from the HLS stream
    private void GetFrames(object obj)
    {
        while (true){
            ffmpeg.GetVideoThumbnail(streamUrl, stream);            
        }
    }


    private void LateUpdate()
    {
        ReloadTexture();
    }

    /// <summary>
    /// Reload latest frame into a texture and apply to image plane, 
    /// rescale plane to match image aspect ratio
    /// </summary>
    void ReloadTexture()
    {
        if (stream.Length > 0)
        {
            print(stream.Length);
            tex.LoadImage(stream.ToArray());
            stream = new MemoryStream(); //Clear the Memory Stream
            tex.Apply();

            _renderer.material.mainTexture = tex;
        }
    }


}
