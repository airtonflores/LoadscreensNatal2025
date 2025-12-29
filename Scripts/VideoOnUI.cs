using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/*Este script é o que adiciona o trailer do FD ao Canva,
ele deverá ser um Raw Image dentro do Canva. Adicione
outline ao objeto Raw Image se for criar uma borda.
*/

public class VideoOnUI : MonoBehaviour
{
    public VideoClip videoClip; // MP4 aqui
    public RawImage rawImage;

    private VideoPlayer videoPlayer;
    private RenderTexture renderTexture;

    void Start()
    {
        // Cria RenderTexture (tamanho inicial genérico)
        renderTexture = new RenderTexture(1920, 1080, 0);

        // Configura RawImage
        rawImage.texture = renderTexture;

        // Adiciona VideoPlayer
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = true;
        videoPlayer.clip = videoClip;

        // Render para a RenderTexture
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        videoPlayer.targetTexture = renderTexture;

        // Quando estiver pronto, ajusta tamanho e dá Play
        videoPlayer.prepareCompleted += OnPrepared;
        videoPlayer.Prepare();
    }

    private void OnPrepared(VideoPlayer vp)
    {
        // Pega resolução real do vídeo
        int width = (int)vp.width;
        int height = (int)vp.height;

        // Recria RenderTexture com resolução correta
        renderTexture.Release();
        renderTexture = new RenderTexture(width, height, 0);
        vp.targetTexture = renderTexture;
        rawImage.texture = renderTexture;

        // Ajusta RawImage para o tamanho do vídeo
        rawImage.rectTransform.sizeDelta = new Vector2(width, height);

        // Dá Play
        vp.Play();
    }
}