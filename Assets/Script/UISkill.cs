using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UISkill : MonoBehaviour
{
    private Player player;

    public Button doubleAttackButton;
    public Button dashUpButton;
    public Button beamButton;

    public TextMeshProUGUI doubleAttackTextMesh;
    public TextMeshProUGUI dashUpTextMesh;
    public TextMeshProUGUI beamTextMesh;
    public TextMeshProUGUI skillPointMesh;


    public TextMeshProUGUI doubleAttackExplanMesh;
    public TextMeshProUGUI dashUpExplanMesh;
    public TextMeshProUGUI beamExplanMesh;

    public GameObject doubleAttackInfoPanel;
    public GameObject dashUpInfoPanel;
    public GameObject beamInfoPanel;





    void Start()
    {

        player = GameObject.Find("Player").GetComponent<Player>();

        doubleAttackButton = GameObject.Find("DoubleAttackButton").GetComponent<Button>();
        dashUpButton = GameObject.Find("DashUpButton").GetComponent<Button>();
        beamButton = GameObject.Find("BeamButton").GetComponent<Button>();


        doubleAttackButton.onClick.AddListener(DoubleAttackUp);
        dashUpButton.onClick.AddListener(DashUp);
        beamButton.onClick.AddListener(BeamUp);


        doubleAttackTextMesh.text = $"이중 사격"; //재사격 시간 *1.5
        dashUpTextMesh.text = $"대쉬 강화"; // 거리 1.5배
        beamTextMesh.text = $"빔"; // 에네르기파

        doubleAttackInfoPanel.SetActive(false);
        dashUpInfoPanel.SetActive(false);
        beamInfoPanel.SetActive(false);

        doubleAttackTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseOverEvent(doubleAttackInfoPanel));
        doubleAttackTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseExitEvent(doubleAttackInfoPanel));

        dashUpTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseOverEvent(dashUpInfoPanel));
        dashUpTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseExitEvent(dashUpInfoPanel));

        beamTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseOverEvent(beamInfoPanel));
        beamTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseExitEvent(beamInfoPanel));

        doubleAttackExplanMesh.text = $"한번에 2번에 에너지를 발사합니다. 대신 재사격에 필요한 시간이 1.5배로 늘어납니다.";
        dashUpExplanMesh.text = $"대쉬를 강화합니다. 대쉬 사용 시 비거리가 1.5배로 늘어납니다.";
        beamExplanMesh.text = $"에너지를 모아 전방에 일자형태에 빔을 발사합니다. 빔을 발사하는 동안은 움직임이 제한됩니다. R로 사용 재사용 시간 : 10초";


    }

    private EventTrigger.Entry CreateMouseOverEvent(GameObject targetObject)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { targetObject.SetActive(true); });
        return entry;
    }

    private EventTrigger.Entry CreateMouseExitEvent(GameObject targetObject)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) => { targetObject.SetActive(false); });
        return entry;
    }

    void Update()
    {
        skillPointMesh.text = $"SkillPoint : {player.skillPoint}";
    }

    void DoubleAttackUp()
    {
        if (player.skillPoint > 0)
        {
            player.skillPoint--;
            player.isDoubleAttackUp = true;
            doubleAttackButton.gameObject.SetActive(false);

        }
    }
    void DashUp()
    {
        if (player.skillPoint > 0)
        {
            player.skillPoint--;
            player.isDashUP = true;
            dashUpButton.gameObject.SetActive(false);
        }
    }
    void BeamUp()
    {
        if (player.skillPoint > 0)
        {
            player.skillPoint--;
            player.isBeamUp = true;
            beamButton.gameObject.SetActive(false);
        }
    }


}