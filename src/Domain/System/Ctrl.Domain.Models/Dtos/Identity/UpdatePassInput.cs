using Ctrl.Core.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ctrl.Domain.Models.Dtos.Identity
{
    public class UpdatePassInput : IInputDto
    {
        public string UserID { get; set; }
      

        public string NewPassword { get; set; }

        public string NewPassword1 { get; set; }
    }
}
