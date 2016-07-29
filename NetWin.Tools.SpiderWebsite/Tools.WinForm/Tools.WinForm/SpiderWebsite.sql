drop table if exists T_Primary_WebSites;

/*==============================================================*/
/* Table: T_Primary_WebSites                                    */
/*==============================================================*/
create table T_Primary_WebSites
(
   ID                   bigint not null auto_increment,
   Source_ID            bigint,
   WebSite_Url          varchar(100) not null comment '种子站点',
   Level                int not null default 0 comment '深度',
   Status               int not null default 100 comment '状态:
            100:未爬去
            200:已爬去',
   create_time          datetime not null comment '创建时间',
   is_erased            tinyint not null default 0,
   primary key (ID)
);



drop table if exists T_Target_WebSites;

/*==============================================================*/
/* Table: T_Target_WebSites                                     */
/*==============================================================*/
create table T_Target_WebSites
(
   ID                   bigint not null auto_increment,
   Primary_ID           bigint not null comment '种子站点',
   WebSite_Name         varchar(100) not null comment '目标平台名字',
   WebSite_Url          varchar(100) not null comment '目标网址',
   Weights              int not null comment '权重',
   create_time          datetime not null comment '创建时间',
   is_erased            tinyint not null default 0,
   primary key (ID)
);



drop table if exists T_Exclude_WebSites;

/*==============================================================*/
/* Table: T_Exclude_WebSites                                    */
/*==============================================================*/
create table T_Exclude_WebSites
(
   ID                   bigint not null auto_increment,
   WebSite_Name         varchar(100) not null comment '资源平台名字',
   WebSite_Url          varchar(100) not null comment '主域网址',
   create_time          datetime not null comment '创建时间',
   is_erased            tinyint not null default 0,
   primary key (ID)
);



drop table if exists T_Filter_Rule_Configuration;

/*==============================================================*/
/* Table: T_Filter_Rule_Configuration                           */
/*==============================================================*/
create table T_Filter_Rule_Configuration
(
   ID                   bigint not null auto_increment,
   Filter_KeyWord       varchar(50) not null comment '关键字',
   Filter_Type          int not null default 100 comment '筛选类型:
            100:包含
            200:开头
            300:结尾',
   Filter_Position      int not null default 100 comment '筛选位置:
            100:网站标题
            200:网站链接',
   create_time          datetime not null comment '创建时间',
   is_erased            tinyint not null default 0,
   primary key (ID)
);
