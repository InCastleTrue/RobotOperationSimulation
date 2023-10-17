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

    // Inspector 에 표시할 배경음악 목록
    public BgmType[] BGMList;

    private AudioSource BGM;
    private string NowBGMname = "";

    public Slider volumeSlider; // 슬라이더를 Inspector에서 연결해야 합니다.

    void Start()
    {
        BGM = gameObject.AddComponent<AudioSource>();
        BGM.loop = true;
        if (BGMList.Length > 0) PlayBGM(BGMList[0].name);

        // 슬라이더의 값 변경 이벤트에 볼륨 조절 함수 연결
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

    // 슬라이더 값을 이용하여 볼륨 조절
    void ChangeVolume(float volume)
    {
        BGM.volume = volume;
    }
}
