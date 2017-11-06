using System;
using Xunit;
using Ittech24.OAuth;
using System.Collections.Generic;
using System.Net.Http;
using Moq;

namespace ittech24.tests
{
    public class OAuthConnection
    {
        [Fact]
        public async void CanRequestToken()
        {
            //creating a token for sending
            var token = new Token
            {
                ConsumerKey = "BKHOB9YKZCA7DNWA0BC3XA2BT3SEWY",
                ConsumerSecret = "OELRFOXTIAON5P7L8KMK6WMURZWZ7K",
                Url = new Uri("https://api.xero.com/oauth/RequestToken")
            };

            var requestToken = new RequestToken(token);
            await requestToken.SendAsync();
            //executing the test
            Assert.NotNull(requestToken.Response.AccessToken);
            Assert.Null(requestToken.Response.Exception);
        }
    }
}
