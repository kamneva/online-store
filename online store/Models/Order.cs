using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Laba4.Models
{
    public class Order
    {
        [Display(Name = "Заказ UID")]
        public int OrderID { get; set; }

        [Display(Name = "№")]
        public string Number { get; set; }

        [Display(Name = "Покупатель")]
        public string Buyer { get; set; }

        [Display(Name = "Дата заказа")]
        public string OrderDate { get; set; }

        [Display(Name = "Статус заказа")]
        public string OrderStatus { get; set; }

        [Display(Name = "Способ доставки")]
        public string DeliveryMethod { get; set; }

        [Display(Name = "Дата получения")]
        public string DateOfReceiving { get; set; }

        [Display(Name = "Ответственный сотрудник")]
        public string ResponsibleOfficer { get; set; }

        [Display(Name = "Оплата")]
        public string Payment { get; set; }
    }
}