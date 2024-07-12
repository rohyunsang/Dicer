using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

namespace Dicer.Fusion {
    public class FusionConnection : MonoBehaviour
    {
        public enum ConnectionStatus
        {
            Disconnected,
            Connecting,
            Failed,
            Connected,
            Loading,
            Loaded
        }

        public bool connectOnAwake = false;
        [HideInInspector] public NetworkRunner _runner = null;
        [SerializeField] private string _mainScenePath = "";
        [SerializeField] private string _battleScenePath = "";
        [SerializeField] private string _battleTestScenePath = "";

        [SerializeField] NetworkObject _commanderPrefab;

        private void Awake()
        {
            if(connectOnAwake)
            {
                ConnectToRunner();
            }
        }

        public async void ConnectToRunner()
        {
            if (_runner == null)
            {
                _runner = gameObject.AddComponent<NetworkRunner>();
            }

            NetworkSceneInfo scene = new NetworkSceneInfo();
            scene.AddSceneRef(SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex));

            var startGameArgs = new StartGameArgs()
            {
                GameMode = GameMode.Shared,
                SessionName = "test",
                Scene = scene,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
            };

            // GameMode.Host = Start a session with a specific name
            // GameMode.Client = Join a session with a specific name
            await _runner.StartGame(startGameArgs);
        }
    }
}

