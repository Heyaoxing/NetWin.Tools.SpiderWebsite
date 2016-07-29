using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shove._IO;
using Tools.Services;
using Tools.Services.Models;
using Tools.Services.Enum;
using Tools.WinForm.Controller;
using Tools.WinForm.Models;

namespace Tools.WinForm
{
    public partial class MainSetting : Form
    {
        /// <summary>
        /// 并行数
        /// </summary>
        private static int PAGESIZE = Int32.Parse(ConfigurationManager.AppSettings["pageSize"]);

        public MainSetting()
        {
            InitializeComponent();
            Init();
        }


        private List<FilterTypeComboBoxModel> LoadFilterTypes()
        {
            var filterTypes = new List<FilterTypeComboBoxModel>();
            filterTypes.Add(new FilterTypeComboBoxModel()
            {
                PositionName = "包含",
                PositionValue = 100,
            });
            filterTypes.Add(new FilterTypeComboBoxModel()
            {
                PositionName = "开头",
                PositionValue = 200,
            });
            filterTypes.Add(new FilterTypeComboBoxModel()
            {
                PositionName = "结尾",
                PositionValue = 300,
            });
            return filterTypes;
        }

        /// <summary>
        /// 系统初始化
        /// </summary>
        private void Init()
        {
            this.Level_Text.Text = "5";//默认深度为5
            RefreshPrimaryWebSites();//加载种子地址列表
            RefreshFilterRuleConfiguration();//刷新过滤规则列表
            IsDisableFilterControllers();//根据是否存在筛选条件来判断是否禁用单选框
            this.Filter_Title_Radio.Checked = true;//选中筛选条件网站标题
            EnableControls();
            #region 下拉选项框

            this.Filter_Type_ComboBox.DataSource = LoadFilterTypes();
            this.Filter_Type_ComboBox.DisplayMember = "PositionName";//显示名称
            this.Filter_Type_ComboBox.ValueMember = "PositionValue";//实际值


            #endregion
        }

        /// <summary>
        /// 根据是否存在筛选条件来判断是否禁用单选框
        /// </summary>
        private void IsDisableFilterControllers()
        {
            var count = DBOperationService.GetFilterRuleCount();
            if (count > 0)
            {
                this.Filter_Title_Radio.Enabled = false;
                this.Filter_Url_Radio.Enabled = false;
            }
            else
            {
                this.Filter_Title_Radio.Enabled = true;
                this.Filter_Url_Radio.Enabled = true;
            }
        }

        /// <summary>
        /// 禁用控件
        /// </summary>
        private void DisableControls()
        {
            this.Export_Btn.Enabled = false;
            this.Import_Btn.Enabled = false;
            this.PrimaryUrl_Text.Enabled = false;
            this.Add_PrimaryUrl_Btn.Enabled = false;
            this.Filter_Title_Radio.Enabled = false;
            this.Filter_Url_Radio.Enabled = false;
            this.Filter_Type_ComboBox.Enabled = false;
            this.KeyWord_Text.Enabled = false;
            this.Add_KeyWord_Btn.Enabled = false;
            this.Level_Text.Enabled = false;
            this.Start_Btn.Enabled = false;
            this.Reset_Btn.Enabled = false;
            //暂停按钮
            this.Stop_Btn.Enabled = true;

        }

        /// <summary>
        /// 启用控件
        /// </summary>
        private void EnableControls()
        {
            this.Export_Btn.Enabled = true;
            this.Import_Btn.Enabled = true;
            this.PrimaryUrl_Text.Enabled = true;
            this.Add_PrimaryUrl_Btn.Enabled = true;
            this.Filter_Title_Radio.Enabled = true;
            this.Filter_Url_Radio.Enabled = true;
            this.Filter_Type_ComboBox.Enabled = true;
            this.KeyWord_Text.Enabled = true;
            this.Add_KeyWord_Btn.Enabled = true;
            this.Level_Text.Enabled = true;
            this.Start_Btn.Enabled = true;
            this.Reset_Btn.Enabled = true;
            //暂停按钮
            this.Stop_Btn.Enabled = false;
        }

        /// <summary>
        /// 添加种子地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_PrimaryUrl_Btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.Level_Text.Text))
            {
                MessageBox.Show("未设置爬取深度!");
                return;
            }


            var primaryUrl = this.PrimaryUrl_Text.Text;
            if (string.IsNullOrWhiteSpace(primaryUrl))
                return;

