using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    [System.Serializable]
    public struct BgmType
    {
        public string name;
        public AudioClip audio;
    }

    // Inspector �� ǥ���� ������� ���
    public BgmType[] BGMList;

    private AudioSource BGM;
    private string NowBGMname = "";

    public Slider volumeSlider; // �����̴��� Inspector���� �����ؾ� �մϴ�.

    void Start()
    {
        BGM = gameObject.AddComponent<AudioSource>();
        BGM.loop = true;
        if (BGMList.Length > 0) PlayBGM(BGMList[0].name);

        // �����̴��� �� ���� �̺�Ʈ�� ���� ���� �Լ� ����
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void PlayBGM(string name)
    {
        if (NowBGMname.Equals(name)) return;

        for (int i = 0; i < BGMList.Length; ++i)
        {
            if (BGMList[i].name.Equals(name))
            {
                BGM.clip = BGMList[i].audio;
                BGM.Play();
                NowBGMname = name;
            }
        }
    }

    // �����̴� ���� �̿��Ͽ� ���� ����
    void ChangeVolume(float volume)
    {
        BGM.volume = volume;
    }
}
