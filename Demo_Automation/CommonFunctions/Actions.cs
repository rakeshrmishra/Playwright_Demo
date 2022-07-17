using System.ComponentModel.Design;
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using Utils;
using AventStack.ExtentReports;
using System.Collections;
using System.Reflection.Metadata.Ecma335;
using System.Net;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
public static class Utilities{

  public static async Task PageNavigation(IPage page, string RE_CRMUrl, string PageName)
  {
      await page.GotoAsync(Helper.GetID(RE_CRMUrl));
      page.Dialog += (_, dialog) => dialog.AcceptAsync();
//       await page.WaitForNavigationAsync();
      BasePlaywrightTest.test.Log(AventStack.ExtentReports.Status.Pass,"Navigated Successfully to : "+PageName);
  }
  public static async Task PerformClickAction(IPage page, string locater, string FieldName)
  {
      await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
//       await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await page.ClickAsync(Helper.GetID(locater));
      BasePlaywrightTest.test.Log(AventStack.ExtentReports.Status.Pass,"Clicked Successfully on : "+FieldName);
  }

  public static async Task PerformClickActiononApp(IPage page, string locater, string FieldName)
  {
      await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await page.Frame("AppLandingPage").ClickAsync(Helper.GetID(locater));
      BasePlaywrightTest.test.Log(AventStack.ExtentReports.Status.Pass,"Clicked Successfully on : "+FieldName);
  }

  public static async Task PerformDblClickAction(IPage page, string locater, string FieldName)
  {
      await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
      await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await page.DblClickAsync(Helper.GetID(locater));
      BasePlaywrightTest.test.Log(AventStack.ExtentReports.Status.Pass,"Clicked Successfully on : "+FieldName);
  }

  public static async Task EnterDataInTextBox(IPage page,string locater, string value, string FieldName)
  {
        await page.ClickAsync(Helper.GetID(locater));
        await page.FillAsync(Helper.GetID(locater), value);
        if(FieldName != "Password")
        BasePlaywrightTest.test.Log(AventStack.ExtentReports.Status.Pass,"Value entered on "+FieldName+" as : "+value);
        else
        BasePlaywrightTest.test.Log(AventStack.ExtentReports.Status.Pass,"Value entered on "+FieldName+" as : ********");
  }

  public static async Task SelectOption(IPage page, string FieldName)
  {
        await page.SelectOptionAsync(Helper.GetID("CRMFamilies_PhonePrefences"), new [] {
        Helper.GetValue("CRMFamilies_PhonePrefences")
        });
        BasePlaywrightTest.test.Log(AventStack.ExtentReports.Status.Pass,"Select Option selected as : "+FieldName);
  }

  public static async Task<string> CaptureText(IPage page, string locator)
  {
          var Textelement = await page.WaitForSelectorAsync(Helper.GetID(locator));
          var TextValue = await Textelement.GetAttributeAsync("title");
          BasePlaywrightTest.test.Log(AventStack.ExtentReports.Status.Pass,"Text Captured as : "+TextValue);
          return TextValue;
  }

  public static async Task Validationforexistenceofvalue(IPage page, string TextValue)
  {
          Assert.IsNotNull(TextValue);
          BasePlaywrightTest.test.Log(AventStack.ExtentReports.Status.Pass,"Text verified as : "+TextValue);
  }

  public static async Task ValidationforequalComparision(IPage page, string firstValue, string secondValue)
  {
          Assert.AreEqual(firstValue, secondValue);
          BasePlaywrightTest.test.Log(AventStack.ExtentReports.Status.Pass,"Text verified as : "+firstValue);
  }

  public static async Task PressKeyboardbutton(IPage page, string locater, string buttonName)
  {
          await page.PressAsync(Helper.GetID(locater), buttonName);
          BasePlaywrightTest.test.Log(AventStack.ExtentReports.Status.Pass, buttonName+" button Pressed successfully");
  }

  public static async Task ReportLogger(IPage page,ExtentTest test,Status status, String log)
  {
          var title = await page.TitleAsync();
          string logdetail = title =":" + log;
          test.Log(status,logdetail);
  }
}