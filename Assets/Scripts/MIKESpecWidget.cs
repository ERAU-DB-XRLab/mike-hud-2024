using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MIKESpecWidget : MIKEExpandingWidget
{

    [SerializeField] private GameObject specDataPrefab;
    [SerializeField] private GameObject specEntries;
    [SerializeField] private MIKESpecDataViewer viewer;

    // Start is called before the first frame update
    protected new void Awake()
    {
        base.Awake();
        TSSManager.Main.OnSpecUpdated += AddSpecData;
    }

    public void AddSpecData(SpecData data)
    {
        if(data.id == 0)
        {
            return;
        }
        MIKESpecDataEntry entry = Instantiate(specDataPrefab, specEntries.transform).GetComponent<MIKESpecDataEntry>();
        entry.SetSpecData(data);
        entry.Clicked.AddListener(() => {
            OpenSpecData(entry.SpecData);
        });
    }

    public void OpenSpecData(SpecData data)
    {
        viewer.gameObject.SetActive(true);
        specEntries.gameObject.SetActive(false);
        viewer.View(data);   
    }

    public void GoBack()
    {
        viewer.gameObject.SetActive(false);
        specEntries.gameObject.SetActive(true);
    }

}