            var isExistsResult = DBOperationService.IsExistsPrimaryWebSite(primaryUrl);
            if (isExistsResult.Result)
            {
                MessageBox.Show(isExistsResult.Message);
                return;
            }

            var insertResult = DBOperationService.InsertPrimaryWebSite(new PrimaryWebSiteModel()
               {
                   WebSiteUrl = primaryUrl.Trim(),
                   SourceID = 0,
                   Level = 0,
                   Status = SpiderEnum.PrimaryStatus.NoneSpider
               });

            if (insertResult.Result)
            {
                RefreshPrimaryWebSites();
            }
            else
            {
                MessageBox.Show(insertResult.Message);
            }
        }

        /// <summary>
        /// 刷新种子地址列表框
        /// </summary>
        private void RefreshPrimaryWebSites()
        {
            this.PrimaryUrl_ListView.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度  
            int level = Int32.Parse(this.Level_Text.Text);
            this.PrimaryUrl_ListView.Items.Clear();
            var primaryWebSites = DBOperationService.GetPrimaryWebSites(level);
            if (primaryWebSites.Result)
            {
                foreach (var item in primaryWebSites.Data)
                {
                    this.PrimaryUrl_ListView.Items.Add(item.WebSiteUrl);
                }
            }
            this.PrimaryUrl_ListView.EndUpdate(); //结束数据处理，UI界面一次性绘制。  
            RefreshPrimaryCoount();
        }


        /// <summary>
        /// 添加关键字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_KeyWord_Btn_Click(object sender, EventArgs e)
        {
            var keyWord = this.KeyWord_Text.Text;
            if (string.IsNullOrWhiteSpace(keyWord))
                return;

            var filterRule = new FilterRuleConfigurationModel();
            filterRule.FilterKeyWord = keyWord.Trim();
            filterRule.FilterType = Convert.ToInt32(this.Filter_Type_ComboBox.SelectedValue);
            if (this.Filter_Title_Radio.Checked == true)
                filterRule.FilterPosition = 100;
            else
                filterRule.FilterPosition = 200;

            var insertResult = DBOperationService.InsertFilterRuleConfiguration(filterRule);
            if (insertResult.Result)
            {
                RefreshFilterRuleConfiguration();
            }
            else
            {
                MessageBox.Show(insertResult.Message);
            }
            IsDisableFilterControllers();
        }

        /// <summary>
        /// 刷新过滤规则列表
        /// </summary>
        private void RefreshFilterRuleConfiguration()
        {
            this.KeyWord_ListView.Items.Clear();
            var filterRules = DBOperationService.GetFilterRuleConfiguration();
            if (filterRules.Result)
            {
                var filters = LoadFilterTypes();
                for (int i = 0; i < filterRules.Data.Count; i++)
                {
                    //添加行  
                    var item = new ListViewItem();
                    item.Text = filterRules.Data[i].FilterKeyWord;
                    item.Tag = filterRules.Data[i].ID;
                    item.SubItems.Add(filters.Where(p => p.PositionValue.ToString() == filterRules.Data[i].FilterType.ToString())
                        .Select(p => p.PositionName).FirstOrDefault());
                    item.SubItems.Add(filterRules.Data[i].FilterPosition.ToString() == "100" ? "网站标题" : "网址链接");
                    this.KeyWord_ListView.Items.Add(item);
                }
            }
        }

        private void MainSetting_Load(object sender, EventArgs e)
        {

        }

