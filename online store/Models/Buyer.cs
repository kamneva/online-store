using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Laba4.Models {
    public class Buyer 
    {
        [Display(Name = "Покупатель UID")]
        public int BuyerID { get; set; }

        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Адрес")]
        public string Adress { get; set; }

        [Display(Name = "e-mail")]
        public string Mail { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Заказы покупателя")]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}