using TMPro;

public class UI_Loot : UI_Popup
{
    enum Texts
    {
        LootText,
    }

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
    }

    public void ShowLootText(string itemName)
    {
        TextMeshProUGUI tmpUGUI = GetTextMeshProUGUI((int)Texts.LootText);

        if (tmpUGUI == null)
            return;

        tmpUGUI.text = $"{itemName}을(를) 주우려면[E] 키를 누르세요";
    }
}
