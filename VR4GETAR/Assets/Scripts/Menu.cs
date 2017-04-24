using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject saveButton;
    [SerializeField] GameObject loadButton;
    [SerializeField] GameObject clearButton;
    [SerializeField] GameObject col1Button;
    [SerializeField] GameObject col2Button;
    [SerializeField] GameObject col3Button;
    [SerializeField] Material selectMaterial;
    [SerializeField] Material defaultMaterial;

    private CollectionController collection;
    private string currentCollectionName;

    private void Start()
    {
        collection = GameObject.FindGameObjectWithTag("Collection").GetComponent<CollectionController>();
        buttonPressed(col1Button);
    }

    public void buttonPressed(GameObject button)
    {
        if(button == saveButton)
        {
            collection.save(currentCollectionName);
        }
        else if(button == loadButton)
        {
            collection.load(currentCollectionName);
        }
        else if(button == clearButton)
        {
            collection.clear();
        }
        else if (button == col1Button)
        {
            currentCollectionName = "collection1";
            col1Button.GetComponent<Renderer>().material = selectMaterial;
            col2Button.GetComponent<Renderer>().material = defaultMaterial;
            col3Button.GetComponent<Renderer>().material = defaultMaterial;
        }
        else if (button == col2Button)
        {
            currentCollectionName = "collection2";
            col1Button.GetComponent<Renderer>().material = defaultMaterial;
            col2Button.GetComponent<Renderer>().material = selectMaterial;
            col3Button.GetComponent<Renderer>().material = defaultMaterial;
        }
        else if (button == col3Button)
        {
            currentCollectionName = "collection3";
            col1Button.GetComponent<Renderer>().material = defaultMaterial;
            col2Button.GetComponent<Renderer>().material = defaultMaterial;
            col3Button.GetComponent<Renderer>().material = selectMaterial;
        }
    }
}
