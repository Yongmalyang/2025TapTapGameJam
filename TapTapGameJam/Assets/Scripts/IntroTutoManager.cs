using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroTutoManager : MonoBehaviour
{
    public List<Sprite> sprites;      // 이미지 목록
    public Image displayImage;        // 가운데 이미지
    public Button leftButton;         // 왼쪽 버튼
    public Button rightButton;        // 오른쪽 버튼

    private int currentIndex = 0;

    void Start()
    {
        UpdateImage(); // 초기 이미지 표시
    }

    public void OnClickLeft()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateImage();
        }
    }

    public void OnClickRight()
    {
        if (currentIndex < sprites.Count - 1)
        {
            currentIndex++;
            UpdateImage();
        }
    }

    private void UpdateImage()
    {
        // 현재 이미지 갱신
        displayImage.sprite = sprites[currentIndex];

        // 양 끝에서 버튼 비활성화
        leftButton.interactable = currentIndex > 0;
        rightButton.interactable = currentIndex < sprites.Count - 1;
    }
}
