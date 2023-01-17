using Google.Cloud.Translate.V3;

namespace BlazorServerSignalRApp.Data;

public class TranslationService
{
    public virtual Translation TranslateText(string targetLangCode, string text)
    {
        throw new NotImplementedException();
    }

    public List<Language> AvailableLanguages = new List<Language>() { };
}
