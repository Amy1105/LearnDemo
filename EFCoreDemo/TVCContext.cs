using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo
{
    internal class TVCContext: DbContext
    {
        //Scaffold-DbContext 'Name=ConnectionStrings:Chinook' Microsoft.EntityFrameworkCore.SqlServer -Tables OutOrderHeader, OutOrderDetail,OutOrderSample,OutWipOrder
        //"DbConnectionString": "Data Source= 10.201.16.11 ;Initial Catalog=tvc_test2;Persist Security Info=True;User ID=tvc_test;Password=Vgh%@jhAd930;",
    }
}
