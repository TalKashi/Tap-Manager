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
            case Store.INSTANT_TRAIN_PACK_ID:
                handleInstantTrainPurchased(6);
                break;
            case Store.FIVE_K_COINS_ID:
                handleCoinPurchased(500000);
                break;
            case Store.FIVE_M_COINS_ID:
                handleCoinPurchased(5000000);
                break;
            case Store.TEN_M_COINS_ID:
                handleCoinPurchased(10000000);
                break;

        }
    }

    private void handleInstantTrainPurchased(int i_Amout)
    {
        StartCoroutine(sendInstantTrainPurchased(i_Amout));
    }

    private void handleCoinPurchased(int i_Amout)
    {
        StartCoroutine(sendCoinsPurchased(i_Amout));
    }

    private IEnumerator sendInstantTrainPurchased(int i_Amount)
    {
        string erorMsg =
            @"We failed to complete your purchase.
Please send mail to team.vanilla.dev@gmail.com so we can figure it out
User: " + GameManager.s_GameManger.m_User.ID;
        GameManager.s_GameManger.IsLoadingData = true;
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
            MyUtils.DisplayErrorMessage(GameManager.s_GameManger.m_GenericPopup, erorMsg);
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
                    MyUtils.DisplayErrorMessage(GameManager.s_GameManger.m_GenericPopup, erorMsg);
                    break;

                default:
                    MyUtils.DisplayErrorMessage(GameManager.s_GameManger.m_GenericPopup, erorMsg);
                    break;
            }
        }

        GameManager.s_GameManger.IsLoadingData = false;
    }

    private IEnumerator sendCoinsPurchased(int i_Amount)
    {
        string erorMsg =
            @"We failed to complete your purchase.
Please send mail to team.vanilla.dev@gmail.com so we can figure it out
User: " + GameManager.s_GameManger.m_User.ID;
        GameManager.s_GameManger.IsLoadingData = true;
        WWWForm form = new WWWForm();
        form.AddField("id", GameManager.s_GameManger.m_User.ID);
        form.AddField("money", i_Amount);

        Debug.Log("Sending InstantTrainPurchased to server");
        WWW request = new WWW(GameManager.URL + "addMoney", form);
        yield return request;
        Debug.Log("Recieved response");

        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.Log("ERROR: " + request.error);
            MyUtils.DisplayErrorMessage(GameManager.s_GameManger.m_GenericPopup, erorMsg);
        }
        else
        {
            // Check ok response
            Debug.Log(request.text);
            switch (request.text)
            {
                case "ok":
                    GameManager.s_GameManger.m_User.Money += i_Amount;
                    break;
                case "null":
                    Debug.Log("WARN: DB out of sync!");
                    MyUtils.DisplayErrorMessage(GameManager.s_GameManger.m_GenericPopup, erorMsg);
                    break;

                default:
                    MyUtils.DisplayErrorMessage(GameManager.s_GameManger.m_GenericPopup, erorMsg);
                    break;
            }
        }

        GameManager.s_GameManger.IsLoadingData = false;
    }
}
