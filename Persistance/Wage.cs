using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace wageсalculation.Persistance
{
    public class Wage
    {
        /// <summary>
        /// class for wage data and case of culculation
        /// </summary>
        const int hourInMonth= 160;

        //для EF
        public int Id { get; set; }
        public decimal MonthWage { get; }
        public decimal Bonus { get; }
        public bool IsBonus { get; }
        public bool IsMonthWage { get; }
        public decimal HourWage { get; }
        
        //для EF
        public Wage()
        {

        }
        public Wage(decimal monthWage, decimal bonus,  
            bool isMonthWage = true, bool isBonus=true,decimal hourWage =0)
        {
            if (monthWage == 0 && isMonthWage == true)
                throw new Exception("При работе по окладу зарплата не может быть 0");
            if (hourWage == 0 && isMonthWage == false)
                throw new Exception("При работе по часовой оплате оплата часа не может быть 0");
            if (monthWage != 0 && hourWage != 0)
                throw new Exception("Работник должен работать или по окладу или по часовой ставке");
            MonthWage = monthWage;
            Bonus = bonus;
            IsBonus = isBonus;
            IsMonthWage = isMonthWage;
            HourWage = IsMonthWage ==true?MonthWage / hourInMonth : hourWage;
        }

        public decimal PayWage(int time)
        {
            if (time > hourInMonth)
            {
                if (IsBonus == true)
                    return (hourInMonth * HourWage)+(time-hourInMonth)* DayBonus;
                else
                    return hourInMonth * HourWage;
            }
            else
                return time * HourWage;
        }

        [NotMapped]
        public decimal DayBonus => Bonus / hourInMonth;

        public int? RoleId { get; set; } // внешний ключ
        public Role Role { get; set; }  // навигационное свойство

    }
}