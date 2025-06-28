using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class UIVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;       // ���ڲ�����Ƶ�� VideoPlayer
    public RawImage videoDisplay;         // ������ʾ��Ƶ�� RawImage
    public Image overlayImage;            // ��������͸���ȵ� Image

    void Start()
    {
        // ������Ƶ�����������Ŀ��Ϊ RawImage
        videoPlayer.targetTexture = new RenderTexture(1920, 1080, 0);
        videoDisplay.texture = videoPlayer.targetTexture;

        // ����͸����Ϊ 50%
        SetTransparency(0.5f);

        // ������Ƶ
        videoPlayer.Play();
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // ������һ���������������ƿ��Ը�����Ҫ�滻
            LoadNextScene();
        }
    }

    // ����͸����
    void SetTransparency(float alpha)
    {
        // �� RawImage ����ɫ����Ϊ 100% ��͸������Ƶ������͸����
        Color videoColor = videoDisplay.color;
        videoColor.a = 1f;  // ȷ�� RawImage ��ȫ��͸��
        videoDisplay.color = videoColor;

        // �� OverlayImage ��͸��������Ϊ 50%
        Color overlayColor = overlayImage.color;
        overlayColor.a = alpha;  // ����͸����Ϊ 50%
        overlayImage.color = overlayColor;
    }
    void LoadNextScene()
    {
        string nextSceneName = "UI";
        SceneManager.LoadScene(nextSceneName);
    }
}
