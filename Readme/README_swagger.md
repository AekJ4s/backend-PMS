# Generate API Spec with Swagger

```sh
# สำหรับกรณี เลือกใช้เวอร์ชัน หรือ ไม่ได้ติดตั้ง webapi ตั้งแต่เริ่มสร้าง project ก็ต้องเพิ่ม package นี้
dotnet add WebApi.csproj package Swashbuckle.AspNetCore -v 6.5.0
```

- https://learn.microsoft.com/th-th/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio

- API Spec เครื่องมือสำคัญที่ช่วยให้ Dev สามารถทำความเข้าใจกับการทำงานของ API ได้อย่างชัดเจน และช่วยให้สามารถสร้างและใช้งาน API ได้ถูกต้องปลอดภัย

- เพิ่ม Generate ในไฟล์ WebApi/WebApi.csproj

```cs
  <PropertyGroup>
    ...
    ...
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);1591</NoWarn>
  </PropertyGroup>
```

- เพิ่ม Version, Title, Description ของ API บนหน้า Swagger

```cs
using System.Reflection;

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo{
        Version = "v1",
        Title = "My WebApi Project API",
        Description = "A simple example ASP.NET Core Web API",
    });

    //using System.Reflection; สำหรับใช้งาน XML Comment
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
```

## XML Comment - Action & Response

- วิธีกรอกฆข้อมูล input แล้วจำลอง Response การเพิ่มข้อมูลให้ดูล่วงหน้ากรณี Success

ตัวอย่าง :

```
        /// <summary>
        /// Create Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>A newly created Employee</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Employee
        ///     {
        ///         "FirstName": "John",
        ///         "LastName": "Don",
        ///         "Salary": 25000,
        ///         "DepartmentId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">
        /// Success
        /// <br/>
        /// <br/>
        /// Example response:
        ///
        ///     {
        ///         "Code": 201,
        ///         "Message": "Success",
        ///         "Data": {
        ///             "Id": 1,
        ///             "FirstName": "John",
        ///             "LastName": "Doe",
        ///             "Salary": 25000,
        ///             "DepartmentId": 1
        ///         }
        ///     }
        ///
        /// </response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
```

### XML Comment - Attribute

- วิธีกำหนดค่าเริ่มต้นให้กับ fields ใน [FormBody] Request ได้ เช่น class, struct หรือกำหนดใน model ของ DbContext โดยตรง (กรณีใช้รับฟอร์ม Request)

ตัวอย่าง :

```cs
        public struct EmployeeCreate
        {
            /// <summary>
            /// Employee First Name
            /// </summary>
            /// <example>John</example>
            /// <required>true</required>
            [Required]
            [RegularExpression(@"^[a-zA-Z0-9]*$")]
            public string? FirstName { get; set; }

            /// <summary>
            /// Employee Last Name
            /// </summary>
            /// <example>Don</example>
            /// <required>true</required>
            [Required]
            [StringLength(50)]
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
```
