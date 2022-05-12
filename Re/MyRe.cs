using model;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re
{
   public  class MyRe
    {
     public   int getid() {

          var a =  DbScoped.Sugar.Queryable<gg>().ToList()[0].Id;
        //数据库查询
            return a;
        }

    }
}
