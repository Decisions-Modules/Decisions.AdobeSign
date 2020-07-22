# Decisions.AdobeSign Integration

### Preparation
You need to create your AdobeSign Application: 
 https://secure.na1.echosign.com/public/static/oauthDoc.jsp 
<br/>Also you can read Adobe API documentation:
https://secure.na1.echosign.com/public/docs/restapi/v6
<br/>&nbsp;&nbsp; https://www.adobe.io/apis/documentcloud/sign/docs.html#!adobedocs/adobe-sign/master/api_usage/send_signing.md

### Creating provider for Adobe Oauth in Decisions
1. Go to ***System > Integration > OAuth > Providers***   ,  click ***ADD OAUTH PROVIDER***
  2. Fill the form<br />
&nbsp;&nbsp;   ***OAuth Version***: OAuth2 <br />
&nbsp;&nbsp;   Select CheckBox ***Get Base API URL from Authorization Response*** <br />
&nbsp;&nbsp;   ***Base Url Field Name***: api_access_point <br />
&nbsp;&nbsp;   ***Token Request Relative URL***: oauth/token <br />
&nbsp;&nbsp;   ***Token Refresh Relative URL***: oauth/refresh <br />
&nbsp;&nbsp;   ***Authorize URL***: https://secure.echosign.com/public/oauth <br />
&nbsp;&nbsp;   ***Callback URL***: http://localhost/decisions/HandleTokenResponse <br />
&nbsp;&nbsp;   Set ***Default Consumer Key*** and ***Default Consumer Secret Key***<br />
 ![screenshot of sample](https://github.com/Decisions-Modules/Decisions.AdobeSign/blob/master/Creating_provider.png)
 
 ### Getting AccessToken in ***Decisions***
  1. Go to ***System > Integration > OAuth > Tokens*** and click ***CREATE TOKEN***.
  2. Set ***Token Name*** value.
  3. Choose the provider you have created.
  4. Set ***Scope*** to ***user_login:account agreement_send:account agreement_write:account agreement_read:account***
  5. Click Request Token. A browser window will be open. Just follow the instructions in it.
![screenshot of sample](https://github.com/Decisions-Modules/Decisions.AdobeSign/blob/master/Creating_token.png)

### Decisions Steps
This module implements 3 Decisions steps: ***Create Agreement***, ***Get Agreement Info*** and  ***Download Document***.

***Create Agreement*** step uploads an agreement pdf-document to AdobeSign service, and send the agreement for signing
***Get Agreement Info*** step retrieves the agreement’s info, so you can check if it has been signed.
***Download Document*** step downloads the signed document.

