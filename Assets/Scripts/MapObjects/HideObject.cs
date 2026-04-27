using UnityEngine;

public class HideObject : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private bool isHiding = false;
    private GameObject playerObject;

    void Update()
    {
        if ((isPlayerInRange || isHiding) && Input.GetKeyDown(KeyCode.Q))
        {
            if (isHiding)
            {
                // 다시 나올 때는 켜고 나서 숨음 상태를 풀어야 해...
                playerObject.SetActive(true);
                isHiding = false;
            }
            else
            {
                // 무조건 이게 먼저야!! 숨는다는 상태를 먼저 기록해 둬야 해!
                isHiding = true;

                // 그다음 플레이어를 꺼야 OnTriggerExit2D에서 기억을 안 지워...
                playerObject.SetActive(false);
            }
        }
    }

    // 플레이어가 박스의 센서 영역에 들어왔을 때 실행돼...
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 들어온 게 '플레이어'가 맞는지 태그로 확인해... (태그 대소문자 주의해 줘...)
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            playerObject = collision.gameObject; // 플레이어의 정보를 기억해 둬...
        }
    }

    // 플레이어가 박스의 센서 영역에서 나갔을 때 실행돼...
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;

            // 숨어있는 상태가 아닐 때만 기억했던 플레이어 정보를 지워야 해...
            // 숨어있을 때 정보를 날려버리면 나중에 다시 켤 때 에러가 나버리니까...
            if (!isHiding)
            {
                playerObject = null;
            }
        }
    }
}