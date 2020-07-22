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
This module has some data structures and implements 3 Decisions steps: ***Create Agreement***, ***Get Agreement Info*** and  ***Download Document***. 

#### Most important data structures
***AdobeSignAgreementCreationData*** data structure is used to create an agreement. <br />
 ![screenshot of sample](https://github.com/Decisions-Modules/Decisions.AdobeSign/blob/master/AdobeSignAgreementCreationData.png)
 
There are two options for Agreement Info Types: ***Simplified*** and ***Full***.<br />
***Simplified*** allows you to add some Agreement Recipients and that’s it.<br />
***Full*** gives you full control of ***AdobeSignAgreementInfo***'s properties. Read AdobeSign documentation to use it.<br />

***AdobeSignAgreementInfo*** data structure holds all agreement's properties. A signed document has ***SIGNED*** value in ***Status*** property.

#### Steps
***Create Agreement*** step uploads an agreement pdf - document to AdobeSign service, and send the agreement for signing<br />

***Get Agreement Info*** step retrieves the agreement’s info, so you can check if it has been signed. A signed document has ***SIGNED*** value in ***Status*** property<br />

***Download Document*** step downloads the signed document.<br />



