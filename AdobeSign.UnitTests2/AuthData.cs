using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdobeSign.UnitTests
{
    //https://localhost/decisions/HandleTokenResponse?code=CBNCKBAAHBCAABAABiKPfgIFVuvYzC_mLy5m0E437SrUdwBd&api_access_point=https%3A%2F%2Fapi.na2.echosign.com%2F&state=6cf65b3b-d2d7-47ec-89c5-3edaf9796b10&web_access_point=https%3A%2F%2Fsecure.na2.echosign.com%2F
    static class AuthData
    {
        //{"access_token":"3AAABLblqZhBrx6COcZ-wVnJXdb9LHKmlkb02UxPNVxRi8rvNOAgSGfPTmb8u-r4grLifEA36P38r6dYk3UBDBZ1cUTa-2FDm","refresh_token":"3AAABLblqZhCmucYV0c9vynmwLLKE7KtpaLU3jHkLn3vtckyv5qb1q5o_sKE_VLKsvnUkkI-pOrk*","token_type":"Bearer","expires_in":3600}
        //{"access_token":"3AAABLblqZhDISDazC4PGoNXuVXHsSOZz1nvJT30Tu8JTOYex1o2NkNgK8BeMgSw6WouOzphGagTEmoinRzloayLANwRHRXrz","token_type":"Bearer","expires_in":3600}
        public static string access_token = "3AAABLblqZhDiqT4TqH5iqW-LldoQ2DKfv4ivuXQBZrQbnkRWLbJpt1dFUGSwDxBaEHT9kZJI53E_KOCN59HRyk2fXa7T3lEq";
        public static string refresh_token = "3AAABLblqZhCmucYV0c9vynmwLLKE7KtpaLU3jHkLn3vtckyv5qb1q5o_sKE_VLKsvnUkkI-pOrk*";

        public static string AppId = "CBJCHBCAABAA5P7m1KFrsilfXywFWFRWLsOy_IBK_QUS";
        public static string ClientSecret = "YfyKLNKn7PDl7MN-1NIAQNVVqro2z1o3";

        public static string EndPoint = @"https://api.na2.echosign.com";
    }
}
