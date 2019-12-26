using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Applitools.Images;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;

namespace XamarinEyesTests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        //IApp app;
        AndroidApp app;
        Platform platform;
        Eyes eyes;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp
                .Android
                .ApkFile("../../libs/selendroid-test-app.apk")
                .EnableLocalScreenshots()
                .PreferIdeSettings()
                .StartApp();

            eyes = new Eyes();
            eyes.ApiKey = "YOUR_API_KEY";
            eyes.SetAppEnvironment("Pixel 3 API 29", "XamarinEyesImagesTest");
        }

        [Test]
        public void AppLaunches()
        {
            try
            {
                eyes.Open("Android-test-one", "Xamarin-UI-Test-demo");
                app.Tap(x => x.Id("startUserRegistration"));
                var eyesImage1 = app.Screenshot("Home Page screen.").FullName;
                Console.WriteLine(eyesImage1);
                eyes.CheckImageFile(eyesImage1);
                app.EnterText(x => x.Id("inputUsername"), "Eyes");
                app.EnterText(x => x.Id("inputEmail"), "hello@applitools.com");
                app.EnterText(x => x.Id("inputPassword"), "ApPl!t0ols");
                app.ClearText((x => x.Id("inputName")));
                app.EnterText(x => x.Id("inputName"), "Hello World");
                app.DismissKeyboard();
                app.Tap(x => x.Id("input_preferedProgrammingLanguage"));
                app.TapCoordinates(79, 1070);
                app.Tap(x => x.Id("input_adds"));
                System.Threading.Thread.Sleep(2);//For Demo.Feel free to remove this line.
                var eyesImage2 = app.Screenshot("After Input- screen.").FullName;

                eyes.CheckImageFile(eyesImage2);
                app.Tap(x => x.Id("btnRegisterUser"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                eyes.Close();
                eyes.AbortIfNotClosed();
            }
        }
    }
}
