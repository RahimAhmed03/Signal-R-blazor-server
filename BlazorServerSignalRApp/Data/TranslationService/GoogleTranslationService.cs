using Google.Cloud.Translate.V3;

namespace BlazorServerSignalRApp.Data;

public class GoogleTranslationService : TranslationService
{
    private TranslationServiceClient client;
    private string? parent => Environment.GetEnvironmentVariable("GOOGLE_TRANSLATION_PARENT");

    public GoogleTranslationService()
    {
        this.AvailableLanguages = new List<Language>() {
            new Language() { Name = "Afrikaans", Code = "af" },
            new Language() { Name = "Albanian", Code = "sq" },
            new Language() { Name = "Amharic", Code = "am" },
            new Language() { Name = "Arabic", Code = "ar" },
            new Language() { Name = "Armenian", Code = "hy" },
            new Language() { Name = "Assamese*", Code = "as" },
            new Language() { Name = "Aymara*", Code = "ay" },
            new Language() { Name = "Azerbaijani", Code = "az" },
            new Language() { Name = "Bambara*", Code = "bm" },
            new Language() { Name = "Basque", Code = "eu" },
            new Language() { Name = "Belarusian", Code = "be" },
            new Language() { Name = "Bengali", Code = "bn" },
            new Language() { Name = "Bhojpuri*", Code = "bho" },
            new Language() { Name = "Bosnian", Code = "bs" },
            new Language() { Name = "Bulgarian", Code = "bg" },
            new Language() { Name = "Catalan", Code = "ca" },
            new Language() { Name = "Cebuano", Code = "ceb" },
            new Language() { Name = "Chinese (Simplified)", Code = "zh-CN" },
            new Language() { Name = "Chinese (Traditional)", Code = "zh-TW" },
            new Language() { Name = "Corsican", Code = "co" },
            new Language() { Name = "Croatian", Code = "hr" },
            new Language() { Name = "Czech", Code = "cs" },
            new Language() { Name = "Danish", Code = "da" },
            new Language() { Name = "Dhivehi*", Code = "dv" },
            new Language() { Name = "Dogri*", Code = "doi" },
            new Language() { Name = "Dutch", Code = "nl" },
            new Language() { Name = "English", Code = "en" },
            new Language() { Name = "Esperanto", Code = "eo" },
            new Language() { Name = "Estonian", Code = "et" },
            new Language() { Name = "Ewe*", Code = "ee" },
            new Language() { Name = "Filipino (Tagalog)", Code = "fil" },
            new Language() { Name = "Finnish", Code = "fi" },
            new Language() { Name = "French", Code = "fr" },
            new Language() { Name = "Frisian", Code = "fy" },
            new Language() { Name = "Galician", Code = "gl" },
            new Language() { Name = "Georgian", Code = "ka" },
            new Language() { Name = "German", Code = "de" },
            new Language() { Name = "Greek", Code = "el" },
            new Language() { Name = "Guarani*", Code = "gn" },
            new Language() { Name = "Gujarati", Code = "gu" },
            new Language() { Name = "Haitian Creole", Code = "ht" },
            new Language() { Name = "Hausa", Code = "ha" },
            new Language() { Name = "Hawaiian", Code = "haw" },
            new Language() { Name = "Hebrew", Code = "he or iw" },
            new Language() { Name = "Hindi", Code = "hi" },
            new Language() { Name = "Hmong", Code = "hmn" },
            new Language() { Name = "Hungarian", Code = "hu" },
            new Language() { Name = "Icelandic", Code = "is" },
            new Language() { Name = "Igbo", Code = "ig" },
            new Language() { Name = "Ilocano*", Code = "ilo" },
            new Language() { Name = "Indonesian", Code = "id" },
            new Language() { Name = "Irish", Code = "ga" },
            new Language() { Name = "Italian", Code = "it" },
            new Language() { Name = "Japanese", Code = "ja" },
            new Language() { Name = "Javanese", Code = "jv or jw" },
            new Language() { Name = "Kannada", Code = "kn" },
            new Language() { Name = "Kazakh", Code = "kk" },
            new Language() { Name = "Khmer", Code = "km" },
            new Language() { Name = "Kinyarwanda", Code = "rw" },
            new Language() { Name = "Konkani*", Code = "gom" },
            new Language() { Name = "Korean", Code = "ko" },
            new Language() { Name = "Krio*", Code = "kri" },
            new Language() { Name = "Kurdish", Code = "ku" },
            new Language() { Name = "Kurdish (Sorani)*", Code = "ckb" },
            new Language() { Name = "Kyrgyz", Code = "ky" },
            new Language() { Name = "Lao", Code = "lo" },
            new Language() { Name = "Latin", Code = "la" },
            new Language() { Name = "Latvian", Code = "lv" },
            new Language() { Name = "Lingala*", Code = "ln" },
            new Language() { Name = "Lithuanian", Code = "lt" },
            new Language() { Name = "Luganda*", Code = "lg" },
            new Language() { Name = "Luxembourgish", Code = "lb" },
            new Language() { Name = "Macedonian", Code = "mk" },
            new Language() { Name = "Maithili*", Code = "mai" },
            new Language() { Name = "Malagasy", Code = "mg" },
            new Language() { Name = "Malay", Code = "ms" },
            new Language() { Name = "Malayalam", Code = "ml" },
            new Language() { Name = "Maltese", Code = "mt" },
            new Language() { Name = "Maori", Code = "mi" },
            new Language() { Name = "Marathi", Code = "mr" },
            new Language() { Name = "Meiteilon (Manipuri)*", Code = "mni-Mtei" },
            new Language() { Name = "Mizo*", Code = "lus" },
            new Language() { Name = "Mongolian", Code = "mn" },
            new Language() { Name = "Myanmar (Burmese)", Code = "my" },
            new Language() { Name = "Nepali", Code = "ne" },
            new Language() { Name = "Norwegian", Code = "no" },
            new Language() { Name = "Nyanja (Chichewa)", Code = "ny" },
            new Language() { Name = "Odia (Oriya)", Code = "or" },
            new Language() { Name = "Oromo*", Code = "om" },
            new Language() { Name = "Pashto", Code = "ps" },
            new Language() { Name = "Persian", Code = "fa" },
            new Language() { Name = "Polish", Code = "pl" },
            new Language() { Name = "Portuguese (Portugal, Brazil)", Code = "pt" },
            new Language() { Name = "Punjabi", Code = "pa" },
            new Language() { Name = "Quechua*", Code = "qu" },
            new Language() { Name = "Romanian", Code = "ro" },
            new Language() { Name = "Russian", Code = "ru" },
            new Language() { Name = "Samoan", Code = "sm" },
            new Language() { Name = "Sanskrit*", Code = "sa" },
            new Language() { Name = "Scots Gaelic", Code = "gd" },
            new Language() { Name = "Sepedi*", Code = "nso" },
            new Language() { Name = "Serbian", Code = "sr" },
            new Language() { Name = "Sesotho", Code = "st" },
            new Language() { Name = "Shona", Code = "sn" },
            new Language() { Name = "Sindhi", Code = "sd" },
            new Language() { Name = "Sinhala (Sinhalese)", Code = "si" },
            new Language() { Name = "Slovak", Code = "sk" },
            new Language() { Name = "Slovenian", Code = "sl" },
            new Language() { Name = "Somali", Code = "so" },
            new Language() { Name = "Spanish", Code = "es" },
            new Language() { Name = "Sundanese", Code = "su" },
            new Language() { Name = "Swahili", Code = "sw" },
            new Language() { Name = "Swedish", Code = "sv" },
            new Language() { Name = "Tagalog (Filipino)", Code = "tl" },
            new Language() { Name = "Tajik", Code = "tg" },
            new Language() { Name = "Tamil", Code = "ta" },
            new Language() { Name = "Tatar", Code = "tt" },
            new Language() { Name = "Telugu", Code = "te" },
            new Language() { Name = "Thai", Code = "th" },
            new Language() { Name = "Tigrinya*", Code = "ti" },
            new Language() { Name = "Tsonga*", Code = "ts" },
            new Language() { Name = "Turkish", Code = "tr" },
            new Language() { Name = "Turkmen", Code = "tk" },
            new Language() { Name = "Twi (Akan)*", Code = "ak" },
            new Language() { Name = "Ukrainian", Code = "uk" },
            new Language() { Name = "Urdu", Code = "ur" },
            new Language() { Name = "Uyghur", Code = "ug" },
            new Language() { Name = "Uzbek", Code = "uz" },
            new Language() { Name = "Vietnamese", Code = "vi" },
            new Language() { Name = "Welsh", Code = "cy" },
            new Language() { Name = "Xhosa", Code = "xh" },
            new Language() { Name = "Yiddish", Code = "yi" },
            new Language() { Name = "Yoruba", Code = "yo" },
            new Language() { Name = "Zulu", Code = "zu" },
        };

        var builder = new TranslationServiceClientBuilder()
        {
            JsonCredentials = Environment.GetEnvironmentVariable("GOOGLE_TRANSLATION_CREDENTIAL")
        };
        client = builder.Build();
    }

    public override Translation TranslateText(string targetLangCode, string text)
    {
        TranslateTextRequest request = new TranslateTextRequest
        {
            Contents = { text },
            TargetLanguageCode = targetLangCode,
            Parent = parent
        };
        TranslateTextResponse response = client.TranslateText(request);
        return response.Translations[0];
    }
}
