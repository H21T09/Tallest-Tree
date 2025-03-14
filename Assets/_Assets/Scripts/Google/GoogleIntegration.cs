using System.Threading.Tasks;
using UnityEngine;
using GooglePlayGames;
using Unity.Services.Core;
using GooglePlayGames.BasicApi;
using Unity.Services.Authentication;
using System.Reflection;

public class GoogleIntegration : MonoBehaviour
{
    public static GoogleIntegration Instance;
    public string GooglePlayToken;
    public string GooglePlayError;
    
    public bool connectedToGooglePlay;


    
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
        }
    

    private void Start()
    {
        LogInToGooglePlay();
    }

   void LogInToGooglePlay()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    private void ProcessAuthentication(SignInStatus status)
    {
        if(status == SignInStatus.Success)
        {
            connectedToGooglePlay = true;
        }
        else connectedToGooglePlay =false;
    }

    public void UpdateLeaderboard()
    {
        if (connectedToGooglePlay)
        {

            string leaderboardID = "CgkIxs6L2YscEAIQAg";
            // Đẩy điểm lên Leaderboard
            Social.ReportScore(SeedManager.Instance.totalseed, leaderboardID, (bool success) =>
            {
                if (success)
                    Debug.Log("Total Seed updated to Leaderboard successfully!");
                else
                    Debug.LogError("Failed to update Total Seed to Leaderboard.");
            });
        }
    }

    public void UnlockAchievement(string achievementID)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(achievementID, 100.0f, success => {
                if (success)
                    Debug.Log("Mở khóa thành tích thành công!");
                else
                    Debug.Log("Lỗi khi mở khóa thành tích!");
            });
        }
    }
    public void ShowLeaderBoard()
    {
        if (!connectedToGooglePlay) LogInToGooglePlay();
        Social.ShowLeaderboardUI();
    }

    public void ShowAchievements()
    {
        if (!connectedToGooglePlay) LogInToGooglePlay();
        Social.ShowAchievementsUI();
    }



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
