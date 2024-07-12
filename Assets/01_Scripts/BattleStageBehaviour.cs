using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleStageBehaviour : NetworkBehaviour
{
    [Networked]
    private TickTimer StartTimer { get; set; }
    [Networked]
    private TickTimer StageTimer { get; set; }

    [SerializeField] private Text _stageTimerText;
    [SerializeField] private float _stageTime = 300f;

    public override void Spawned()
    {
        FindObjectOfType<PlayerSpawner>().RespawnPlayers(Runner);
        StartStage();
    }


    public void StartStage()
    {
        SetStartWaitingTime(); 
        GameManager.Instance.SetGameState(GameManager.GameState.Playing);
    }

    public override void FixedUpdateNetwork()
    {
        if (StartTimer.Expired(Runner))
        {
            StageTimer = TickTimer.CreateFromSeconds(Runner, _stageTime);
            GameManager.Instance.AllowAllPlayersInputs();
        }

        if (StageTimer.IsRunning)
        {
            // RPC_FinishLevel();
        }
    }

    private void SetStartWaitingTime()  // 시작 대기 시간 5초 설정
    {
        StartTimer = TickTimer.CreateFromSeconds(Runner, 5);
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    private void RPC_FinishLevel()
    {

    }



}
