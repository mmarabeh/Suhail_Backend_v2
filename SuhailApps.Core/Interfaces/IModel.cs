using System;
using System.Collections.Generic;
using System.Text;

namespace SuhailApps.Core.Interfaces
{
   public  interface IModel
   {
       string ToString();
   }

   public interface IDeletedModel
   {
       public bool IsDeleted { get; set; }
   }

   public interface IActiveModel
   {
       public bool Active { get; set; }
   }

   public interface IAuditModel
   {
       public DateTimeOffset CreatedAt { get; set; }

       public DateTimeOffset ModifiedAt { get; set; }


   }

   public class BaseModelGuid : IGuidModel
   {
       public Guid Id { get; set; }
   }

   public interface IGuidModel : IModel
   {
       Guid Id { get; set; }
   }

}
