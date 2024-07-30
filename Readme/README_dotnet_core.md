# Create a web project

```bash
dotnet new webapi --use-controllers -o WebApi
cd WebApi
dotnet add package Microsoft.EntityFrameworkCore.InMemory
code -r ../WebApi
```

```bash
touch .gitignore README.md
```

- .gitignore for untracked directory & files ( bin, obj )
- README.md for written documentation

# Initial Github

- Create a repository : dotnet-webapi to https://github.com/new

```bash
git init
git add .
git commit -m "Initial commit"
git branch -M main
git remote add origin https://github.com/jayrboy/dotnet-webapi.git
git push -u origin main
```

# SQL Server Management Studio

1. Create The Database

- Database name: Employee
- Options -> Collation: Thai_CI_AS

2. New Table: Employee (Name; Type; Allow Nulls)

ID; int; false
Firstname; nvarchar(50); true
Lastname; nvarchar(50); true
Salary; int; true
IsDelete; bit; true
CreateDate; datetime; true
UpdateDate; datetime; true

- Set Primary Key: ID
- Identity (Name): Employee
- Identity Column: ID

```bash
dotnet tool install --global dotnet-ef --version 8.*
```

- เพิ่ม InvariantGlobalization เป็น false ที่ไฟล์ WebApi.csproj

```cs
<PropertyGroup>
    ...
    ...
    <InvariantGlobalization>false</InvariantGlobalization>
</PropertyGroup>
```

- Create a folder: Models
- Add "ConnectionStrings": { "DefaultConnection": "..." } ที่ไฟล์ WebApi/appsettings.json

```json
{
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost; Database=Employee; Trusted_Connection=False; TrustServerCertificate=True; User ID=sa; Password=Password"
  }
}
```

## Scaffold Database มาใส่ Models

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
```

- Data Source=BUMBIM\SQLEXPRESS เอามาจาก SSMS

```bash
dotnet ef dbcontext scaffold "Data Source=BUMBIM\SQLEXPRESS;Initial Catalog=CBEsManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --context-dir Data --output-dir Models --force
```

- หากสำเร็จจะมีข้อมูล Table EmployeeContext จาก Database อยู่ใน Folder Data
- Model ทำหน้าที่จัดเตรียมข้อมูล เพื่อนำไปใช้
  - Data Logic
  - เป็นอิสระ ไม่ถูกกระทบจาก Controller
  - ตัวอย่าง: การเช็ค User ซ้ำใน Model User ควรอยู่ใน Model เมื่อนำไปใช้กับ Project อื่นก็ยังมีการเช็ค user ซ้ำได้เหมือนกัน ไม่ขึ้นกับ Controller

## Add Database Context Services by ConnectionStrings

```cs
using Microsoft.EntityFrameworkCore;
using WebApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

//TODO: Connect SQL Server Management Studio
builder.Services.AddDbContext<EmployeeContext>(option =>
  option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection");));
```

## Metadata file

- New C# -> Class -> EmployeeMetadata

```cs
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Models
{
  public class EmployeeMetadata { }

  [MetadataType(typeof(EmployeeMetadata))]
  public partial class Employee
  {
    public static Employee Create(EmployeeContext db, Employee employee) { }

    public static List<Employee> GetAll(EmployeeContext db) { }

    public static Employee GetById(EmployeeContext db, int id) { }

    public static Employee Update(EmployeeContext db, Employee employee) { }

    public static Employee Delete(EmployeeContext db, int id) { }

    public static List<Employee> Search(EmployeeContext db, string keyword) { }
  }
}
```

## Controller

- New C# -> Class -> EmployeeController

```cs
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private EmployeeContext _db = new EmployeeContext();

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        public struct EmployeeCreate
        {
            /// <summary>
            /// Employee First Name
            /// </summary>
            /// <example>John</example>
            /// <required>true</required>
            [Required]
            public string? FirstName { get; set; }

            /// <summary>
            /// Employee Last Name
            /// </summary>
            /// <example>Don</example>
            /// <required>true</required>
            [Required]
            public string? LastName { get; set; }

            /// <summary>
            /// Employee Salary
            /// </summary>
            /// <example>25000</example>
            /// <required>true</required>
            [Required]
            [Range(15000, 80000)]
            public int? Salary { get; set; }

            /// <summary>
            /// Employee Department ID
            /// </summary>
            /// <example>1</example>
            /// <required>true</required>
            [Required]
            [Range(1, 5)]
            public int? DepartmentId { get; set; }
        }

        [HttpPost(Name = "CreateEmployee")]
        public ActionResult Create(EmployeeCreate employeeCreate) { }

        [HttpGet(Name = "GetAllEmployee")]
        public ActionResult GetAll() { }

        [HttpGet("{id}", Name = "GetEmployeeById")]
        public ActionResult GetById(int id) { }

        [HttpPut(Name = "UpdateEmployee")]
        public ActionResult Update(Employee employee) { }

        [HttpDelete("{id}", Name = "DeleteEmployeeById")]
        public ActionResult DeleteById(int id) { }

        [HttpGet("search/{name}", Name = "SearchEmployeeByName")]
        public ActionResult Search(string name) { }

        [HttpGet("page/{page}", Name = "GetAllEmployeeByPage")]
        public ActionResult GetAllByPage(int page) { }
    }
}
```
