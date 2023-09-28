using Decisions.OAuth;

namespace AdobeSignature.UnitTests
{
    static class AuthData
    {
        public static OAuthToken AccessToken = new OAuthToken()
        {
            TokenId = "UnitTests",
            TokenData =
                "3AAABLblqZhB-XK1FLBN1b3qyk3OqIO6kofX0bGsmO927m9OaGARF7Xz6m0KP_f5UDr-aSrv-xD74dDsCH_udaNpbRf9gm9X8"
        };
        
        public static string AppId = "CBJCHBCAABAA5P7m1KFrsilfXywFWFRWLsOy_IBK_QUS";
        public static string ClientSecret = "YfyKLNKn7PDl7MN-1NIAQNVVqro2z1o3";

        public static string BaseUrl = @"https://api.na2.echosign.com";
    }
}
