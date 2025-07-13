// Assets/Scripts/Customer/CustomerProfile.cs
using UnityEngine;

[CreateAssetMenu(fileName = "NewCustomerProfile", menuName = "Customer/Customer Profile")]
public class CustomerProfile : ScriptableObject
{
    public string customerName;
    [TextArea] public string[] introLines;
    [TextArea] public string orderLine;
    [TextArea] public string[] playerResponseOptions; // 3 options
    
    public string[] reactionLines; // Same length as responseButtons
    public int[] tipModifiers; // e.g. +20, 0, -20
    public string[] emotionalResults; // e.g. "Flattered", "Neutral", "Annoyed"
}
