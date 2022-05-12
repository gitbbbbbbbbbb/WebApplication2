using IService;
using Re;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MyService : IMyService
    {
        private readonly MyRe _MyRe;

        public MyService(MyRe myRe)
        {
            _MyRe = myRe;
        }

        public int Getid()
        {
            int i =_MyRe.getid();
            // throw   Tnew NotImplementedException();
            return i;
        }
    }
}
