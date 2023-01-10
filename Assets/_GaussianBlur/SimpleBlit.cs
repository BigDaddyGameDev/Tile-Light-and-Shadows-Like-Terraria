using UnityEngine;

[ExecuteInEditMode]
public class SimpleBlit : MonoBehaviour
{
    //[SerializeField]
    Texture2D _baseTexture;

    //private void Awake()
    //{
    //    _baseTexture = toTexture2D(GetComponent<Camera>().targetTexture);
    //}

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        //_baseTexture = toTexture2D(GetComponent<Camera>().targetTexture);
        Graphics.Blit(GetComponent<Camera>().targetTexture, destination);
    }

    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}
