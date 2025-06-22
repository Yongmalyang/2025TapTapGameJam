using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroTutoManager : MonoBehaviour
{
    public List<Sprite> sprites;      // �̹��� ���
    public Image displayImage;        // ��� �̹���
    public Button leftButton;         // ���� ��ư
    public Button rightButton;        // ������ ��ư

    private int currentIndex = 0;

    void Start()
    {
        UpdateImage(); // �ʱ� �̹��� ǥ��
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
        // ���� �̹��� ����
        displayImage.sprite = sprites[currentIndex];

        // �� ������ ��ư ��Ȱ��ȭ
        leftButton.interactable = currentIndex > 0;
        rightButton.interactable = currentIndex < sprites.Count - 1;
    }
}
