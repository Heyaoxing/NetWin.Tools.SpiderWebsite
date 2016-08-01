using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpiderWebsite.DAL;
using System.Data;
using Tools.Services.Enum;
using Tools.Services.Models;
namespace Tools.Services
{
    /// <summary>
    /// 数据操纵类
    /// </summary>
    public class DBOperationService
    {
        #region 种子地址操作
        /// <summary>
        /// 获取种子地址集
        /// </summary>
        /// <param name="level">深度</param>
        /// <returns></returns>
        public static ResultModel<List<PrimaryWebSiteModel>> GetPrimaryWebSites(int level)
        {
            return GetPrimaryWebSites(level, 0, 50);
        }

        /// <summary>
        /// 获取种子地址集
        /// </summary>
        /// <param name="level">深度</param>
        /// <param name="LimitStart">从哪里开始获取</param>
        /// <param name="LimitCount">共获取几条</param>
        /// <returns></returns>
        public static ResultModel<List<PrimaryWebSiteModel>> GetPrimaryWebSites(int level, int LimitStart, int LimitCount)
        {
            ResultModel<List<PrimaryWebSiteModel>> resulrModel = new ResultModel<List<PrimaryWebSiteModel>>();
            resulrModel.Result = true;
            resulrModel.Message = "成功";
            resulrModel.Data = new List<PrimaryWebSiteModel>();

            Tables.t_primary_websites entity = new Tables.t_primary_websites();
            var dt = entity.Open("ID,WebSite_Url,Source_ID,Level", "Status=100 and Level<=" + level, "", LimitStart, LimitCount);
            if (dt == null)
            {
                resulrModel.Message = "数据查询失败!";
                resulrModel.Result = false;
                return resulrModel;
            }
            if (dt.Rows.Count == 0)
            {
                resulrModel.Message = "未找到符合条件的数据!";
                resulrModel.Result = false;
                return resulrModel;
            }
            foreach (DataRow dr in dt.Rows)
            {
                var url = dr["WebSite_Url"];
                if (url != null)
                {
                    resulrModel.Data.Add(new PrimaryWebSiteModel()
                    {
                        ID = dr["ID"] != null ? Int32.Parse(dr["ID"].ToString()) : 0,
                        Level = dr["Level"] != null ? Int32.Parse(dr["Level"].ToString()) : 0,
                        SourceID = dr["Source_ID"] != null ? Int32.Parse(dr["Source_ID"].ToString()) : 0,
                        WebSiteUrl = url.ToString(),
                    });
                }
            }
            return resulrModel;
        }

        /// <summary>
        /// 更新抓取状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Staus"></param>
        public static void UpdatePrimaryStatus(List<int> Id, SpiderEnum.PrimaryStatus Staus)
        {
            Shove.DatabaseFactory.Factory facotry = new Shove.DatabaseFactory.FactoryManager().factory;
            string sql = "update T_Primary_WebSites set Status=" + (int)Staus + " where id in (" + string.Join(",", Id) + ")";
            try
            {
                facotry.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                facotry.Close();
            }
        }

        /// <summary>
        /// 插入种子地址数据
        /// </summary>
        /// <param name="model"></param>
        public static ResultModel InsertPrimaryWebSite(List<PrimaryWebSiteModel> models)
        {
            ResultModel resultModel = new ResultModel();
            resultModel.Result = false;
            if (models == null && !models.Any())
            {
                resultModel.Message = "无种子地址值!";
            }
            try
            {
                StringBuilder sb=new StringBuilder();
                foreach (var model in models)
                {
                    sb.AppendFormat(
                        "INSERT INTO T_Primary_WebSites(WebSite_Url,Level,Status,create_time,is_erased,Source_ID)SELECT '{0}',{1},{2},'{3}',{4},{5} FROM DUAL where NOT EXISTS(SELECT 1 FROM T_Primary_WebSites WHERE WebSite_Url='{0}') and NOT EXISTS(SELECT 1 FROM T_Exclude_WebSites WHERE WebSite_Url='{0}') LIMIT 1;",
                        model.WebSiteUrl, model.Level, (int) model.Status, DateTime.Now, 0, model.SourceID);
                }
                Shove.Database.MySQL.ExecuteNonQuery(sb.ToString());
                resultModel.Result = true;
            }
            catch (Exception ex)
            {
                resultModel.Message = ex.Message;
            }
            return resultModel;
        }

        /// <summary>
        /// 判断是否存在种子地址表
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static ResultModel IsExistsPrimaryWebSite(string url)
        {
            ResultModel resultModel = new ResultModel();
            resultModel.Result = false;
            resultModel.Message = "存在种子队列中,或已抓取完成";
            try
            {
                Tables.t_primary_websites entity = new Tables.t_primary_websites();
                var count = entity.GetCount("WebSite_Url='" + url + "'");
                if (count > 0)
                    resultModel.Result = true;
            }
            catch (Exception ex)
            {
                resultModel.Message = ex.Message;
            }
            return resultModel;
        }

