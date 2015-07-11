using System.Collections;
using System.Collections.Generic;
using Soomla.Store;
using UnityEngine;
using UnityEngine.Analytics;

public class ShopEventHandler : MonoBehaviour
{
    public static ShopEventHandler s_ShopEventHandler;

    void Start()
    {
        if (s_ShopEventHandler == null)
        {
            s_ShopEventHandler = this;
            StoreEvents.OnMarketPurchase += OnMarketPurchase;
            StoreEvents.OnUnexpectedErrorInStore += OnUnexpectedErrorInStore;
            SoomlaStore.Initialize(new Store());
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnUnexpectedErrorInStore(string i_Payload)
    {
        Debug.Log("ERROR: " + i_Payload);
    }

    private void OnMarketPurchase(PurchasableVirtualItem i_PurchasableVirtualItem, string i_Payload, Dictionary<string, string> i_Extras)
    {
        //Analytics.Transaction(i_PurchasableVirtualItem.ID, decimal.Parse(Store.INSTANT_TRAIN_PRICE.ToString()), "USD", i_Extras[])
        Debug.Log("OnMarketPurchase() Item ID=" + i_PurchasableVirtualItem.ID);
        switch (i_PurchasableVirtualItem.ID)
        {
            case Store.INSTANT_TRAIN_ID:
                handleInstantTrainPurchased(1);
                break;
        }
    }

    private void handleInstantTrainPurchased(int i_Amout)
    {
        StartCoroutine(sendInstantTrainPurchased(i_Amout));
    }

    private IEnumerator sendInstantTrainPurchased(int i_Amount)
    {
        //m_WaitingForServer = true;
        WWWForm form = new WWWForm();
        form.AddField("id", GameManager.s_GameManger.m_User.ID);
        form.AddField("amount", i_Amount);

        Debug.Log("Sending InstantTrainPurchased to server");
        WWW request = new WWW(GameManager.URL + "addInstantTrain", form);
        yield return request;
        Debug.Log("Recieved response");

        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.Log("ERROR: " + request.error);
        }
        else
        {
            // Check ok response
            Debug.Log(request.text);
            switch (request.text)
            {
                case "ok":
                    GameManager.s_GameManger.m_myTeam.TotalInstantTrain++;
                    break;
                case "null":
                    Debug.Log("WARN: DB out of sync!");
                    // Sync DB
                    break;

                default:
                    // Do nothing
                    break;
            }
        }

        //m_WaitingForServer = false;
    }
}
