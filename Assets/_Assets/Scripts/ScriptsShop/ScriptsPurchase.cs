using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Purchasing;
[Serializable]
public class NonConsumableItem
{
    public string Name;
    public string Description;
    public string Id;
    public float price;
}
[Serializable]
public class ConsumableItem
{
    public string Name1;
    public string Description1;
    public string Id1;
    public float price1;
}

public class ScriptsPurchase : MonoBehaviour, IStoreListener
{
    public GameObject Button_Gold;
    public HatData eventHat;
    IStoreController storeController;
    public NonConsumableItem ncItem;

    public ConsumableItem cItem;

    private void Start()
    {
        SetupBuilder();
    }
    void SetupBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(ncItem.Id, ProductType.NonConsumable);
        builder.AddProduct(cItem.Id1, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        print("Success");
        storeController = controller;
        

    }

    public void NonConsumable_Btn_Pressed()
    {
        //hat
        storeController.InitiatePurchase(ncItem.Id);
    }

    public void Consumable_Btn_Pressed()
    {
        //seed
         storeController.InitiatePurchase(cItem.Id1);
    }

     public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;

        print("Purchase Complete" + product.definition.id);
        if (product.definition.id == ncItem.Id)
        {
            ReceiveEventHat(eventHat);
        }
        else if (product.definition.id == cItem.Id1)
        {
            SeedManager.Instance.AddSeed(500);
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        print("initialize failed" + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        print("initialize failed" + error + message);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        print("Purchase Failed");
    }

    void ReceiveEventHat(HatData hatData)
    {
        string savedEventHats = PlayerPrefs.GetString("EventHatsReceived", "");
        List<string> eventHatList = new List<string>(savedEventHats.Split(','));

        if (!eventHatList.Contains(hatData.id.ToString()))
        {
            eventHatList.Add(hatData.id.ToString());
            PlayerPrefs.SetString("EventHatsReceived", string.Join(",", eventHatList));
            PlayerPrefs.Save();
        }
    }

    



}


