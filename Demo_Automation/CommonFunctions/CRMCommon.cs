using System.Threading;
using System.Net;
using Utils;
using Microsoft.Playwright; 
using System; 
using System.Threading.Tasks; 
using AventStack.ExtentReports;
namespace Utils
{  
  
  public static class CRMCommon
  {
    public static async Task Login(IPage page,ExtentTest test,String RE_CRMUrl, String login , String password)
    {
        await Utilities.PageNavigation(page, "RE_CRMUrl", "CRM Login Page");
        await Utilities.EnterDataInTextBox(page, "MSFTLoginAccount", login, "Login ID");
        await Utilities.PerformClickAction(page, "MSFTLoginNextButton", "Next Button");
        await Task.Delay(10000);
        // if (await page.IsVisibleAsync("//*[text()='Use your password instead']")){
        //     await Utilities.PerformClickAction(page, "UsePasswordInstead", "Use your password instead link");
        // }
        // await Utilities.PerformClickAction(page, "CRMPassword", "Password Button");
        await Utilities.PerformClickAction(page, "MSFTLoginPassword", "Password");
        await Utilities.EnterDataInTextBox(page, "MSFTLoginPassword", password, "Password");
        // await page.RunAndWaitForNavigationAsync(async () => {
        await Utilities.PerformClickAction(page, "MSFTRE_SignInButton", "Sign In Button");
        // });
        await page.RunAndWaitForNavigationAsync(async () => {
        await Utilities.PerformClickAction(page, "MSFTLoginPasswordRemember", "Yes Button");
        });
    }

    public static async Task LogoutFromCRM(IPage page)
    {
        await page.ClickAsync("[id=mectrl_headerPicture]");
        await page.ClickAsync("text=Sign out");
    }

    public static async Task SelectCRMSideMenu(IPage page, string locator, string MenuName)
    {
        await page.RunAndWaitForNavigationAsync(async () => {
        await Task.Delay(30000);   
        await Utilities.PerformClickAction(page, locator, MenuName);
        });
    }


    public static async Task WaitWhileSaving(IPage page) {
        await page.WaitForSelectorAsync("text=Saving...", new PageWaitForSelectorOptions() {
        State = WaitForSelectorState.Hidden
        });
    }

    public static async Task SaveBtn(IPage page)
    {
        await Utilities.PerformClickAction(page, "CRM_Save", "Save Button");
    }
    
    public static async Task SaveAndCloseBtn(IPage page)
    {
        await page.ClickAsync("[aria-label=\"Save & Close\"]");
        BasePlaywrightTest.test.Log(AventStack.ExtentReports.Status.Pass,"Button Clicked as : Save & Close");
    }

    public static async Task popupSaveAndCloseBtn(IPage page)
    {
        await page.ClickAsync("[aria-label=\"Save and Close\"]");
        BasePlaywrightTest.test.Log(AventStack.ExtentReports.Status.Pass,"Button Clicked as : Save and Close");
    }

    public static async Task ClickOnCRMApp(IPage page, string locator, string AppName)
    {
        await page.RunAndWaitForNavigationAsync(async () => {
        await Utilities.PerformClickActiononApp(page, locator, AppName);
        });
    }

        public static async Task QuickFind(IPage page, string locator, string value, string searchsection)
        {
            await Utilities.PerformClickAction(page, locator, searchsection);
            await Utilities.EnterDataInTextBox(page, "Quickfind", value, searchsection);
            await Utilities.PerformClickAction(page, "StartSearch", "Start Search");
        }

        public static async Task DblClickonSearchrecord(IPage page, string locator, string searchsection)
        {
            await Utilities.PerformDblClickAction(page, "ClickOnSearchRecord", "Search record");
        }
        
  }

  
   

     



        

        


    }