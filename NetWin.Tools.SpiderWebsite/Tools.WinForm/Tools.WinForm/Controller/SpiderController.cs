using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Tools.Services;
using System.Threading.Tasks;
using Shove._IO;
using Tools.Services.Enum;
using Tools.Services.Models;
using System.Windows.Forms;
namespace Tools.WinForm.Controller
{
    public class SpiderController
    {




        private static Dictionary<SpiderEnum.FilterType, List<string>> _filterRule = new Dictionary<SpiderEnum.FilterType, List<string>>();
        private static bool _isFilterTitle = false;//是否筛选标题
        private static bool _isFilterUrl = false;//是否筛选网址





        /// <summary>
        /// 批量插入种子地址
        /// </summary>
        /// <param name="concurrent"></param>
        public static void BatchInsertPrimaryWebSites(ConcurrentDictionary<int, List<string>> concurrent, List<PrimaryWebSiteModel> primaryWebSites)
        {
            List<PrimaryWebSiteModel> primaryWebSiteList = new List<PrimaryWebSiteModel>();
            foreach (var item in concurrent)
            {
                foreach (var value in item.Value)
                {
                    primaryWebSiteList.Add(new PrimaryWebSiteModel()
                    {
                        SourceID = item.Key,
                        WebSiteUrl = value,
                        Level = primaryWebSiteList.Where(p => p.ID == item.Key).Select(p => p.Level + 1).FirstOrDefault(),
                        Status = SpiderEnum.PrimaryStatus.NoneSpider
                    });
                }
            }

            if (primaryWebSiteList.Any())
                DBOperationService.InsertPrimaryWebSite(primaryWebSiteList);
        }

        /// <summary>
        /// 批量插入目标地址
        /// </summary>
        /// <param name="concurrent"></param>
        /// <param name="primaryWebSites"></param>
        public static void BatchInsertTargetWebSites(ConcurrentBag<TargetWebSiteModel> concurrent)
        {
            foreach (var item in concurrent)
            {
                DBOperationService.InsertTargetWebSite(item);
            }
        }

        /// <summary>
        /// html处理
        /// </summary>
        /// <param name="Html"></param>
        /// <param name="WebSiteUrl"></param>
        public static ResultModel<List<string>> HtmlProcess(string Html, string WebSiteUrl, ref string Title, ref int Weights)
        {
            ResultModel<List<string>> resultModel = new ResultModel<List<string>>();
            resultModel.Result = false;
            resultModel.Data = new List<string>();
            var titile = SpiderService.GetTitle(Html);//标题
            Title = titile;
            if ((_isFilterTitle && ValidateContent(titile)) || (_isFilterUrl && ValidateContent(WebSiteUrl)))
            {
                Weights = SpiderService.GetWeights(WebSiteUrl);//权重
                var linkResult = SpiderService.GetLinks(Html);//获取所有网页中的合法链接
                foreach (var item in linkResult.Data)
                {
                    string host = SpiderService.GetHost(item);


                    if (!string.IsNullOrWhiteSpace(host) && item != WebSiteUrl)
                        resultModel.Data.Add(host); //域名
                }

                resultModel.Data = resultModel.Data.Distinct().ToList();//去重
                resultModel.Data.RemoveAll(p => p.Contains(WebSiteUrl));//移除种子域名

                resultModel.Result = true;
            }
            return resultModel;
        }

        /// <summary>
        /// 加载筛选规则
        /// </summary>
        public static void LoadFilterRule()
        {
            _filterRule.Clear();
            var filterRuleResult = DBOperationService.GetFilterRuleConfiguration();
            if (filterRuleResult.Result)
            {
                if (filterRuleResult.Data.Exists(p => p.FilterType == 100))
                {
                    _filterRule.Add(SpiderEnum.FilterType.Contains, filterRuleResult.Data.Where(p => p.FilterType == 100).Select(p => p.FilterKeyWord).ToList());
                }
                if (filterRuleResult.Data.Exists(p => p.FilterType == 200))
                {
                    _filterRule.Add(SpiderEnum.FilterType.StartWith, filterRuleResult.Data.Where(p => p.FilterType == 200).Select(p => p.FilterKeyWord).ToList());
                }
                if (filterRuleResult.Data.Exists(p => p.FilterType == 300))
                {
                    _filterRule.Add(SpiderEnum.FilterType.EndWith, filterRuleResult.Data.Where(p => p.FilterType == 300).Select(p => p.FilterKeyWord).ToList());
                }

                _isFilterTitle = filterRuleResult.Data.Exists(p => p.FilterType == 100);
                _isFilterUrl = filterRuleResult.Data.Exists(p => p.FilterType == 200);
            }
        }

        /// <summary>
        /// 判断是否符合筛选条件
        /// </summary>
        /// <param name="Title"></param>
        /// <returns></returns>
        public static bool ValidateContent(string Content)
        {
            if (string.IsNullOrWhiteSpace(Content))
                return false;

            var result = false;
            foreach (var item in _filterRule)
            {

                switch (item.Key)
                {
                    case SpiderEnum.FilterType.Contains:
                        foreach (var s in item.Value)
                        {
                            result |= Content.Contains(s);
                        }
                        break;
                    case SpiderEnum.FilterType.StartWith:
                        foreach (var s in item.Value)
                        {
                            result |= Content.StartsWith(s);
                        }
                        break;
                    case SpiderEnum.FilterType.EndWith:
                        foreach (var s in item.Value)
                        {
                            result |= Content.EndsWith(s);
                        }
                        break;
                }
                if (result)
                    break;
            }
            return result;
        }


    }
}
