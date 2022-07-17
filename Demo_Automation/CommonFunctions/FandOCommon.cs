using System.Threading;
using System.Net;
using Utils;
using Microsoft.Playwright; 
using System; 
using System.Threading.Tasks; 
using AventStack.ExtentReports;
namespace Utils
{  
    public static class FandOCommon
    {
        public static async Task FandOLogin(IPage page,ExtentTest test,String FandOUrl, String login , String password)
        {
            await Utilities.PageNavigation(page, "FandOUrl", "FandO Login Page");
            await Task.Delay(10000);
            if (await page.IsVisibleAsync("div[role='button']:has-text('Use another account')")){
                await Utilities.PerformClickAction(page, "UsePasswordInstead", "Use your password instead link");
            }
            await Utilities.EnterDataInTextBox(page, "MSFTLoginAccount", login, "Login ID");
            await Utilities.PerformClickAction(page, "MSFTLoginNextButton", "Next Button");
            await Task.Delay(10000);
            // if (await page.IsVisibleAsync("//*[text()='Use your password instead']")){
            // await Utilities.PerformClickAction(page, "UsePasswordInstead", "Use your password instead link");
            // }
            // await Utilities.PerformClickAction(page, "CRMPassword", "Password Button");
            await Utilities.PerformClickAction(page, "FandOPassword", "Password");
            await Utilities.EnterDataInTextBox(page, "FandOPassword", password, "Password");
            await page.RunAndWaitForNavigationAsync(async () => {
            await Utilities.PerformClickAction(page, "MSFTRE_SignInButton", "Sign In Button");
            });
            await page.RunAndWaitForNavigationAsync(async () => {
            await Utilities.PerformClickAction(page, "MSFTLoginPasswordRemember", "Yes Button");
            });
        }

        public static async Task LogoutFromFandO(IPage page)
        {
            await page.ClickAsync("[id=mectrl_headerPicture]");
            await page.ClickAsync("text=Sign out");
        }

        public static async Task ApplyFilter(IPage page,string BookingNo)
        {
        await Utilities.EnterDataInTextBox(page,"LblFilter",BookingNo, "Booking Number");
        await page.ClickAsync("text=/.*Booking Number \"" + BookingNo + "\".*/");
        await page.WaitForLoadStateAsync();
        await Task.Delay(5000);
        }

    public static async Task ClickOnFandOApp(IPage page, string locator, string AppName)
    {
        await page.RunAndWaitForNavigationAsync(async () => {
        await Utilities.PerformClickAction(page, locator, AppName);
        });
    }

    public static async Task ClickOnFandOsubApp(IPage page, string locator, string AppName)
    {
        await page.RunAndWaitForNavigationAsync(async () => {
        await Utilities.PerformClickAction(page, locator, AppName);
        });
    }
    }
}