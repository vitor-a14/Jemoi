using UnityEngine;
using UnityEngine.UI;

public class ChoiceCard : Card
{
    private bool selected = false;
    public ChoiceManager choiceManager;

    protected override void OnButtonDown()
    {
        base.OnButtonDown();
    }

    protected override void OnButtonUp()
    {
        base.OnButtonUp();

        selected = !selected;
        if(selected)
        {
            if(choiceManager.selectedCards.Count >= choiceManager.maxCards) return;
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            choiceManager.selectedCards.Add(cardObject);
            GetComponent<Image>().color = Color.black;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            choiceManager.selectedCards.Remove(cardObject);
            GetComponent<Image>().color = Color.white;
        }

        choiceManager.UpdateCounter();
    }
}
