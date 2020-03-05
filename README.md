# LiteORM-For-DotNet
轻量级高性能的DotNet ORM框架，解决C#.Net开发过程中重复繁琐的数据库CURD操作

YEasyModel ORM 框架使用                                                                                                           

1、YEasyModel 动态库：

 项目工程引用YEasyModel.dll，将YEasyModel.xml复制到当前bin生成的目录下，用于显示接口、参数说明。

 

2、ModelApp实体类（模型）生成工具：

 ModelApp.exe用于生成表、视图、存储过程实体类。

 

支持平台                                                                                                                                    

目标平台：.Net Framework4.0及以上

开发语言：C#；

目标数据库：MS Sqlserver 2005及以上


项目开源库结构介绍：

一、YEasyModel

主要实体类反射类库，定义实体类字段的数据类型、长度、主键等特性；定义CURD方法，查询参数表达式、排序表达式。利用lambda定义查询逻辑，生成sql过滤条件；查询/更新字段定义，通过反射生成对应的Sql参数；排序逻辑定义，生成字段排序规则；DataTable与实体类转换方法。

 

YEasyModel主要类库说明：

 

1、YEasyModel.ModelDAL，数据库增、删、改、查操作工具类；

 

类方法说明：

//
        // 摘要:
        //     SqlDataAdapter批量更新
        //
        // 参数:
        //   modelList:
        //     列表实体
        //
        // 类型参数:
        //   T:
        //     表实体类
        //
        // 返回结果:
        //     返回更新影响的行数
        public static int BatchUpdate<T>(List<T> modelList);
       
        //
        // 摘要:
        //     删除一条记录
        //
        // 参数:
        //   keyValue:
        //     主键值
        //
        // 类型参数:
        //   T:
        //     表实体类
        public static int Delete<T>(object keyValue);
        //
        // 摘要:
        //     删除一条记录
        //
        // 参数:
        //   filter:
        //     查询条件：lambda条件过滤表达式
        //
        // 类型参数:
        //   T:
        //     表实体类
        public static int Delete<T>(Expression<Func<T, bool>> filter);
        //
        // 摘要:
        //     执行sql脚本
        //
        // 参数:
        //   sqlScript:
        //     sql脚本
        public static int ExecuteSql(string sqlScript);
        //
        // 摘要:
        //     查询当前主键是否已存在
        //
        // 参数:
        //   keyValue:
        //     主键值
        //
        // 类型参数:
        //   T:
        //     表实体类
        public static bool Exists<T>(object keyValue);
        //
        // 摘要:
        //     查询当前字段值是否已存在
        //
        // 参数:
        //   filter:
        //     查询条件：lambda条件过滤表达式
        //
        // 类型参数:
        //   T:
        //     表实体类
        public static bool Exists<T>(Expression<Func<T, bool>> filter);
        //
        // 摘要:
        //     查询当前表最大的主键值
        //
        // 类型参数:
        //   T:
        //     表实体类
        public static object GetMaxID<T>();
        //
        // 摘要:
        //     根据主键值查询获取一条数据
        //
        // 参数:
        //   keyValue:
        //     主键值
        //
        // 类型参数:
        //   T:
        //     表实体类
        public static T GetModel<T>(object keyValue);
        //
        // 摘要:
        //     查询当前字段值是否已存在
        //
        // 参数:
        //   filter:
        //     查询条件：lambda条件过滤表达式
        //
        // 类型参数:
        //   T:
        //     表实体类
        public static int GetRecordCount<T>(Expression<Func<T, bool>> filter);
        //
        // 摘要:
        //     查询当前指定字段的值
        //
        // 参数:
        //   filter:
        //     查询条件：lambda条件过滤表达式
        //
        //   order:
        //     排序表达式
        //
        //   field:
        //     查询字段：lambda字段表达式
        //
        // 类型参数:
        //   T:
        //     表实体类
        //
        // 返回结果:
        //     返回当前行的查询字段值
        public static object GetValue<T>(Expression<Func<T, bool>> filter = null, OrderBy<T> order = null, Expression<Func<T, object>> field = null);
        //
        // 摘要:
        //     新增一条数据
        //
        // 参数:
        //   model:
        public static int Insert(object model);
        //
        // 摘要:
        //     新增一条数据
        //
        // 参数:
        //   model:
        //
        //   writeIdentityKey:
        //     是否写入自增长ID
        public static int Insert(object model, bool writeIdentityKey);
        //
        // 摘要:
        //     多表联合查询(left join)
        //
        // 参数:
        //   joinExpression:
        //     联接条件
        //
        //   filter:
        //     查询条件
        //
        //   order:
        //     排序
        //
        //   fields:
        //     查询字段
        //
        // 类型参数:
        //   T:
        //     返回的数据实体类型
        //
        //   T1:
        //     表1
        //
        //   T2:
        //     表2
        //
        // 返回结果:
        //     数据实体列表
        public static List<T> Join<T, T1, T2>(Expression<Func<T1, T2, bool>> joinExpression, Expression<Func<T1, T2, bool>> filter = null, OrderBy<T> order = null, params Expression<Func<T1, T2, object>>[] fields);
        //
        // 摘要:
        //     多表联合查询(left join)
        //
        // 参数:
        //   joinExpression:
        //     联接条件
        //
        //   filter:
        //     查询条件
        //
        //   order:
        //     排序
        //
        //   fields:
        //     查询字段
        //
        // 类型参数:
        //   T:
        //     返回的数据实体类型
        //
        //   T1:
        //     表1
        //
        //   T2:
        //     表2
        //
        // 返回结果:
        //     数据实体列表
        public static DataTable JoinForDataTable<T, T1, T2>(Expression<Func<T1, T2, bool>> joinExpression, Expression<Func<T1, T2, bool>> filter = null, OrderBy<T> order = null, params Expression<Func<T1, T2, object>>[] fields);
        //
        // 摘要:
        //     获取join联接表
        //
        // 参数:
        //   parameters:
        //
        //   agrs:
        public static string JoinTable(ReadOnlyCollection<ParameterExpression> parameters, params Type[] agrs);
        //
        // 摘要:
        //     执行sql脚本查询数据
        //
        // 参数:
        //   sqlScript:
        //     sql脚本
        //
        // 返回结果:
        //     DataSet
        public static DataSet Query(string sqlScript);
        //
        // 摘要:
        //     根据条件查询
        //
        // 参数:
        //   filter:
        //     查询条件：lambda条件过滤表达式
        //
        //   order:
        //     排序表达式
        //
        //   fields:
        //     查询字段：lambda字段表达式【可多组】
        //
        // 类型参数:
        //   T:
        //     表实体类
        //
        // 返回结果:
        //     列表实体
        public static List<T> Select<T>(Expression<Func<T, bool>> filter = null, OrderBy<T> order = null, params Expression<Func<T, object>>[] fields);
        //
        // 摘要:
        //     根据条件查询
        //
        // 参数:
        //   filter:
        //     查询条件：lambda条件过滤表达式
        //
        //   order:
        //     排序表达式
        //
        //   fields:
        //     查询字段：lambda字段表达式【可多组】
        //
        // 类型参数:
        //   T:
        //     表实体类
        //
        // 返回结果:
        //     列表实体
        public static DataTable SelectForDataTable<T>(Expression<Func<T, bool>> filter = null, OrderBy<T> order = null, params Expression<Func<T, object>>[] fields);
        //
        // 摘要:
        //     根据条件查询一条记录
        //
        // 参数:
        //   filter:
        //     查询条件：lambda条件过滤表达式
        //
        //   order:
        //     排序表达式
        //
        //   fields:
        //     查询字段：lambda字段表达式【可多组】
        //
        // 类型参数:
        //   T:
        //     表实体类
        //
        // 返回结果:
        //     列表实体
        public static T SelectSingleRecord<T>(Expression<Func<T, bool>> filter = null, OrderBy<T> order = null, params Expression<Func<T, object>>[] fields);
        //
        // 摘要:
        //     根据条件查询第一条记录
        //
        // 参数:
        //   topNumber:
        //     记录数：默认1条记录
        //
        //   filter:
        //     查询条件：lambda条件过滤表达式
        //
        //   order:
        //     排序表达式
        //
        //   fields:
        //     查询字段：lambda字段表达式【可多组】
        //
        // 类型参数:
        //   T:
        //     表实体类
        //
        // 返回结果:
        //     列表实体
        public static T SelectTopRecord<T>(int topNumber = 1, Expression<Func<T, bool>> filter = null, OrderBy<T> order = null, params Expression<Func<T, object>>[] fields);
        //
        // 摘要:
        //     SqlBulkCopy批量提交数据[复制]
        //
        // 参数:
        //   modelList:
        //     列表实体
        //
        // 类型参数:
        //   T:
        //     表实体类
        public static void SqlBulkCopy<T>(List<T> modelList);
        //
        // 摘要:
        //     更新一条数据
        //
        // 参数:
        //   model:
        //     数据实体
        //
        //   filter:
        //     过滤条件
        //
        //   fields:
        //     更新字段
        //
        // 类型参数:
        //   T:
        //     实体类型
        public static int Update<T>(T model, Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] fields);
        //
        // 摘要:
        //     根据主键更新一条数据
        //
        // 参数:
        //   model:
        public static int Update(object model);

