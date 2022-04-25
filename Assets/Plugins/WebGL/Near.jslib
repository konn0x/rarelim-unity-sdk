// Note: jslib file must be in ES5 format.

mergeInto(LibraryManager.library, {
  SignInWithNear: function (networkId, contractId, dappName) {

    var nearApi  = window.nearApi

    var connect = nearApi.connect

    var keyStores = nearApi.keyStores

    var WalletConnection = nearApi.WalletConnection

    var config = {}

    if (UTF8ToString(networkId) == 'testnet') {
      config = {
        networkId: 'testnet',
        keyStore: new keyStores.BrowserLocalStorageKeyStore(),
        nodeUrl: 'https://rpc.testnet.near.org',
        walletUrl: 'https://wallet.testnet.near.org',
        helperUrl: 'https://helper.testnet.near.org',
        explorerUrl: 'https://explorer.testnet.near.org'
      }

      // TODO: Add config for mainnet.
    }

    // Connect to NEAR.
    connect(config).then(function (near) {
      // Create wallet connection.
      var wallet = new WalletConnection(near)

      if (!wallet.isSignedIn()) {
        // Sign in.
        try {
          wallet.requestSignIn([
            UTF8ToString(contractId),
            UTF8ToString(dappName),
            "http://localhost:50144/?nearLogin=success", // optional
            "http://localhost:50144/?nearLogin=failure", // optional
          ])
        } catch (error) {
          console.log(error);
          return
       }
      }
    })
  },

  SignOutFromNear: function (networkId) {

    var nearApi  = window.nearApi

    var connect = nearApi.connect

    var keyStores = nearApi.keyStores

    var WalletConnection = nearApi.WalletConnection

    var config = {}

    if (UTF8ToString(networkId) == 'testnet') {
      config = {
        networkId: 'testnet',
        keyStore: new keyStores.BrowserLocalStorageKeyStore(),
        nodeUrl: 'https://rpc.testnet.near.org',
        walletUrl: 'https://wallet.testnet.near.org',
        helperUrl: 'https://helper.testnet.near.org',
        explorerUrl: 'https://explorer.testnet.near.org'
      }

      // TODO: Add config for mainnet.
    }

    // Connect to NEAR.
    connect(config).then(function (near) {
      // Create wallet connection.
      var wallet = new WalletConnection(near)

      if (wallet.isSignedIn()) {
        // If signed in, sign out.
        try {
          wallet.signOut()
        } catch (error) {
          console.log(error)
          return
       }
      }
    })
  },

  IsSignedInWithNear : function (networkId) {

    var nearApi  = window.nearApi

    var connect = nearApi.connect

    var keyStores = nearApi.keyStores

    var WalletConnection = nearApi.WalletConnection

    var config = {}

    if (UTF8ToString(networkId) == 'testnet') {
      config = {
        networkId: 'testnet',
        keyStore: new keyStores.BrowserLocalStorageKeyStore(),
        nodeUrl: 'https://rpc.testnet.near.org',
        walletUrl: 'https://wallet.testnet.near.org',
        helperUrl: 'https://helper.testnet.near.org',
        explorerUrl: 'https://explorer.testnet.near.org'
      }

      // TODO: Add config for mainnet.
    }

    // Connect to NEAR.
    connect(config).then(function (near) {
      // Create wallet connection.
      var wallet = new WalletConnection(near)

      var isSignedIn = wallet.isSignedIn()

      // Return `isSignedIn` boolean.
      return isSignedIn
    })
  }
})
