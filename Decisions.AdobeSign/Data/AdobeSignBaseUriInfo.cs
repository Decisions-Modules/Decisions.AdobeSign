namespace Decisions.AdobeSign
{
    /// <summary>
    /// Matches the return for retrieving region specific base uri information as documented here:
    ///     https://secure.na3.adobesign.com/public/docs/restapi/v6#!/baseUris/baseUris
    /// </summary>
    public class AdobeSignBaseUriInfo
    {
        //The access point from where other APIs need to be accessed. In case other APIs are accessed
        //from a different end point, it will be considered an invalid request
        public string apiAccessPoint { get; set; }
        
        //The access point from where Acrobat Sign website can be be accessed
        public string webAccessPoint { get; set; }
    }
}