        private CancellationTokenSource _cts;//方便做取消功能
        /// <summary>
        /// 开始抓取网站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_Btn_Click(object sender, EventArgs e)
        {
            _cts = new CancellationTokenSource();
            if (string.IsNullOrWhiteSpace(this.Level_Text.Text))
            {
                MessageBox.Show("未设置爬取深度!");
                return;
            }
            int level = Convert.ToInt32(this.Level_Text.Text);
            try
            {
                ThreadPool.QueueUserWorkItem(state => Start(Int32.Parse(this.Level_Text.Text), _cts.Token));//作为后台进程
                DisableControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 开始执行抓取
        /// </summary>
        /// <param name="Level"></param>
        private void Start(int Level, CancellationToken ct)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            this.Invoke(new Action(() => UserdTime(sw.Elapsed)));
            this.Invoke(new Action(() => FormLog("开始抓取")));
            SpiderController.LoadFilterRule();
            //并发键值对的线程安全集合
            //保存种子站点
            ConcurrentDictionary<int, List<string>> primarydiDictionary = new ConcurrentDictionary<int, List<string>>();
            //保存目标站点
            ConcurrentBag<TargetWebSiteModel> targetDictionary;

            while (true)
            {
                this.Invoke(new Action(() => UserdTime(sw.Elapsed)));
                primarydiDictionary.Clear();
                targetDictionary = null;
                targetDictionary = new ConcurrentBag<TargetWebSiteModel>();

                var primaryWebSites = DBOperationService.GetPrimaryWebSites(Level, 0, PAGESIZE);
                if (primaryWebSites.Result)
                {
                    Parallel.ForEach(primaryWebSites.Data, p =>
                    {
                        try
                        {
                            var htmlResult = SpiderService.GetHtml(p.WebSiteUrl);
                            if (htmlResult.Result)
                            {
                                string title = string.Empty;
                                int weights = -1;
                                var processResult = SpiderController.HtmlProcess(htmlResult.Data, p.WebSiteUrl,
                                    ref title, ref weights);

                                this.Invoke(new Action(() => FormLog(title)));

                                if (processResult.Result)
                                {
                                    primarydiDictionary.GetOrAdd(p.ID, processResult.Data);

                                    targetDictionary.Add(new TargetWebSiteModel()
                                    {
                                        PrimaryID = p.ID,
                                        WebSiteName = title,
                                        WebSiteUrl = p.WebSiteUrl,
                                        Weights = weights
                                    });
                                }
                            }
                            else
                            {
                                this.Invoke(new Action(() => FormLog("错误:" + htmlResult.Message)));
                            }
                        }
                        catch (Exception exception)
                        {
                            this.Invoke(new Action(() => FormLog("错误:" + exception.Message)));
                        }
                    });

                    this.Invoke(new Action(() => UserdTime(sw.Elapsed)));
                    //更新抓状态
                    DBOperationService.UpdatePrimaryStatus(primaryWebSites.Data.Select(p => p.ID).ToList(), SpiderEnum.PrimaryStatus.Completed);
                    //批量插入种子地址
                    SpiderController.BatchInsertPrimaryWebSites(primarydiDictionary, primaryWebSites.Data);
                    //批量插入目标地址
                    SpiderController.BatchInsertTargetWebSites(targetDictionary);
                    //刷新界面中待抓取的种子队列数
                    this.Invoke(new Action(() => RefreshPrimaryWebSites()));
                }
                else
                {
                    break;
                }

                //是否取消
                if (ct.IsCancellationRequested)
                {
                    break;
                }
            }
            this.Invoke(new Action(() => UserdTime(sw.Elapsed)));
            this.Invoke(new Action(() => FormLog("结束抓取")));
            this.Invoke(new Action(() => EnableControls()));
        }

        private void FormLog(string Content)
        {
            this.ProcessMessae_LisView.Items.Add(string.Format("{0} {1}", DateTime.Now, Content));
            this.ProcessMessae_LisView.EnsureVisible(this.ProcessMessae_LisView.Items.Count - 1);
        }

        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stop_Btn_Click(object sender, EventArgs e)
        {
            if (_cts != null)
            {
                _cts.Cancel();
                this.Invoke(new Action(() => FormLog("取消抓取")));
                EnableControls();
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Guid.NewGuid() + ".xlsx");
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "表格文件 (*.xls)|*.xls";
                saveFileDialog.FileName = "采集的结果.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Shove._Excel.NPOIExcelHelper.RenderToExcel(DBOperationService.GetTargetWebSite(), filePath);
                    string fileName = saveFileDialog.FileName;
                    System.IO.File.Move(filePath, fileName);
                    MessageBox.Show("导出成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 导出排除地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Import_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FilterIndex = 1;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;
                    if (string.IsNullOrWhiteSpace(fileName))
                        return;

                    var dt = Shove._Excel.NPOIExcelHelper.RenderFromExcel(fileName);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<ExcludeWebSiteModel> excludeWebSite = new List<ExcludeWebSiteModel>();
                        foreach (DataRow row in dt.Rows)
                        {
                            var url = row["网址"] != null ? row["网址"].ToString() : "";
                            if (string.IsNullOrWhiteSpace(url))
                                continue;

                            excludeWebSite.Add(new ExcludeWebSiteModel()
                            {
                                WebSiteName = row["资源平台名字"] != null ? row["资源平台名字"].ToString() : "",
                                WebSiteUrl = url
                            });
                        }
                        int count = 0;
                        foreach (var item in excludeWebSite)
                        {
                            if (DBOperationService.InserExcludeWebSite(item).Result)
                                count++;
                        }

                        MessageBox.Show(string.Format("共导入:{0},导出成功:{1},导入失败:{2}", dt.Rows.Count, count, dt.Rows.Count - count));
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("导入失败:" + exception.Message);
            }
        }

        /// <summary>
        /// 刷新界面中的种子队列数
        /// </summary>
        public void RefreshPrimaryCoount()
        {
            if (string.IsNullOrWhiteSpace(this.Level_Text.Text))
            {
                MessageBox.Show("未设置爬取深度!");
                return;
            }
            this.PrimaryCount_Label.Text =
                DBOperationService.GetPrimaryWebSiteCount(Int32.Parse(this.Level_Text.Text)).ToString();
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Btn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("警告:此操作将会删除所有数据,包括采集的结果!(请提前导出采集结果)", "确定是否重置所有数据", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var deleteResult = DBOperationService.DeleteAll();
                if (deleteResult.Result)
                {
                    MessageBox.Show("重置成功!");
                    Init();
                }
                else
                {
                    MessageBox.Show(deleteResult.Message);
                }
            }
        }

        /// <summary>
        /// 格式化显示时间
        /// </summary>
        /// <param name="ts"></param>
        private void UserdTime(TimeSpan ts)
        {
            UserdTime_Label.Text = String.Format("{0}:{1}:{2}:{3}:{4}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void PrimaryUrl_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PrimaryUrl_ListView.SelectedIndices.Count > 0)
            {
                //有选中项，这里设右键某项可用
                Primary_ContextMenuStrip.Items[0].Enabled = true;
            }
            else
            {
                Primary_ContextMenuStrip.Items[0].Enabled = false;
            }
        }

        private void PrimaryUrl_ListView_MouseEnter(object sender, EventArgs e)
        {
            if (PrimaryUrl_ListView.SelectedIndices.Count > 0)
            {
                //有选中项，这里设右键某项可用
                Primary_ContextMenuStrip.Items[0].Enabled = true;
            }
            else
            {
                Primary_ContextMenuStrip.Items[0].Enabled = false;
            }
        }

        /// <summary>
        /// 移除种子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 移除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var url = PrimaryUrl_ListView.SelectedItems[0].Text;
            if (string.IsNullOrWhiteSpace(url))
                return;

            var deleteResult = DBOperationService.DeletePrimaryWebSite(url);
            if (!deleteResult.Result)
            {
                MessageBox.Show(deleteResult.Message);
            }
            else
            {
                RefreshPrimaryWebSites();
            }
        }

        /// <summary>
        /// 种子全部移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 全部移除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var deleteResult = DBOperationService.DeletePrimaryWebSites();
            if (!deleteResult.Result)
            {
                MessageBox.Show(deleteResult.Message);
            }
            else
            {
                RefreshPrimaryWebSites();
            }
        }

