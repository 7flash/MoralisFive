using UnityEngine;
// WalletBaseComponent
using AllArt.Solana;
using Solnet.Wallet;

public class LoveWallet : WalletBaseComponent {
    public LoveWallet() {
        this.autoConnectOnStartup = true;
        this.clientSource = EClientUrlSource.EDevnet;
    }

    public Wallet GenerateWalletWithMnemonic(string mnemonic) {
        return GenerateWalletWithMenmonic(mnemonic); // TODO: fix typo in original library
    }
}