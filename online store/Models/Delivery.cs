using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Laba4.Models
{
    public class Delivery
    {
        [Display(Name = "Доставка UID")]
        public int DeliveryID { get; set; }

        [Display(Name = "Заказ")]
        public string Order { get; set; }

        [Display(Name = "Служба доставки")]
        public string DeliveryService { get; set; }

        [Display(Name = "Дата доставки")]
        public string DeliveryDate { get; set; }

        [Display(Name = "Доставка заказов")]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
