using System.Collections;
using System.Collections.Generic;
using AllArt.Solana;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Solnet.Wallet;

public class Main : MonoBehaviour
{
    string recipientPublicKey = "4D75HzPsznnuMFKaxRDK9SH8ktyXmVondqjtTJeTEFYF";
    ulong OneSol = 10000000;

    SendLoveButton sendLove;

    ContentContainer content;

    GameObject heart;

    LoveWallet wallet;

    bool canTransfer = false;

    bool hasInitializedBalance = false;

    void Start()
    {
        print("Start");

        sendLove = new SendLoveButton();

        content = new ContentContainer();

        wallet = GetComponent<LoveWallet>();

        heart = GameObject.Find("Heart").gameObject;

        // heart.SetActive(false);

        WebSocketActions.WebSocketAccountSubscriptionAction += (bool istrue) => 
        {
            MainThreadDispatcher.Instance().Enqueue(() => { RefreshBalance(); });
        };
        
        sendLove.buttonComponent.onClick.AddListener(SendTransaction);

        // StartCoroutine(AnimateHeart());

        StartCoroutine(CreateWallet());
    }

    private async void SendTransaction() {
        if (canTransfer == false) return;

        print("SendTransaction");

        content.SetText(
            "Sending Love..."
        );

        canTransfer = false;

        long amount = (long) OneSol / 100;

        var result = await wallet.TransferSol(recipientPublicKey, amount);
    
        content.SetText(
            "Sent to " + recipientPublicKey + ": " + $"{amount}" + " Sol"
        );

        print(result);

        canTransfer = true;
    }

    //  beef popular canvas convince curve decline uphold depend radio address design inmate repeat achieve hat any shop rural safe boss whale grid busy
    // 4D75HzPsznnuMFKaxRDK9SH8ktyXmVondqjtTJeTEFYF
    public IEnumerator CreateWallet() {
        print("CreateWallet");

        string mnemonic = WalletKeyPair.GenerateNewMnemonic();

        print("mnemonic " + mnemonic);

        // string text = "";
        // text += "Your address: 0x123\n";
        // text += "Partner address: 0x456\n";
        // text += "Your balance: 5\n";
        // text += "Partner balance: 10\n";

        content.SetText(
            "Wallet Mnemonic: " + mnemonic
        );

        yield return new WaitForSeconds(0.5f);

        wallet.GenerateWalletWithMnemonic(mnemonic);

        Account account = wallet.wallet.GetAccount(0);

        string publicKey = account.GetPublicKey;

        print(publicKey);

        content.SetText(
            "Public Key: " + publicKey
        );

        yield return new WaitForSeconds(1f);

        wallet.RequestAirdrop(account, OneSol);

        canTransfer = true;

        yield return new WaitForSeconds(2.5f);

        // save mnemonic into local storage

        // save public key into moralis collection

        content.SetText(
            "Requested Airdrop..."
        );
    }

    public IEnumerator AnimateHeart() {
        print("AnimateHeart");

        heart.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        heart.transform.localScale = new Vector3(2, 2, 1);
    
        yield return new WaitForSeconds(0.25f);

        heart.transform.localScale = new Vector3(3, 3, 1);

        yield return new WaitForSeconds(0.25f);

        heart.transform.localScale = new Vector3(4, 4, 1);

        yield return new WaitForSeconds(0.25f);

        heart.transform.localScale = new Vector3(5, 5, 1);

        yield return new WaitForSeconds(0.25f);

        heart.transform.localScale = new Vector3(25, 25, 1);  

        yield return new WaitForSeconds(0.5f);

        heart.transform.localScale = new Vector3(0, 0, 1);
    }

    async void RefreshBalance() {
        print("RefreshBalance");
        
        if (wallet is null) return;

        Account account = wallet.wallet.GetAccount(0);

        double balance = await wallet.GetSolAmmount(account);

        // content.SetText("Wallet Balance: " + $"{balance}" + " SOL");

        MainThreadDispatcher.Instance().Enqueue(() => { content.SetText("Wallet Balance: " + $"{balance}" + " SOL"); });
    
        if (hasInitializedBalance) {
            // bool isIncomingTransaction = true;

            // StartCoroutine(AnimateHeart());

            // content.SetText(
            //     "Receiving Love..."
            // );
        }

        hasInitializedBalance = true;
    }
}