2、YEasyModel.ModelUtil，数据表转实体模型工具类； 

类方法说明：

复制代码
        //
        // 摘要:
        //     DataRow数据行转实体
        //
        // 参数:
        //   dr:
        //     DataRow数据行
        //
        // 类型参数:
        //   T:
        //     实体类
        //
        // 返回结果:
        //     单个实体
        public static T DataRowParse<T>(DataRow dr);
        //
        // 摘要:
        //     DataTable数据表转实体列表
        //
        // 参数:
        //   dataTable:
        //     数据表
        //
        // 类型参数:
        //   T:
        //     实体类
        //
        // 返回结果:
        //     列表实体
        public static List<T> DataTableParse<T>(DataTable dataTable);
        //
        // 摘要:
        //     DataTable数据表转实体
        //
        // 参数:
        //   dataTable:
        //     数据表
        //
        // 类型参数:
        //   T:
        //     实体类
        //
        // 返回结果:
        //     单个实体
        public static T DataTableParseSingle<T>(DataTable dataTable);
        //
        // 摘要:
        //     实体列表转DataTable
        //
        // 参数:
        //   modelList:
        //     实体列表
        //
        // 类型参数:
        //   T:
        //     实体类
        public static DataTable ModelList2DataTable<T>(List<T> modelList);
