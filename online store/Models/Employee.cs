using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Laba4.Models
{
    public class Employee
    {
        [Display(Name = "Сотрудник UID")]
        public int EmployeeID { get; set; }

        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Display(Name = "Заказы сотрудника")]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
