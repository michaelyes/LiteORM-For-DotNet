using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Web.Http;
using YEasyModel;

namespace WebDemo.Controllers
{
    public class DemoController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<DBModel.Person_FaceInfoModel> Get()
        {
            List<DBModel.Person_FaceInfoModel> person_FaceInfoModel = YEasyModel.ModelDAL.Join<DBModel.Person_FaceInfoModel, 
                DBModel.Person_FaceInfoModel, DBModel.ST_PersonModel>((t1, t2) => YEasyModel.SqlColumnUtil.IsNull(t1.Person_ID,0) == t2.Person_ID,(t1,t2)=>t1.Remark ==null,
                null,(t1,t2)=>t1.Person_ID, (t1, t2) => t1.Person_No, (t1, t2) => t1.FacePath);
            //YEasyModel.ModelDAL.JoinLeft<DBModel.Person_FaceInfoModel, DBModel.ST_PersonModel, DBModel.ST_PersonModel > ((t1, t2,t3) => t1.Person_ID == t2.Person_ID&& t3.Person_ID==int.Parse(t1.Remark)&& t1.Remark!=null);


            var dt = YEasyModel.ModelDAL.SelectForDataTable<DBModel.Person_FaceInfoModel>();
            var list = YEasyModel.ModelUtil.DataTableParse<Models.FaceExt>(dt);
            System.Collections.SortedList ss = new System.Collections.SortedList();

            //查询 Person_FaceInfoModel 对应表的所有记录
            return YEasyModel.ModelDAL.Select<DBModel.Person_FaceInfoModel>();
        }

        // GET api/<controller>/5
        public DBModel.Person_FaceInfoModel Get(int id)
        {
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
                        //DataRow数据转实体
            YEasyModel.ModelUtil.DataRowParse<DBModel.Person_FaceInfoModel>(dt.Rows[0]);
            

            return YEasyModel.ModelDAL.Select<DBModel.Person_FaceInfoModel>(q => q.FaceID == id)[0];//根据FaceID查询记录
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

    }
}