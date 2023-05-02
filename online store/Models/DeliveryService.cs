using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Laba4.Models
{
    public class DeliveryService
    {
        [Display(Name = "Служба доставки UID")]
        public int DeliveryServiceID { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Заказы службы доставки")]
        public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
    }
}
