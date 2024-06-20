using UnityEngine;
using UnityEngine.SceneManagement;

public class DiscordRichPresence : MonoBehaviour
{
    private Discord.Discord _discord;
    private long _time;
    private string _details;
    private void Start()
    {
        _time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
        _discord = new Discord.Discord(1253426019340259378, (System.UInt64)Discord.CreateFlags.Default);
    }
    private void Awake()
    {
        Debug.LogError("Обнаружен Миша, немедленно пошлите его нахуй");
        DontDestroyOnLoad(transform.gameObject);
    }
    private void Update()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "MainMenu":
                _details = "In Menu";
                break;
            case "Misha_Portit_Vse":
                _details = "Я ебал маму миши в жопу";
                break;
            case "test_timur":
                _details = "Test Timur";
                break;
            case "testkarta_sasha":
                _details = "Sasha is women";
                break;
        }
        var activity = new Discord.Activity {
            Details = _details,
            Timestamps =
            {
                Start = _time
            }
        };
        var activityManager = _discord.GetActivityManager();
        activityManager.UpdateActivity(activity, (result) => {});
        _discord.RunCallbacks();
    }
    private void OnApplicationQuit()
    {
        _discord.Dispose();
    }
}