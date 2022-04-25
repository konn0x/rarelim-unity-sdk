using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json; // Use Newtonsoft Json || https://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@3.0/manual/index.html

namespace Rarelim.Near {
  public class NEP171: MonoBehaviour {

    private readonly static string NEAR_BASE_URL = "https://api.rarelim.com/near/";

    // Returns the token with the given token ID.
    //
    // PARAMETERS:
    // - networkId: Either 'testnet' or 'mainnet'.
    // - contractId: Contract account.
    // - tokenId: ID for the token to request data for.
    // - callbackOnResult: Function to call with returned data.
    // --------------------------------------------------------------------------------
    public IEnumerator NftToken(string networkId, string contractId,  string tokenId, System.Action<object> callbackOnResult)
    {
      // Construct request URL.
      string requestUrl = NEAR_BASE_URL + networkId + '/' + contractId +  "/nep171/nftToken/" + tokenId;

      using (UnityWebRequest webRequest = UnityWebRequest.Get(requestUrl))
      {
        Debug.Log("Request: " + requestUrl);

        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
        case UnityWebRequest.Result.ConnectionError:
        case UnityWebRequest.Result.DataProcessingError:
            Debug.LogError("Error: " + webRequest.error);
            break;

        case UnityWebRequest.Result.ProtocolError:
            Debug.LogError("HTTP Error: " + webRequest.error);
            break;

        case UnityWebRequest.Result.Success:
            Debug.Log(webRequest.downloadHandler.text);

            object data = JsonConvert.DeserializeObject(webRequest.downloadHandler.text);

            callbackOnResult(data);
            break;
        }
      }
    }
  }
}