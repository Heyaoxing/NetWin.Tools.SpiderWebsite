using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections;

using Shove.Database;

namespace SpiderWebsite.DAL
{
	/*
	Program Name: Shove.DAL.30 for MySQL
	Program Version: 3.0
	Writer By: 3km.shovesoft.shove (zhou changjun)
	Release Time: 2009.7.16

	System Request: Shove.dll, MySql.Data.dll, MySql.Data.Entity.dll, MySql.Web.dll
	All Rights saved.
	*/


	// Please Add a Key in Web.config File's appSetting section, Exemple:
	// <add key="ConnectionString" value="server=localhost;user id=root;password=;database=test;port=3306;" />


	public class Tables
	{
        public class t_primary_websites : MySQL.TableBase
        {
            public MySQL.Field ID;
            public MySQL.Field Source_ID;
            public MySQL.Field WebSite_Url;
            public MySQL.Field Master_Host;
            public MySQL.Field Level;
            public MySQL.Field Status;
            public MySQL.Field create_time;

            public t_primary_websites()
            {
                TableName = "t_primary_websites";

                ID = new MySQL.Field(this, "ID", "ID", MySqlDbType.Int64, true);
                Source_ID = new MySQL.Field(this, "Source_ID", "Source_ID", MySqlDbType.Int64, false);
                WebSite_Url = new MySQL.Field(this, "WebSite_Url", "WebSite_Url", MySqlDbType.VarChar, false);
                Master_Host = new MySQL.Field(this, "Master_Host", "Master_Host", MySqlDbType.VarChar, false);
                Level = new MySQL.Field(this, "Level", "Level", MySqlDbType.Int32, false);
                Status = new MySQL.Field(this, "Status", "Status", MySqlDbType.Int32, false);
                create_time = new MySQL.Field(this, "create_time", "create_time", MySqlDbType.DateTime, false);
            }
        }

        public class t_filter_rule_configuration : MySQL.TableBase
        {
            public MySQL.Field ID;
            public MySQL.Field Filter_KeyWord;
            public MySQL.Field Filter_Type;
            public MySQL.Field Filter_Position;
            public MySQL.Field create_time;

            public t_filter_rule_configuration()
            {
                TableName = "t_filter_rule_configuration";

                ID = new MySQL.Field(this, "ID", "ID", MySqlDbType.Int64, true);
                Filter_KeyWord = new MySQL.Field(this, "Filter_KeyWord", "Filter_KeyWord", MySqlDbType.VarChar, false);
                Filter_Type = new MySQL.Field(this, "Filter_Type", "Filter_Type", MySqlDbType.Int32, false);
                Filter_Position = new MySQL.Field(this, "Filter_Position", "Filter_Position", MySqlDbType.Int32, false);
                create_time = new MySQL.Field(this, "create_time", "create_time", MySqlDbType.DateTime, false);
            }
        }

        public class t_exclude_websites : MySQL.TableBase
        {
            public MySQL.Field ID;
            public MySQL.Field WebSite_Name;
            public MySQL.Field WebSite_Url;
            public MySQL.Field create_time;

            public t_exclude_websites()
            {
                TableName = "t_exclude_websites";

                ID = new MySQL.Field(this, "ID", "ID", MySqlDbType.Int64, true);
                WebSite_Name = new MySQL.Field(this, "WebSite_Name", "WebSite_Name", MySqlDbType.VarChar, false);
                WebSite_Url = new MySQL.Field(this, "WebSite_Url", "WebSite_Url", MySqlDbType.VarChar, false);
                create_time = new MySQL.Field(this, "create_time", "create_time", MySqlDbType.DateTime, false);
            }
        }


        public class t_target_websites : MySQL.TableBase
        {
            public MySQL.Field ID;
            public MySQL.Field Primary_ID;
            public MySQL.Field WebSite_Name;
            public MySQL.Field WebSite_Url;
            public MySQL.Field Group_Name;
            public MySQL.Field Weights;
            public MySQL.Field create_time;
            public MySQL.Field is_erased;

            public t_target_websites()
            {
                TableName = "t_target_websites";

                ID = new MySQL.Field(this, "ID", "ID", MySqlDbType.Int64, true);
                Primary_ID = new MySQL.Field(this, "Primary_ID", "Primary_ID", MySqlDbType.Int64, true);
                WebSite_Name = new MySQL.Field(this, "WebSite_Name", "WebSite_Name", MySqlDbType.VarChar, false);
                WebSite_Url = new MySQL.Field(this, "WebSite_Url", "WebSite_Url", MySqlDbType.VarChar, false);
                Group_Name = new MySQL.Field(this, "Group_Name", "Group_Name", MySqlDbType.VarChar, false);
                Weights = new MySQL.Field(this, "Weights", "Weights", MySqlDbType.Int32, false);
                create_time = new MySQL.Field(this, "create_time", "create_time", MySqlDbType.DateTime, false);
                is_erased = new MySQL.Field(this, "is_erased", "is_erased", MySqlDbType.Int16, false);
            }
        }
		
	}

	public class Views
	{
		
	}

	public class Functions
	{
	}

	public class Procedures
	{
	}
}
