using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class UIVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;       // 用于播放视频的 VideoPlayer
    public RawImage videoDisplay;         // 用于显示视频的 RawImage
    public Image overlayImage;            // 用于设置透明度的 Image

    void Start()
    {
        // 设置视频播放器的输出目标为 RawImage
        videoPlayer.targetTexture = new RenderTexture(1920, 1080, 0);
        videoDisplay.texture = videoPlayer.targetTexture;

        // 设置透明度为 50%
        SetTransparency(0.5f);

        // 播放视频
        videoPlayer.Play();
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // 加载下一个场景，场景名称可以根据需要替换
            LoadNextScene();
        }
    }

    // 设置透明度
    void SetTransparency(float alpha)
    {
        // 将 RawImage 的颜色设置为 100% 不透明，视频本身有透明度
        Color videoColor = videoDisplay.color;
        videoColor.a = 1f;  // 确保 RawImage 完全不透明
        videoDisplay.color = videoColor;

        // 将 OverlayImage 的透明度设置为 50%
        Color overlayColor = overlayImage.color;
        overlayColor.a = alpha;  // 设置透明度为 50%
        overlayImage.color = overlayColor;
    }
    void LoadNextScene()
    {
        string nextSceneName = "UI";
        SceneManager.LoadScene(nextSceneName);
    }
}
