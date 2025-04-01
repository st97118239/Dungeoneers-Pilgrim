using UnityEngine;

public class CoinStack : MonoBehaviour
{
    // Amount of coins this stack will give when collected
    public int coinAmount = 1;  // Default to 1 coin, but can be adjusted in the Inspector

    // Get the amount of coins this stack gives
    public int GetCoinAmount()
    {
        return coinAmount;
    }
}