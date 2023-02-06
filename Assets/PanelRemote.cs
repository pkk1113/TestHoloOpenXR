using Microsoft.MixedReality.OpenXR.Remoting;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PanelRemote : MonoBehaviour
{
    [SerializeField] MeshRenderer lamp;
    [SerializeField] TMP_InputField field;

    RemotingConnectConfiguration config = new RemotingConnectConfiguration();

    public void Start()
    {
        config.RemoteHostName = "192.168.53.119";
        config.RemotePort = 8265;
        config.MaxBitrateKbps = 200000;

        AppRemoting.Connected += AppRemoting_Connected;
        AppRemoting.Disconnecting += AppRemoting_Disconnecting;

        AppRemoting.TryGetConnectionState(out ConnectionState state, out DisconnectReason reason);
    }

    private void AppRemoting_Connected()
    {
        TurnOnLamp();
    }

    private void AppRemoting_Disconnecting(DisconnectReason disconnectReason)
    {
        TurnOffLamp();
        field.text = disconnectReason.ToString();
    }

    public void OnConnectButton()
    {
        AppRemoting.Disconnect();
        field.text = "loading...";

        AppRemoting.StartConnectingToPlayer(config);
    }

    public void OnDisconeectButton()
    {
        AppRemoting.Disconnect();
    }

    private void TurnOnLamp()
    {
        lamp.material.color = Color.green;
    }

    private void TurnOffLamp()
    {
        lamp.material.color = Color.red;
    }
}
