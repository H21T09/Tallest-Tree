using System.Threading.Tasks;
using UnityEngine;
using GooglePlayGames;
using Unity.Services.Core;
using GooglePlayGames.BasicApi;
using Unity.Services.Authentication;

public class GoogleIntegration : MonoBehaviour
{
    public string GooglePlayToken;
    public string GooglePlayError;

    public async Task Authenticate()
    {
        PlayGamesPlatform.Activate();
        await UnityServices.InitializeAsync();

        PlayGamesPlatform.Instance.Authenticate((success) =>
        {
            if(success == SignInStatus.Success)
            {
                Debug.Log("login with gg was success");
                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    Debug.Log("Auth code is" + code);
                    GooglePlayToken = code;
                });
            }
            else
            {
                GooglePlayError = "Fail to retrieve GPG auth code";
                Debug.LogError("Login Unsuccessful");
            }
        });
        await AuthenticateWithUnity();
    }

    private async Task AuthenticateWithUnity()
    {
        try
        {
            await AuthenticationService.Instance.SignInWithGoogleAsync(GooglePlayToken);
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
            throw;
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
            throw;
        }
    }
}