        /// <summary>
        /// 获取种子地址数
        /// </summary>
        /// <returns></returns>
        public static long GetPrimaryWebSiteCount(int Level)
        {
            try
            {
                Tables.t_primary_websites entity = new Tables.t_primary_websites();
                return entity.GetCount(" Status=100 and Level<=" + Level + " ");
            }
            catch (Exception exception)
            {
            }
            return 0;
        }

        /// <summary>
        /// 移除全部
        /// </summary>
        /// <returns></returns>
        public static ResultModel DeletePrimaryWebSites()
        {
            ResultModel resultModel = new ResultModel();
            resultModel.Result = false;
            resultModel.Message = "未知原因";
            try
            {
                string sql = "delete from T_Primary_WebSites;";
                Shove.Database.MySQL.ExecuteNonQuery(sql);
                resultModel.Result = true;
            }
            catch (Exception ex)
            {
                resultModel.Message = ex.Message;
            }
            return resultModel;
        }

        /// <summary>
        /// 移除单个
        /// </summary>
        /// <returns></returns>
        public static ResultModel DeletePrimaryWebSite(string Url)
        {
            ResultModel resultModel = new ResultModel();
            resultModel.Result = false;
            resultModel.Message = "未知原因";
            try
            {
                string sql = "delete from T_Primary_WebSites where WebSite_Url='" + Url + "'";
                Shove.Database.MySQL.ExecuteNonQuery(sql);
                resultModel.Result = true;
            }
            catch (Exception ex)
            {
                resultModel.Message = ex.Message;
            }
            return resultModel;
        }
        
        #endregion

        #region 筛选规则
        /// <summary>
        /// 获取筛选规则实体
        /// </summary>
        /// <returns></returns>
        public static ResultModel<List<FilterRuleConfigurationModel>> GetFilterRuleConfiguration()
        {
            var resulrModel = new ResultModel<List<FilterRuleConfigurationModel>>();
            resulrModel.Result = true;
            resulrModel.Message = "成功";
            resulrModel.Data = new List<FilterRuleConfigurationModel>();
            var entity = new Tables.t_filter_rule_configuration();
            var dt = entity.Open("ID,Filter_KeyWord,Filter_Type,Filter_Position", "", "", -1, -1);

            if (dt == null)
            {
                resulrModel.Message = "数据查询失败!";
                resulrModel.Result = false;
                return resulrModel;
            }
            if (dt.Rows.Count == 0)
            {
                resulrModel.Message = "未找到符合条件的过滤规则数据!";
                resulrModel.Result = false;
                return resulrModel;
            }
            foreach (DataRow dr in dt.Rows)
            {
                resulrModel.Data.Add(new FilterRuleConfigurationModel()
                {
                    ID = Int32.Parse(dr["ID"].ToString()),
                    FilterKeyWord = dr["Filter_KeyWord"].ToString(),
                    FilterType = Int32.Parse(dr["Filter_Type"].ToString()),
                    FilterPosition = Int32.Parse(dr["Filter_Position"].ToString()),
                });
            }
            return resulrModel;
        }
        /// <summary>
        /// 插入筛选条件配置
        /// </summary>
        /// <param name="model"></param>
        public static ResultModel InsertFilterRuleConfiguration(FilterRuleConfigurationModel model)
        {
            ResultModel resultModel = new ResultModel();
            resultModel.Result = false;
            try
            {
                string sql = string.Format("insert into T_Filter_Rule_Configuration(Filter_KeyWord,Filter_Type,Filter_Position,create_time,is_erased)values('{0}',{1},{2},'{3}',{4})"
             , model.FilterKeyWord, model.FilterType, (int)model.FilterPosition, DateTime.Now, 0);
                Shove.Database.MySQL.ExecuteNonQuery(sql);
                resultModel.Result = true;
            }
            catch (Exception ex)
            {
                resultModel.Message = ex.Message;
            }
            return resultModel;
        }

        /// <summary>
        /// 获取筛选规则数
        /// </summary>
        /// <returns></returns>
        public static long GetFilterRuleCount()
        {
            try
            {
                Tables.t_filter_rule_configuration entity = new Tables.t_filter_rule_configuration();
                return entity.GetCount("");
            }
            catch (Exception ex)
            {
            }
            return 0;
        }


