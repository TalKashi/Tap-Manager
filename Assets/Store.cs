using Soomla.Store;
using System.Collections.Generic;

public class Store : IStoreAssets
{
    public int GetVersion()
    {
        return 0;
    }

    public VirtualCurrency[] GetCurrencies()
    {
        return new VirtualCurrency[0];
    }

    public VirtualGood[] GetGoods()
    {
        return new VirtualGood[] { INSTANT_TRAIN, INSTANT_TRAIN_PACK , FIVE_K_COINS, FIVE_M_COINS, TEN_M_COINS};
    }

    public VirtualCurrencyPack[] GetCurrencyPacks()
    {
        return new VirtualCurrencyPack[0];
    }

    public VirtualCategory[] GetCategories()
    {
        return new VirtualCategory[] { GENERAL_CATEGORY };
    }

    /* Final members ID */
    public const string INSTANT_TRAIN_ID = "inst_train";
    public const double INSTANT_TRAIN_PRICE = 0.99;

    public const string INSTANT_TRAIN_PACK_ID = "inst_train_pack";
    public const double INSTANT_TRAIN_PACK_PRICE = 4.99;

    public const string FIVE_K_COINS_ID = "500k_coins";
    public const double FIVE_K_COINS_PRICE = 0.99;

    public const string FIVE_M_COINS_ID = "5m_coins";
    public const double FIVE_M_COINS_PRICE = 6.99;

    public const string TEN_M_COINS_ID = "10m_coins";
    public const double TEN_M_COINS_PRICE = 9.99;

    /* virtual good */

    public static VirtualGood INSTANT_TRAIN = new SingleUseVG(
        "Instant Train", // name
        "Level's up your player", // description
        INSTANT_TRAIN_ID, // item id
        new PurchaseWithMarket(INSTANT_TRAIN_ID, INSTANT_TRAIN_PRICE));

    public static VirtualGood INSTANT_TRAIN_PACK = new SingleUseVG(
        "Instant Train 7 Pack", // name
        "Level's up your players 7 times", // description
        INSTANT_TRAIN_PACK_ID, // item id
        new PurchaseWithMarket(INSTANT_TRAIN_PACK_ID, INSTANT_TRAIN_PACK_PRICE));

    public static VirtualGood FIVE_K_COINS = new SingleUseVG(
        "500K Game Coins", // name
        "Get's 500K coins in game", // description
        FIVE_K_COINS_ID, // item id
        new PurchaseWithMarket(FIVE_K_COINS_ID, FIVE_K_COINS_PRICE));

    public static VirtualGood FIVE_M_COINS = new SingleUseVG(
        "5M Game Coins", // name
        "Get's 5M coins in game", // description
        FIVE_M_COINS_ID, // item id
        new PurchaseWithMarket(FIVE_M_COINS_ID, FIVE_M_COINS_PRICE));

    public static VirtualGood TEN_M_COINS = new SingleUseVG(
        "10M Game Coins", // name
        "Get's 10M coins in game", // description
        TEN_M_COINS_ID, // item id
        new PurchaseWithMarket(TEN_M_COINS_ID, TEN_M_COINS_PRICE));

    /** Virtual Categories **/
    // The muffin rush theme doesn't support categories, so we just put everything under a general category.
    public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory(
            "General", new List<string>(new string[] { INSTANT_TRAIN_ID, INSTANT_TRAIN_PACK_ID, FIVE_K_COINS_ID, FIVE_M_COINS_ID, TEN_M_COINS_ID })
    );
}
