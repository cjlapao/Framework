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
                ConsumerKey = Constants.ConsumerKey,
                ConsumerSecret = Constants.ConsumerSecret,
                Url = new Uri(Constants.RequestTokenNode)
            };

            var requestToken = await new RequestToken(token).SendAsync();
            //executing the test
            Assert.NotNull(requestToken.AccessToken);
            Assert.Null(requestToken.Exception);
        }

        [Fact]
        public async void CanAuthorizeToken()
        {
            //creating the Request Token
            var token = new Token
            {
                ConsumerKey = Constants.ConsumerKey,
                ConsumerSecret = Constants.ConsumerSecret,
                Url = new Uri(Constants.RequestTokenNode)
            };
            var requestToken = await new RequestToken(token).SendAsync();

            Token authoriseToken = null;
            if (requestToken.IsSuccess)
            {
                var authtoken = new Token();
                authtoken.Duplicate(requestToken);
                authtoken.Url = new Uri(Constants.AuthoriseTokenNode);
                authoriseToken = await new AuthorizationToken(authtoken).SendAsync();
            }
            //executing the test
            Assert.True(requestToken.IsSuccess);
            Assert.NotNull(authoriseToken);
            Assert.True(authoriseToken.IsSuccess);
            Assert.NotNull(authoriseToken.AuthorizationUrl);
            Assert.NotNull(authoriseToken.AccessToken);
            Assert.NotNull(authoriseToken.AccessTokenSecret);
        }
    }
}
