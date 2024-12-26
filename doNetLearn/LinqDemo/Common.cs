using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace doNetLearn.LinqDemo
{
    internal class CommonMethod
    {
        /// <summary>
        /// 基本 SELECT 查询
        /// SELECT * FROM Employees;
        /// </summary>
        public void Method1()
        {
            //方法语法
            //var query = dbContext.Employees.ToList();

            //查询语法
            //var query = from e in dbContext.Employees  
            //select e;
        }

        /// <summary>
        /// 带有 WHERE 子句的 SELECT
        /// SELECT * FROM Employees WHERE Age > 30;
        /// </summary>
        public void Method2()
        {
            //var query = dbContext.Employees.Where(e => e.Age > 30).ToList();

            //var query = from e in dbContext.Employees  
            //where e.Age > 30
            //select e;
        }

        /// <summary>
        /// INNER JOIN 查询
        /// SELECT Employees.Name, Departments.Name FROM Employees 
        /// INNER JOIN Departments ON Employees.DepartmentId = Departments.Id;
        /// </summary>
        public void Method3()
        {
            //            var query = dbContext.Employees.Join(
            //    dbContext.Departments,
            //    e => e.DepartmentId,
            //    d => d.Id,
            //    (e, d) => new { EmployeeName = e.Name, DepartmentName = d.Name }
            //).ToList();


            //var query = from e in dbContext.Employees
            //            join d in dbContext.Departments
            //            on e.DepartmentId equals d.Id
            //            select new { e.Name, d.Name };

        }


        /// <summary>
        ///  GROUP BY 查询
        /// SELECT DepartmentId, COUNT(*)  FROM Employees  GROUP BY DepartmentId;
        /// </summary>
        public void Method4()
        {
            //        var query = dbContext.Employees
            //.GroupBy(e => e.DepartmentId)
            //.Select(g => new { DepartmentId = g.Key, EmployeeCount = g.Count() })
            //.ToList();

            //var query = from e in dbContext.Employees
            //            group e by e.DepartmentId into g
            //            select new { DepartmentId = g.Key, EmployeeCount = g.Count() };
        }

        /// <summary>
        /// COUNT 行数
        /// SELECT COUNT(*) FROM Employees;
        /// </summary>
        public void Method5()
        {
            //var count = dbContext.Employees.Count();
        }

        /// <summary>
        /// 排序依据
        /// SELECT * FROM Employees ORDER BY Name;
        /// </summary>
        public void Method6()
        {
            //var query = dbContext.Employees.OrderBy(e => e.Name).ToList();

            //var query = from e in dbContext.Employees
            //            orderby e.Name
            //            select e;
        }

        /// <summary>
        /// LEFT JOIN 查询
        /// SELECT Employees.Name, Departments.Name  FROM Employees 
        /// LEFT JOIN Departments ON Employees.DepartmentId = Departments.Id;
        /// </summary>
        public void Method7()
        {
            //            var query = dbContext.Employees.GroupJoin(
            //    dbContext.Departments,
            //    e => e.DepartmentId,
            //    d => d.Id,
            //    (e, d) => new { Employee = e, Departments = d.DefaultIfEmpty() }
            //).SelectMany(ed => ed.Departments.Select(d => new { ed.Employee.Name, DepartmentName = d?.Name }))
            //.ToList();

            //var query = from e in dbContext.Employees
            //            join d in dbContext.Departments on e.DepartmentId equals d.Id into dept
            //            from d in dept.DefaultIfEmpty()
            //            select new { e.Name, DepartmentName = d?.Name };

        }

        /// <summary>
        /// RIGHT JOIN 查询
        /// SELECT Employees.Name, Departments.Name   FROM Employees 
        /// RIGHT JOIN Departments ON Employees.DepartmentId = Departments.Id;
        /// </summary>
        public void Method8()
        {
            //            var query = dbContext.Departments.GroupJoin(
            //    dbContext.Employees,
            //    d => d.Id,
            //    e => e.DepartmentId,
            //    (d, e) => new { Department = d, Employees = e.DefaultIfEmpty() }
            //).SelectMany(de => de.Employees.Select(e => new { e?.Name, DepartmentName = de.Department.Name }))
            //.ToList();

            //var query = from d in dbContext.Departments
            //            join e in dbContext.Employees on d.Id equals e.DepartmentId into emp
            //            from e in emp.DefaultIfEmpty()
            //            select new { EmployeeName = e?.Name, d.Name };
        }

        /// <summary>
        /// FULL OUTER JOIN 查询
        /// SELECT Employees.Name, Departments.Name   FROM Employees
        /// FULL OUTER JOIN Departments ON Employees.DepartmentId = Departments.Id;
        /// </summary>
        public void Method9()
        {
            //var employees = dbContext.Employees.Select(e => new { e.Name, e.DepartmentId });
            //var departments = dbContext.Departments.Select(d => new { d.Name, DepartmentId = d.Id });

            //var query = (from e in employees
            //             join d in departments on e.DepartmentId equals d.DepartmentId into empDept
            //             from d in empDept.DefaultIfEmpty()
            //             select new { e.Name, DepartmentName = d?.Name })
            //            .Union(
            //             from d in departments
            //             join e in employees on d.DepartmentId equals e.DepartmentId into deptEmp
            //             from e in deptEmp.DefaultIfEmpty()
            //             select new { EmployeeName = e?.Name, d.Name }
            //            ).ToList();


            //var query = (from e in dbContext.Employees
            //             join d in dbContext.Departments on e.DepartmentId equals d.Id into empDept
            //             from d in empDept.DefaultIfEmpty()
            //             select new { e.Name, DepartmentName = d?.Name })
            //.Union(
            // from d in dbContext.Departments
            // join e in dbContext.Employees on d.Id equals e.DepartmentId into deptEmp
            // from e in deptEmp.DefaultIfEmpty()
            // select new { EmployeeName = e?.Name, d.Name }
            //);
        }

        /// <summary>
        /// CROSS JOIN 查询
        /// SELECT Employees.Name, Departments.Name   FROM Employees CROSS JOIN Departments;
        /// </summary>
        public void Method10()
        {
            //            var query = dbContext.Employees.SelectMany(
            //    e => dbContext.Departments,
            //    (e, d) => new { e.Name, d.Name }
            //).ToList();

            //var query = from e in dbContext.Employees
            //            from d in dbContext.Departments
            //            select new { e.Name, d.Name };
        }     
    }
}
