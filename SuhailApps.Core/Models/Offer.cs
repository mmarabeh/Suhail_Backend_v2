using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SuhailApps.Core.Models
{
    public class Offer
    {

        public int Id { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        [Column(TypeName ="jsonb")]
        public string OfferData { get; set; }


    }
}
