using Fusion;
using UnityEngine;

[System.Flags]
public enum InputButton
{
    LEFT = 1 << 0,    // 0b0001
    RIGHT = 1 << 1,   // 0b0010
    UP = 1 << 2,      // 0b0100
    DOWN = 1 << 3,    // 0b1000
    RESPAWN = 1 << 4, // �߰� ��ɿ� ���� ����
    JUMP = 1 << 5     // �߰� ��ɿ� ���� ����
}

public struct InputData : INetworkInput
{
    public NetworkButtons Buttons;

    public bool GetButton(InputButton button)
    {
        return Buttons.IsSet(button);
    }

    public NetworkButtons GetButtonPressed(NetworkButtons prev)
    {
        return Buttons.GetPressed(prev);
    }

    public bool AxisPressed()
    {
        // ��, ��, ��, �� �� �ϳ��� ���� ��� true ��ȯ
        return GetButton(InputButton.LEFT) || GetButton(InputButton.RIGHT) ||
               GetButton(InputButton.UP) || GetButton(InputButton.DOWN);
    }
}