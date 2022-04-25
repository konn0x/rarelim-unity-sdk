using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using Rarelim.Near;

public class TitleMetadataHandler : MonoBehaviour
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

    // Get reference to script.
    NEP177 nep177 = gameObject.GetComponent<NEP177>();

    StartCoroutine(nep177.TokenMetadata(networkId, contractId, tokenId, (res) =>
    {

      TokenMetadata token = JsonConvert.DeserializeObject<TokenMetadata>(res.ToString());

      Debug.Log(token.title);

      // Get reference to TextMeshPro. Note that we reference `TextMeshProUGUI` instead of `TextMeshPro`.
      TextMeshProUGUI textMeshPro = GetComponent<TextMeshProUGUI>();

      textMeshPro.SetText(token.title);

    }));

  }

}