        /// <summary>
        /// 移除全部
        /// </summary>
        /// <returns></returns>
        public static ResultModel DeleteFilterRules()
        {
            ResultModel resultModel = new ResultModel();
            resultModel.Result = false;
            resultModel.Message = "未知原因";
            try
            {
                string sql = "delete from T_Filter_Rule_Configuration;";
                Shove.Database.MySQL.ExecuteNonQuery(sql);
                resultModel.Result = true;
            }
            catch (Exception ex)
            {
                resultModel.Message = ex.Message;
            }
            return resultModel;
        }

        /// <summary>
        /// 移除全部
        /// </summary>
        /// <returns></returns>
        public static ResultModel DeleteFilterRule(int Id)
        {
            ResultModel resultModel = new ResultModel();
            resultModel.Result = false;
            resultModel.Message = "未知原因";
            try
            {
                string sql = "delete from T_Filter_Rule_Configuration where Id=" + Id + ";";
                Shove.Database.MySQL.ExecuteNonQuery(sql);
                resultModel.Result = true;
            }
            catch (Exception ex)
            {
                resultModel.Message = ex.Message;
            }
            return resultModel;
        }
        #endregion



        #region 目标地址
        /// <summary>
        /// 插入目标地址数据
        /// </summary>
        /// <param name="model"></param>
        public static void InsertTargetWebSite(TargetWebSiteModel model)
        {
            string sql = string.Format("insert into T_Target_WebSites(Primary_ID,WebSite_Name,WebSite_Url,Weights,create_time,is_erased)SELECT {0},'{1}','{2}',{3},'{4}',{5}  FROM DUAL where NOT EXISTS(SELECT 1 FROM T_Target_WebSites WHERE WebSite_Url='{2}') LIMIT 1;"
                , model.PrimaryID, model.WebSiteName, model.WebSiteUrl, model.Weights, DateTime.Now, 0);
            Shove.Database.MySQL.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取目标地址
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTargetWebSite()
        {
            return Shove.Database.MySQL.Select("select b.WebSite_Url as 种子站点,a.WebSite_Name as 资源平台名字,a.WebSite_Url as 网址,a.Weights as 权重 from T_Target_WebSites  as a JOIN T_Primary_WebSites as b on a.Primary_ID=b.ID;");
        }

        #endregion

        #region 排除地址
        /// <summary>
        /// 获取排除地址集
        /// </summary>
        /// <returns></returns>
        public static ResultModel<List<string>> GetExcludeWebSites()
        {
            ResultModel<List<string>> resulrModel = new ResultModel<List<string>>();
            resulrModel.Result = true;
            resulrModel.Message = "成功";
            resulrModel.Data = new List<string>();

            var entity = new Tables.t_exclude_websites();

            var dt = entity.Open("WebSite_Url", "", "", -1, -1);
            if (dt == null)
            {
                resulrModel.Message = "数据查询失败!";
                resulrModel.Result = false;
                return resulrModel;
            }
            if (dt.Rows.Count == 0)
            {
                resulrModel.Message = "未找到符合条件的数据!";
                resulrModel.Result = false;
                return resulrModel;
            }

            foreach (DataRow dr in dt.Rows)
            {
                var url = dr["WebSite_Url"];
                if (url != null)
                {
                    resulrModel.Data.Add(url.ToString());
                }
            }
            return resulrModel;
        }

        /// <summary>
        /// 插入排除地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ResultModel InserExcludeWebSite(ExcludeWebSiteModel model)
        {
            ResultModel resultModel = new ResultModel();
            resultModel.Result = false;
            try
            {
                string sql = string.Format("INSERT INTO T_Exclude_WebSites(WebSite_Url,WebSite_Name,create_time,is_erased)SELECT '{0}','{1}','{2}',{3} FROM DUAL where NOT EXISTS (SELECT 1 FROM T_Exclude_WebSites WHERE WebSite_Url='{0}') LIMIT 1;",
                        model.WebSiteUrl, model.WebSiteName, DateTime.Now, 0);
                Shove.Database.MySQL.ExecuteNonQuery(sql);
                resultModel.Result = true;
            }
            catch (Exception ex)
            {
                resultModel.Message = ex.Message;
            }
            return resultModel;
        }

        #endregion


        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns></returns>
        public static ResultModel DeleteAll()
        {
            ResultModel resultModel = new ResultModel();
            resultModel.Result = false;
            resultModel.Message = "未知原因";
            try
            {
                string sql = "delete from T_Primary_WebSites;" +
                       "delete from T_Exclude_WebSites;" +
                       "delete from T_Target_WebSites;" +
                       "delete from T_Filter_Rule_Configuration;";
                Shove.Database.MySQL.ExecuteNonQuery(sql);
                resultModel.Result = true;
            }
            catch (Exception ex)
            {
                resultModel.Message = ex.Message;
            }
            return resultModel;
        }
    }
}
