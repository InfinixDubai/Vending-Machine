/*
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System;
using static System.Net.Mime.MediaTypeNames;

public class weight : MonoBehaviour
{
    [SerializeField] UDPReceive m_udpReceive;
    private Dictionary<int, float> shelfWeights = new Dictionary<int, float>(); // Dictionary to store shelf weights
    public UnityEngine.UI.Text udpText; // Unity Text object to display UDP data    

    private void Awake()
    {
            m_udpReceive.receiveUDP += UDPReceiver;
    }


    public void UDPReceiver(string p_data)
    {
        Debug.Log(p_data);

        /*
        // Update Unity text object with UDP data
        Action updateUI = () => { udpText.text = p_data; };
        UnityThread.executeInUpdate(updateUI);
        /
        // Parse UDP data
        string[] parts = p_data.Split(',');
        if (parts.Length == 2)
        {
            int shelfNum;
            if (int.TryParse(parts[0], out shelfNum))
            {
                float weight;
                if (float.TryParse(parts[1], out weight))
                {
                    // Store weight in dictionary
                    shelfWeights[shelfNum] = weight;

                    // Update Unity text object with UDP data
                    Action updateUI = () => { udpText.text = $"{shelfNum}: {weight}"; };
                    UnityThread.executeInUpdate(updateUI);
                }
            }
        }
    }
}
*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeightDisplay : MonoBehaviour
{
    public UDPReceive udpReceiver; // Reference to the UDP receive script
    public int shelfNumber = 1; // Shelf number this Text object corresponds to

    private Text textComponent; // Reference to the Text component

    void Start()
    {
        // Get the Text component attached to this GameObject
        textComponent = GetComponent<Text>();

        // Make sure the UDP receive script is assigned
        if (udpReceiver == null)
        {
            Debug.LogError("UDP receive script is not assigned to " + gameObject.name);
            return;
        }

        // Subscribe to the UDP receive event
        udpReceiver.receiveUDP += OnReceiveUDP;
    }

    void OnDestroy()
    {
        // Unsubscribe from the UDP receive event
        if (udpReceiver != null)
        {
            udpReceiver.receiveUDP -= OnReceiveUDP;
        }
    }

    // Callback method for UDP receive event
    private void OnReceiveUDP(string udpData)
    {
        // Parse UDP data
        string[] parts = udpData.Split(',');
        if (parts.Length == 2)
        {
            int receivedShelfNumber;
            if (int.TryParse(parts[0], out receivedShelfNumber))
            {
                float weight;
                if (float.TryParse(parts[1], out weight))
                {
                    // Check if the received shelf number matches the assigned shelf number
                    if (receivedShelfNumber == shelfNumber)
                    {
                        // Update the Text object with the weight
                        textComponent.text = $"{shelfNumber}: {weight}";
                    }
                }
            }
        }
    }
}