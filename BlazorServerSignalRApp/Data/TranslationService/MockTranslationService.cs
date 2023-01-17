using Google.Cloud.Translate.V3;

namespace BlazorServerSignalRApp.Data;

public class MockTranslationService : TranslationService
{
    public override Translation TranslateText(string targetLangCode, string text)
    {
        return new Translation()
        {
            TranslatedText = ""
        };
    }
}
