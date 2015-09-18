﻿/****************** Copyright Notice *****************
 
This code is licensed under Microsoft Public License (Ms-PL). 
You are free to use, modify and distribute any portion of this code. 
The only requirement to do that, you need to keep the developer name, as provided below to recognize and encourage original work:

=======================================================
   
Architecture Designed and Implemented By:
Mohammad Ashraful Alam
Microsoft Most Valuable Professional, ASP.NET 2007 – 2013
Twitter: http://twitter.com/AshrafulAlam | Blog: weblogs.asp.net/ashraful | Github: https://github.com/ashrafalam
   
*******************************************************/
using System.Data.Entity;
using Eisk.Models;

namespace Eisk.DataAccess
{
    public partial class DatabaseContext : DbContext
    {
        public virtual IDbSet<Employee> EmployeeRepository { get; set; }
    }
}