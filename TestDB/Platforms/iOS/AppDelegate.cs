using Foundation;
using SQLitePCL;

namespace TestDB;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp()
    {
        Batteries_V2.Init();
        raw.SetProvider(new SQLite3Provider_sqlite3());

        return MauiProgram.CreateMauiApp();
    }
}
