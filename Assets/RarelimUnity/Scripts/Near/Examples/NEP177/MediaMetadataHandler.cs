using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Rarelim.Near;
using Newtonsoft.Json;

public class MediaMetadataHandler : MonoBehaviour
{

  public string networkId;
  public string contractId;
  public string tokenId;

  // Define TokenMetadata class according to the NEP177 standard.
  public class TokenMetadata
  {
    public string title { get; set; }
    public string description { get; set; }
    public string media { get; set; }
    public string media_hash { get; set; }
    public object copies { get; set; }
    public object issued_at { get; set; }
    public object expires_at { get; set; }
    public object starts_at { get; set; }
    public object updated_at { get; set; }
    public string extra { get; set; }
    public string reference { get; set; }
    public object reference_hash { get; set; }
  }

  void Start()
  {
    // Get reference to NEP177 script.
    NEP177 nep177 = gameObject.GetComponent<NEP177>();

    StartCoroutine(nep177.TokenMetadata(networkId, contractId, tokenId, (res) =>
    {

      TokenMetadata token = JsonConvert.DeserializeObject<TokenMetadata>(res.ToString());

      StartCoroutine(DownloadImage(token.media));

    }));
  }

  IEnumerator DownloadImage(string MediaUrl)
  {
    using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(MediaUrl))
    {
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
          //   Make sure a `RawImage` component exists on the GameObject this script is referencing.
          this.gameObject.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
          break;
      }
    }
  }
}
