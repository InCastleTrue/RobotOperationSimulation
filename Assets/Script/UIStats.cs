using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class UIStats : MonoBehaviour
{
    private Player player;

    public Button hpButton;
    public Button reloadSpeedButton;
    public Button attackUpButton;
    public Button dashReloadButton;


    public TextMeshProUGUI levelTextMesh;
    public TextMeshProUGUI expTextMesh;
    public TextMeshProUGUI statsPointMesh;
    public TextMeshProUGUI hpTextMesh;
    public TextMeshProUGUI reloadTextMesh;
    public TextMeshProUGUI attackTextMesh;
    public TextMeshProUGUI dashTextMesh;


    public TextMeshProUGUI hpExplanMesh;
    public TextMeshProUGUI reloadExplanMesh;
    public TextMeshProUGUI attackExplanMesh;
    public TextMeshProUGUI dashExplanMesh;

    public GameObject hpInfoPanel;
    public GameObject reloadInfoPanel;
    public GameObject attackInfoPanel;
    public GameObject dashInfoPanel;

    private int dashReloadLevel = 1;
    private int reloadLevel = 1;



    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player").GetComponent<Player>();


        hpButton = GameObject.Find("HpUpButton").GetComponent<Button>();
        reloadSpeedButton = GameObject.Find("BulletReloadSpeedButton").GetComponent<Button>();
        attackUpButton = GameObject.Find("AttackUpButton").GetComponent<Button>();
        dashReloadButton = GameObject.Find("DashReloadSpeedButton").GetComponent<Button>();

        hpButton.onClick.AddListener(HpUp);
        reloadSpeedButton.onClick.AddListener(ReloadSpeedUp);
        attackUpButton.onClick.AddListener(AttackUp);
        dashReloadButton.onClick.AddListener(DashReloadUp);


        hpInfoPanel.SetActive(false);
        reloadInfoPanel.SetActive(false);
        attackInfoPanel.SetActive(false);
        dashInfoPanel.SetActive(false);

        hpTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseOverEvent(hpInfoPanel));
        hpTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseExitEvent(hpInfoPanel));

        reloadTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseOverEvent(reloadInfoPanel));
        reloadTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseExitEvent(reloadInfoPanel));

        attackTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseOverEvent(attackInfoPanel));
        attackTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseExitEvent(attackInfoPanel));

        dashTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseOverEvent(dashInfoPanel));
        dashTextMesh.gameObject.AddComponent<EventTrigger>().triggers.Add(CreateMouseExitEvent(dashInfoPanel));



    }



    // Update is called once per frame
    void Update()
    {

        levelTextMesh.text = $"Level : {player.level}";
        hpTextMesh.text = $"Hp : {player.playerHp} / {player.playerMaxHp}";
        expTextMesh.text = $"Exp : : {player.exp} / {player.maxExp}";
        statsPointMesh.text = $"Stat Point : {player.status}";

        hpExplanMesh.text = $"Hp �����Դϴ�. �ڽ��� �ִ� ü���� �ø���, Hp�� ���� ȸ���մϴ�.";
        reloadExplanMesh.text = $"������ �ӵ��Դϴ�. ������ �ϰ� �ٽ� �����ϴ� �ð��� ª�����ϴ�.";
        attackExplanMesh.text = $"���ݷ��Դϴ�. �� �Ǵ� ��ֹ��� ������ ���ݷ��� �������ϴ�.";
        dashExplanMesh.text = $"�뽬 ���� �ð��Դϴ�. �뽬�� ���� �ٽ� ����� �� �ִ� �ð��� ª�����ϴ�.";
    }

    public void LevelUp()
    {
        player.status++;
    }
    void HpUp()
    {
        if (player.status > 0)
        {
            player.status--;
            player.playerMaxHp++;
            player.playerHp = player.playerMaxHp;
            hpTextMesh.text = $"Hp : {player.playerHp} / {player.playerMaxHp}";
        }
    }
    void ReloadSpeedUp()
    {
        if (player.status > 0 && reloadLevel < 3)
        {
            player.status--;
            player.bulletReload -= 0.1f;
            reloadLevel++;
            reloadTextMesh.text = $"Reload Level : {reloadLevel}";
        }
    }
    void AttackUp()
    {
        if (player.status > 0)
        {
            player.status--;
            player.damage++;
            attackTextMesh.text = $"Attack : {player.damage}";
        }
    }
    void DashReloadUp()
    {
        if (player.status > 0 && dashReloadLevel < 10)
        {
            player.status--;
            player.dashReload -= 0.1f;
            dashReloadLevel++;
            dashTextMesh.text = $"Dash Level : {dashReloadLevel}";
        }
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

}