复制代码
3、YEasyModel.OrderBy，数据排序对象类；

类方法说明：

复制代码
/// <summary>
        /// 添加字段排序
        /// </summary>
        /// <param name="field">字段表达式</param>
        /// <param name="orderByEnum">排序规则</param>
    public void Add(Expression<Func<T, object>> field, OrderByEnum orderByEnum)

/// <summary>
        /// 获取字段排序表达式
        /// </summary>
        /// <returns></returns>
    public Dictionary<Expression<Func<T, object>>, OrderByEnum> GetOrderByList()
复制代码
4、YEasyModel.ProcedureUtil，存储过程调用工具类；

类方法说明：

复制代码
//
        // 摘要:
        //     执行存储过程
        //
        // 参数:
        //   model:
        public static int Execute<T>(T model);
        //
        // 摘要:
        //     执行存储过程
        //
        // 参数:
        //   model:
        public static int Execute<T>(ref T model);
        //
        // 摘要:
        //     执行存储过程-查询数据
        //
        // 参数:
        //   model:
        public static DataTable Select<T>(T model);
        //
        // 摘要:
        //     执行存储过程-查询数据
        //
        // 参数:
        //   model:
        public static DataTable Select<T>(ref T model);
复制代码
 

 

 

二、ModelApp

winform程序，用于配置连接数据库，定义命名空间、实体类名，生成指定的表/视图的实体模型；生成存储过程实体模型，简化存储过程调用方式；

 

三、WebDemo

Webapi范例，简单的表实体模型使用说明；

 

