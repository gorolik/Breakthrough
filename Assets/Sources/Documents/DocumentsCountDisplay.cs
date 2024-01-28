using UnityEngine;
using TMPro;

public class DocumentsCountDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _field;
    [SerializeField] private string _prefix;
    [SerializeField] private string _postfix;


    private void Start()
    {
        Display();
    }

    private void Display()
    {
        _field.text = _prefix + Documents.GetDocumentsCount() + _postfix;
    }
}
