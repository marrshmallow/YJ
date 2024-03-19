using System;

public class MiscEvents // 오브젝트에 넣을 거 아니라서
{
    public event Action onTokenCollected; // 토큰을 획득했다는 이벤트 발생

    public void TokenCollected()
    {
        if (onTokenCollected !=null) // 획득 안 한 게 아니라면
            onTokenCollected();
    }

    // 이 밑으로 저런 식으로 기타 등등 내용 추가 가능
}