复制代码
/******************************************使用示例******************************************/
            
               DBModel.Person_FaceInfoModel addModel = new DBModel.Person_FaceInfoModel()
                        {
                            //给字段赋值
                        };

                        //新增一条数据
            YEasyModel.ModelDAL.Insert(addModel);

                        //根据主键ID修改一条数据
            YEasyModel.ModelDAL.Update(addModel);

                        //修改一条数据，指定条件、更新字段
            YEasyModel.ModelDAL.Update<DBModel.Person_FaceInfoModel>(addModel, q => q.FaceID == 1);
            YEasyModel.ModelDAL.Update<DBModel.Person_FaceInfoModel>(addModel, q => q.FaceID == 1, q => q.FacePath, q => q.Remark);

            //根据ID查询 Person_FaceInfoModel 对应表的所有记录
            YEasyModel.ModelDAL.GetModel<DBModel.Person_FaceInfoModel>(id);

                        //查询所有数据
            YEasyModel.ModelDAL.Select<DBModel.Person_FaceInfoModel>();

                        //根据FaceID查询单条记录
            YEasyModel.ModelDAL.SelectSingleRecord<DBModel.Person_FaceInfoModel>(q => q.FaceID == id);

                        //按条件查询数据
            YEasyModel.ModelDAL.Select<DBModel.Person_FaceInfoModel>(q => q.Create_Date > DateTime.Now.AddDays(-1) 
                        && q.Create_Date < DateTime.Now && q.Remark != "");

                        //按条件查询、指定字段1、字段2...
            YEasyModel.ModelDAL.SelectSingleRecord<DBModel.Person_FaceInfoModel>(q => q.FaceID == id, null, q => q.Person_ID, q => q.FacePath);

                        //创建字段排序规则
                        var order = new OrderBy<DBModel.Person_FaceInfoModel>();
                        order.Add(q => q.Create_Date, OrderByEnum.asc);
                        //查询所有数据+排序
            YEasyModel.ModelDAL.Select<DBModel.Person_FaceInfoModel>(null, order, null);

            //删除指定ID数据
            YEasyModel.ModelDAL.Delete<DBModel.Person_FaceInfoModel>(1);

                        //删除指定条件的数据
            YEasyModel.ModelDAL.Delete<DBModel.Person_FaceInfoModel>(q => q.FacePath == "" && q.Remark == "");

            DataTable dt = new DataTable();//查询数据
                        //DataTable数据转实体列表
            YEasyModel.ModelUtil.DataTableParse<DBModel.Person_FaceInfoModel>(dt);

                        //DataRow数据转实体                            YEasyModel.ModelUtil.DataRowParse<DBModel.Person_FaceInfoModel>(dt.Rows[0]);
    
            //调用存储过程查询CT_Comsume表最大ID
            DBModel.Proc.CT_Comsume_GetMaxIdModel proc = new DBModel.Proc.CT_Comsume_GetMaxIdModel();
            int iReturn = YEasyModel.ProcedureUtil.Execute<DBModel.Proc.CT_Comsume_GetMaxIdModel>(proc);

            //调用存储过程删除Detail_ID = 4的记录
            DBModel.Proc.CT_ComsumeDetail_DeleteModel de = new DBModel.Proc.CT_ComsumeDetail_DeleteModel();
            de.Detail_ID = 4;
            int iReturn2 = YEasyModel.ProcedureUtil.Execute<DBModel.Proc.CT_ComsumeDetail_DeleteModel>(de);

            //多表join联合查询
            List<DBModel.Person_FaceInfoModel> person_FaceInfoModel = YEasyModel.ModelDAL.Join<DBModel.Person_FaceInfoModel, 
                DBModel.Person_FaceInfoModel, DBModel.ST_PersonModel>((t1, t2) => YEasyModel.SqlColumnUtil.IsNull(t1.Person_ID,0) == t2.Person_ID,(t1,t2)=>t1.Remark ==null,
                null,(t1,t2)=>t1.Person_ID, (t1, t2) => t1.Person_No, (t1, t2) => t1.FacePath);

 