        private void KeyWord_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KeyWord_ListView.SelectedIndices.Count > 0)
            {
                //有选中项，这里设右键某项可用
                Filter_ContextMenuStrip.Items[0].Enabled = true;
            }
            else
            {
                Filter_ContextMenuStrip.Items[0].Enabled = false;
            }
        }

        private void KeyWord_ListView_MouseEnter(object sender, EventArgs e)
        {
            if (KeyWord_ListView.SelectedIndices.Count > 0)
            {
                //有选中项，这里设右键某项可用
                Filter_ContextMenuStrip.Items[0].Enabled = true;
            }
            else
            {
                Filter_ContextMenuStrip.Items[0].Enabled = false;
            }
        }

        private void 移除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var id = Int32.Parse(KeyWord_ListView.SelectedItems[0].Tag.ToString());
            var deleteResult = DBOperationService.DeleteFilterRule(id);
            if (!deleteResult.Result)
            {
                MessageBox.Show(deleteResult.Message);
            }
            else
            {
                 RefreshFilterRuleConfiguration();
            }
        }

        private void 全部移除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var deleteResult = DBOperationService.DeleteFilterRules();
            if (!deleteResult.Result)
            {
                MessageBox.Show(deleteResult.Message);
            }
            else
            {
                 RefreshFilterRuleConfiguration();
            }
        }
    }
}
