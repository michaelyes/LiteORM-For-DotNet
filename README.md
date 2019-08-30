# LiteORM-For-DotNet
轻量级高性能的DotNet ORM框架，解决C#.Net开发过程中重复繁琐的数据库CURD操作

项目开源库结构介绍：

1、YEasyModel

主要实体类反射类库，定义实体类字段的数据类型、长度、主键等特性；定义CURD方法，查询参数表达式、排序表达式。利用lambda定义查询逻辑，生成sql过滤条件；查询/更新字段定义，通过反射生成对应的Sql参数；排序逻辑定义，生成字段排序规则；DataTable与实体类转换方法。

2、ModelApp

winform程序，用于配置连接数据库，定义命名空间、实体类名，生成指定的表/视图的实体模型；

3、WebDemo

Webapi范例，简单的表实体模型使用说明；

使用介绍：
    在您开发的工程项目，引用YEasyModel类库。然后，用ModelApp程序生成您需要操作数据库表实体模型，就可以使用ModelDAL类里面的方法去实现CURD操作。
    ModelUtil类是DataTable与实体转换工具类

            /******************************************使用示例******************************************/
            
                        //根据ID查询 Person_FaceInfoModel 对应表的所有记录
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
            YEasyModel.ModelDAL.SelectTopRecord<DBModel.Person_FaceInfoModel>(q => q.FaceID == id);
                        //按条件查询数据
            YEasyModel.ModelDAL.Select<DBModel.Person_FaceInfoModel>(q => q.Create_Date > DateTime.Now.AddDays(-1) 
                        && q.Create_Date < DateTime.Now && q.Remark != "");
                        //按条件查询、指定字段1、字段2...
            YEasyModel.ModelDAL.SelectTopRecord<DBModel.Person_FaceInfoModel>(q => q.FaceID == id, null, q => q.Person_ID, q => q.FacePath);
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
                        //DataRow数据转实体
            YEasyModel.ModelUtil.DataRowParse<DBModel.Person_FaceInfoModel>(dt.Rows[0]);
            
            YEasyModel.ModelDAL.Select<DBModel.Person_FaceInfoModel>(q => q.FaceID == id)[0];//根据FaceID查询记录

            /******************************************使用示例******************************************/
            
（暂时先写这些，后面有时间再完善）
