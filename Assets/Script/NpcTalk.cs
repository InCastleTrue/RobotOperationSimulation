using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 요소를 사용하기 위한 네임스페이스 추가

public class NpcTalk : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] dialogueLines; // 대화 내용을 저장할 배열
    private int currentLine = 0; // 현재 대화 인덱스
    private bool playerInRange; // 플레이어가 NPC 근처에 있는지 여부
    private bool isDialogueActive = false; // 대화가 활성화되었는지 여부

    public GameObject dialoguePanel; // 대화 패널
    public Text dialogueText; // 대화 텍스트 UI 요소

    public CameraMove cameraMove;


    void Start()
    {
        cameraMove = FindObjectOfType<CameraMove>();
        
        // 초기에 대화 패널을 비활성화
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && !isDialogueActive)
        {
            // 플레이어가 E 키를 누르면 대화 시작
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartDialogue();
            }
        }
        else if (isDialogueActive)
        {
            // 대화 중에 E 키를 누르면 다음 대화로 진행 또는 대화 종료
            if (Input.GetKeyDown(KeyCode.E))
            {
                ContinueDialogue();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어 콜라이더와 충돌하면 플레이어가 근처에 있는 것으로 간주
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 플레이어 콜라이더와 충돌하지 않으면 플레이어가 근처에 없는 것으로 간주
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void StartDialogue()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        cameraMove.sensitivityX = 0;
        cameraMove.sensitivityY = 0;
        isDialogueActive = true;
        // 대화 패널을 활성화
        dialoguePanel.SetActive(true);
        // 첫 번째 대화 출력
        dialogueText.text = dialogueLines[currentLine];
    }

    void ContinueDialogue()
    {
        currentLine++;

        // 대화가 끝났을 때
        if (currentLine >= dialogueLines.Length)
        {
            EndDialogue();
        }
        else
        {
            // 다음 대화 출력
            dialogueText.text = dialogueLines[currentLine];
        }
    }

    void EndDialogue()
    {
        // 대화가 끝나면 대화 상태를 비활성화하고 초기화
        isDialogueActive = false;
        currentLine = 0;
        // 대화 패널을 비활성화
        dialoguePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        cameraMove.sensitivityX = 2;
        cameraMove.sensitivityY = 2;

    }
}
