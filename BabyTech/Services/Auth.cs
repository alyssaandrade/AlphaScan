using System;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;

namespace BabyTech.Services
{
    class Auth
    {
        public static async Task<string> SignIn(string username, string password)
        {
            try
            {
                CognitoUserPool userPool = new CognitoUserPool(AWS.UserPoolId, AWS.ClientId, App.provider);
                CognitoUser user = new CognitoUser(username, AWS.ClientId, userPool, App.provider);

                AuthFlowResponse context = await user.StartWithSrpAuthAsync(new InitiateSrpAuthRequest()
                {
                    Password = password
                }).ConfigureAwait(false);

                if (context.ChallengeName == ChallengeNameType.NEW_PASSWORD_REQUIRED)
                    return "User must change password.";
                else
                {
                    App.user = new AWSUser
                    {
                        IdToken = context.AuthenticationResult?.IdToken,
                        RefreshToken = context.AuthenticationResult?.RefreshToken,
                        AccessToken = context.AuthenticationResult?.AccessToken,
                        TokenIssued = user.SessionTokens.IssuedTime,
                        Expires = user.SessionTokens.ExpirationTime
                    };
                    return "User logged in.";
                }
            }
            catch (Exception err)
            {
                return "Error: " + err.Message;
            }
        }

        public static async Task<string> SignUp(string username, string password, string firstName, string lastName)
        {
            try
            {
                SignUpRequest signUpRequest = new SignUpRequest()
                {
                    ClientId = AWS.ClientId,
                    Password = password,
                    Username = username
                };

                AttributeType firstNameAttribute = new AttributeType()
                {
                    Name = "given_name",
                    Value = firstName
                };

                AttributeType lastNameAttribute = new AttributeType()
                {
                    Name = "family_name",
                    Value = lastName
                };

                signUpRequest.UserAttributes.Add(firstNameAttribute);
                signUpRequest.UserAttributes.Add(lastNameAttribute);

                SignUpResponse signUpResult = await App.provider.SignUpAsync(signUpRequest);

                return signUpResult.UserSub;
            }
            catch (Exception e)
            {
                return "Error " + e.Message;
            }
        }
    }
}
