using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerBehaviour : NetworkBehaviour
{
    public Transform CameraTransform;

    [Networked]
    public NetworkString<_16> Nickname { get; set; }
    [Networked]
    public Color PlayerColor { get; set; }

    [Networked]
    public int PlayerID { get; private set; }

    private Fusion.Addons.Physics.NetworkRigidbody2D _rb;
    private InputController _inputController;
    private Collider2D _collider;
    private Collider2D _hitCollider;

    [Networked]
    private TickTimer RespawnTimer { get; set; }
    [Networked]
    private NetworkBool Respawning { get; set; }
    [Networked]
    public NetworkBool InputsAllowed { get; set; }

    private ChangeDetector _changeDetector;

    private void Awake()
    {
        _inputController = GetBehaviour<InputController>();
        _rb = GetBehaviour<Fusion.Addons.Physics.NetworkRigidbody2D>();
        _collider = GetComponentInChildren<Collider2D>();
    }

    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState, false);

        PlayerID = Object.InputAuthority.PlayerId;

        if (Object.HasInputAuthority)
        {
            CameraManager camera = FindObjectOfType<CameraManager>();
            // camera.CameraTarget = CameraTransform;

        }
    }
    /*
       [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
    public void RPC_SetNickname(string nick)
    {
        Nickname = nick;
    }
     */



    public void SetInputsAllowed(bool value)
    {
        Debug.Log("isRun SetInputsAllowed");
        InputsAllowed = value;
    }

    private void SetRespawning()
    {
        if (Runner.IsServer)
        {
            _rb.Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    private void SetGFXActive(bool value)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(value);
    }

    public override void FixedUpdateNetwork()
    {
        /*
         if (GetInput<InputData>(out var input) && InputsAllowed)
        {
            if (input.GetButtonPressed(_inputController.PrevButtons).IsSet(InputButton.RESPAWN) && !Respawning)
            {
                RequestRespawn();
            }
        }

        if (Respawning)
        {
            if (RespawnTimer.Expired(Runner))
            {
                _rb.Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                StartCoroutine(Respawn());
            }
        }
         */


    }

    public override void Render()
    {
        foreach (var change in _changeDetector.DetectChanges(this))
        {
            switch (change)
            {
                case nameof(Respawning):
                    SetGFXActive(!Respawning);
                    break;
                case nameof(Nickname):
                    break;
            }
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(.1f);
        Respawning = false;
        SetInputsAllowed(true);
    }

    public void RequestRespawn()
    {
        Respawning = true;
        SetInputsAllowed(false);
        RespawnTimer = TickTimer.CreateFromSeconds(Runner, 1f);
        SetRespawning();
    }

}
