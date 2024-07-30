using System;
using System.Collections.Generic;
using CBEsApi.Dtos.CBEsDto;
using CBEsApi.Dtos.CBEsUserDto;

namespace CBEsApi.Models;

public partial class CbesLogHeaderDto
{
   public int Id { get; set; }

    public int? Round { get; set; }

    public string? Remark { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CbesLogTypeId { get; set; }

    public int? CbesLogId { get; set; }

    public int? CbesId { get; set; }

    public int? UpdateBy { get; set; }

    public virtual CBEsLogDto? CbesLog { get; set; }

    public virtual CbesLogType? CbesLogType { get; set; }

    public virtual CbesUserDto? UpdateByNavigation { get; set; }
}

public class CBEsLogHeaderForUpdate
    {
        public string? Remark { get; set; }

        public int? Round {get; set;}
        public virtual CBEsDto? Cbes { get; set; }
        public virtual CbesUserDto? UpdateByNavigation { get; set; }
    }
