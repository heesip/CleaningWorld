using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMoveSystem
{
    Joystick joystick;
    Player player;

    [SerializeField] float speed = 5f;
    Vector3 direction;
    Vector3 moveResult;

    public void Initalize(Player player)
    {
        this.player = player;
        joystick = UIManager.Instance.Joystick;
    }

    public void Move()
    {
        //얼리리턴 방식
        if (joystick.IsDrag == false)
        {
            return;
        }

        // 캐릭터 움직임 작성

        direction.x = joystick.Horizontal;
        direction.z = joystick.Vertical;
        direction.Normalize();

        moveResult = speed * Time.deltaTime * direction;
        player.transform.Translate(moveResult, Space.World);
    }

}
