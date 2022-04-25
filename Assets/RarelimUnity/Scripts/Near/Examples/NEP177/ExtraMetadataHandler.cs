using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using Rarelim.Near;

public class ExtraMetadataHandler : MonoBehaviour
{

  public string networkId;
  public string contractId;
  public string tokenId;

  void Start()
  {

    // Get reference to script.
    NEP177 nep177 = gameObject.GetComponent<NEP177>();

    StartCoroutine(nep177.TokenMetadataExtra(networkId, contractId, tokenId, (res) =>
    {

      dynamic jsonResponse = JsonConvert.DeserializeObject(res.ToString());

      Debug.Log(jsonResponse);

      // Get reference to TextMeshPro. Note that we reference `TextMeshProUGUI` instead of `TextMeshPro`.
      //   TextMeshProUGUI textMeshPro = GetComponent<TextMeshProUGUI>();


    }));

  }

}
