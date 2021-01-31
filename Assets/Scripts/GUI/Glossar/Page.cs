using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    public int page;
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(WaitUntilInitialized());
    }

    // Update is called once per frame
    void UpdateUI()
    {
        transform.localScale = GlossarStateHolder.GetInstance().GetState() == page ? Vector3.one : Vector3.zero;
    }
    
    
    
    
    private IEnumerator WaitUntilInitialized()
    {
        yield return new WaitUntil(() =>  GlossarStateHolder.GetInstance());
        yield return new WaitUntil(GlossarStateHolder.IsInitialized);
        
        
        GlossarStateHolder.GetInstance().glossarStateHolderChanged.AddListener(UpdateUI);
        UpdateUI();
    }
}
