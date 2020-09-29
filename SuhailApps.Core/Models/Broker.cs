using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using SuhailApps.Core.Interfaces;
using SuhailApps.Core.Models.Identity;
using IModel = SuhailApps.Core.Interfaces.IModel;

namespace SuhailApps.Core.Models
{
    public class Broker: IModel,IDeletedModel,IAuditModel
    {

        public int Id { get; set; }

        public string MobileNo { get; set; }

        public string Name { get; set; }


        public string ImageId { get; set; }

        public string Description { get; set; }

        public string BrokerStatus { get; set; }

        public string BrokerType { get; set; }


        #region Foreign keys


        #endregion

        #region Navigation Properties

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        

        #endregion

        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
    }
}
