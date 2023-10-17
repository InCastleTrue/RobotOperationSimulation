using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ��Ҹ� ����ϱ� ���� ���ӽ����̽� �߰�

public class NpcTalk : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] dialogueLines; // ��ȭ ������ ������ �迭
    private int currentLine = 0; // ���� ��ȭ �ε���
    private bool playerInRange; // �÷��̾ NPC ��ó�� �ִ��� ����
    private bool isDialogueActive = false; // ��ȭ�� Ȱ��ȭ�Ǿ����� ����

    public GameObject dialoguePanel; // ��ȭ �г�
    public Text dialogueText; // ��ȭ �ؽ�Ʈ UI ���

    public CameraMove cameraMove;


    void Start()
    {
        cameraMove = FindObjectOfType<CameraMove>();
        
        // �ʱ⿡ ��ȭ �г��� ��Ȱ��ȭ
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && !isDialogueActive)
        {
            // �÷��̾ E Ű�� ������ ��ȭ ����
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartDialogue();
            }
        }
        else if (isDialogueActive)
        {
            // ��ȭ �߿� E Ű�� ������ ���� ��ȭ�� ���� �Ǵ� ��ȭ ����
            if (Input.GetKeyDown(KeyCode.E))
            {
                ContinueDialogue();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // �÷��̾� �ݶ��̴��� �浹�ϸ� �÷��̾ ��ó�� �ִ� ������ ����
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // �÷��̾� �ݶ��̴��� �浹���� ������ �÷��̾ ��ó�� ���� ������ ����
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
        // ��ȭ �г��� Ȱ��ȭ
        dialoguePanel.SetActive(true);
        // ù ��° ��ȭ ���
        dialogueText.text = dialogueLines[currentLine];
    }

    void ContinueDialogue()
    {
        currentLine++;

        // ��ȭ�� ������ ��
        if (currentLine >= dialogueLines.Length)
        {
            EndDialogue();
        }
        else
        {
            // ���� ��ȭ ���
            dialogueText.text = dialogueLines[currentLine];
        }
    }

    void EndDialogue()
    {
        // ��ȭ�� ������ ��ȭ ���¸� ��Ȱ��ȭ�ϰ� �ʱ�ȭ
        isDialogueActive = false;
        currentLine = 0;
        // ��ȭ �г��� ��Ȱ��ȭ
        dialoguePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        cameraMove.sensitivityX = 2;
        cameraMove.sensitivityY = 2;

    }
}
