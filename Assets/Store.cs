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
        return new VirtualGood[] {INSTANT_TRAIN};
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

    /* virtual good */

    public static VirtualGood INSTANT_TRAIN = new SingleUseVG(
        "Instant Train", // name
        "Level's up your player", // description
        INSTANT_TRAIN_ID, // item id
        new PurchaseWithMarket(INSTANT_TRAIN_ID, INSTANT_TRAIN_PRICE));

    /** Virtual Categories **/
    // The muffin rush theme doesn't support categories, so we just put everything under a general category.
    public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory(
            "General", new List<string>(new string[] { INSTANT_TRAIN_ID})
    );
}
