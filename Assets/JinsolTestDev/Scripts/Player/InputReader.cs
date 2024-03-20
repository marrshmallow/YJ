using System;
using UnityEngine;
using UnityEngine.InputSystem;

#region 해 줘야 하는 설정들
// 1. InputAction이 Asset > Settings > Input에 생성되어 있을 것.
// 2. 이동은 Value > 2D WASD. 둘러보는 건 Value > Delta.
// 주의사항: 이름! 정말! 중요하다! 코드 쓸 때 헷갈리지 않게 조심.
#endregion

// 이 코드를 플레이어에게 붙여서 입력 신호를 감지합니다.
// Controllers.IPlayerActions는 다른 컨트롤러가 연결되었을 때를 위한 인터페이스입니다.

public class InputReader : MonoBehaviour, Controllers.IPlayerActions
{
    public Vector2 delta;
    public Vector2 moveComposite;
    public Action OnJumpPerformed;
    public Action OnRunPerformed;
    private Controllers controllers;
    private Player player;

    private void OnEnable()
    {
        if (controllers !=null) // 컨트롤러가 연결돼 있다면 컨트롤러 연결해 줄 필요가 없으므로 여기서 중단.
            return;

        controllers = new Controllers(); // 새 인스턴스 생성
        controllers.Player.SetCallbacks(this); // 콜백 호출 설정
        controllers.Player.Enable(); // 입력 활성화

        player = (Player)GetComponent("Player");
    }

    private void OnDisable()
    {
        controllers.Player.Disable(); // 컴포넌트 꺼지면 입력도 비활성화
    }

    // 이것도 전에 OnMove였는데 OnMovement로 바뀌었다ㅏㅏㅏ
    public void OnMovement(InputAction.CallbackContext context)
    {
        // WASD 누를 떄마다 OnMove가 호출되어 매핑대로 Vector2 값을 읽어온다
        moveComposite = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(!context.performed) // 이중 점프 방지용
            return;
        
        OnJumpPerformed?.Invoke();
    }
    public void OnLookAround(InputAction.CallbackContext context)
    {
        delta = context.ReadValue<Vector2>();
    }

    public void OnLookAround_Gamepad(InputAction.CallbackContext context)
    {
        Gamepad.current.leftStick.ReadValue();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        Debug.Log("RUN!" + context);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //player.PlayerInteract(); // 이건 좀 뒤에
        Debug.Log("Interacting..." + context);
        
    }

    public void OnPlayerLookCam(InputAction.CallbackContext context)
    {
        Debug.Log("Looking At Myself" + context);
    }
}
