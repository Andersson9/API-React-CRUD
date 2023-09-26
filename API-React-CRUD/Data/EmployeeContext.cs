using API_React_CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace API_React_CRUD.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options): base(options)
        {

        }

        public DbSet<Employee> Employee { get; set; }


    }
}