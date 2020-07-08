using AdobeSign.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdobeSign.ClassLibrary
{
    public static class AdobeSignService
    {
        public static string SendAgreement(AdobeSignConnection connection, AdobeSignToken ApiToken, AgreementModel agreement)
        {
            ApiClient apiClient = new ApiClient(
                connection.ClientID,
                connection.ClientSecret);
            apiClient.SetAccessToken(
                       ApiToken.AccessToken,
                       ApiToken.RefreshToken,
                       ApiToken.TokenExpiration,
                       ApiToken.ApiAccessPoint);

            string docID = CreateTransientDocument(apiClient, agreement.File, agreement.FileName);

            List<Agreements.Model.ParticipantSetInfo> participantsSetInfo = new List<Agreements.Model.ParticipantSetInfo>();
            foreach (var recipient in agreement.Recipients)
            {

                var participantInfo = new Agreements.Model.ParticipantSetInfo()
                {
                    MemberInfos = new List<Agreements.Model.ParticipantSetMemberInfo>()
                                {
                                     new Agreements.Model.ParticipantSetMemberInfo()
                                     {
                                         Email = recipient.Email,
                                     }
                                },
                    Name = recipient.Name,
                    Role = "SIGNER",
                    Order = recipient.Order
                };
                participantsSetInfo.Add(participantInfo);

            }

            var c = apiClient.GetAgreementsApi();
            var r = c.CreateAgreement(new Agreements.Model.AgreementCreationInfo()
            {
                FileInfos = new List<Agreements.Model.FileInfo>()
                        {
                            new Agreements.Model.FileInfo()
                            {
                                 TransientDocumentId = (string)docID
                            }
                        },
                Name = agreement.AgreementName,
                ParticipantSetsInfo = participantsSetInfo,
                EmailOption = new Agreements.Model.EmailOption()
                {
                    SendOptions = new Agreements.Model.SendOptions()
                    {
                        InitEmails = "ALL",
                        InFlightEmails = "NONE",
                        CompletionEmails = "ALL",
                    }
                },
                SignatureType = "ESIGN",
                State = "IN_PROCESS"
            });
            return r.Id;
        }


        public static string CreateTransientDocument(ApiClient apiClient, byte[] file, string fileName)
        {
            var c = apiClient.GetTransientDocumentsApi();



            var r = c.CreateTransientDocument(
                    file,
                    fileName,
                    "application/PDF");


            return r.TransientDocumentId;
        }

        public static AdobeSignToken GetAuthorizationToken(AdobeSignConnection connection)
        {
            ApiClient apiClient = new ApiClient(
                connection.ClientID,
                connection.ClientSecret);

            Model.ApiToken token = apiClient.GetAccessToken(connection.RedirectURL, connection.Code, apiClient.ApiAccessPoint);
            return new AdobeSignToken() { AccessToken = token.AccessToken, RefreshToken = token.RefreshToken, TokenExpiration = apiClient.TokenExpireDate.Value, TokenType = token.TokenType, ApiAccessPoint = apiClient.ApiAccessPoint };
        }

        public static Agreements.Model.AgreementInfo GetAgreementInfo(AdobeSignConnection connection, AdobeSignToken ApiToken, string agreementID)
        {
            ApiClient apiClient = new ApiClient(
                connection.ClientID,
                connection.ClientSecret);

            apiClient.SetAccessToken(
                      ApiToken.AccessToken,
                      ApiToken.RefreshToken,
                      ApiToken.TokenExpiration,
                      ApiToken.ApiAccessPoint);

            var c = apiClient.GetTransientDocumentsApi();

            var agreementApi = new AdobeSign.Agreements.Api.AgreementsApi(apiClient);
            var agreementInfo = agreementApi.GetAgreementInfo(agreementID);
            return agreementInfo;
        }

        public static byte[] GetTransientDocument(AdobeSignConnection connection, AdobeSignToken ApiToken, string agreementID)
        {
            ApiClient apiClient = new ApiClient(
                connection.ClientID,
                connection.ClientSecret);

            apiClient.SetAccessToken(
                      ApiToken.AccessToken,
                      ApiToken.RefreshToken,
                      ApiToken.TokenExpiration,
                      ApiToken.ApiAccessPoint);

            var c = new AdobeSign.Agreements.Api.AgreementsApi(apiClient);
            var bytes = c.GetCombinedDocument(agreementID);

            return bytes;
        }
    }
}
