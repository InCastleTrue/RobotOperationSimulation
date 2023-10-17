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


        doubleAttackTextMesh.text = $"���� ���"; //���� �ð� *1.5
        dashUpTextMesh.text = $"�뽬 ��ȭ"; // �Ÿ� 1.5��
        beamTextMesh.text = $"��"; // ���׸�����

        doubleAttackInfoPanel.SetActive(false);
        dashUpInfoPanel.SetActive(false);
        beamInfoPanel.SetActive(false);

        doubleAttackTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseOverEvent(doubleAttackInfoPanel));
        doubleAttackTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseExitEvent(doubleAttackInfoPanel));

        dashUpTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseOverEvent(dashUpInfoPanel));
        dashUpTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseExitEvent(dashUpInfoPanel));

        beamTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseOverEvent(beamInfoPanel));
        beamTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseExitEvent(beamInfoPanel));

        doubleAttackExplanMesh.text = $"�ѹ��� 2���� �������� �߻��մϴ�. ��� ���ݿ� �ʿ��� �ð��� 1.5��� �þ�ϴ�.";
        dashUpExplanMesh.text = $"�뽬�� ��ȭ�մϴ�. �뽬 ��� �� ��Ÿ��� 1.5��� �þ�ϴ�.";
        beamExplanMesh.text = $"�������� ��� ���濡 �������¿� ���� �߻��մϴ�. ���� �߻��ϴ� ������ �������� ���ѵ˴ϴ�. R�� ��� ���� �ð� : 10��";


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