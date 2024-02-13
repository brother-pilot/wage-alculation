using System;

namespace wageсalculation.Persistance
{
    public class Wage
    {
        /// <summary>
        /// class for wage data and case of culculation
        /// </summary>
        const int hourInMonth= 160;


        decimal monthWage;
        public decimal MonthWage { get { return monthWage; } }
        decimal bonus;
        public decimal Bonus { get { return bonus; } }
        bool isBonus;
        public bool IsBonus { get { return isBonus; }  }
        bool isMonthWage;
        public bool IsMonthWage { get { return isMonthWage; } }
        decimal hourWage;
        public decimal HourWage { get { return hourWage; } }
        
        public Wage(decimal monthWage, decimal bonus,  
            bool isMonthWage = true, bool isBonus=true,decimal hourWage =0)
        {
            if (monthWage == 0 && isMonthWage == true)
                throw new Exception("При работе по окладу зарплата не может быть 0");
            if (hourWage == 0 && isMonthWage == false)
                throw new Exception("При работе по часовой оплате оплата часа не может быть 0");
            if (monthWage != 0 && hourWage != 0)
                throw new Exception("Работник должен работать или по окладу или по часовой ставке");
            this.monthWage = monthWage;
            this.bonus = bonus;
            this.isBonus = isBonus;
            this.isMonthWage = isMonthWage;
            this.hourWage = IsMonthWage ==true?MonthWage / hourInMonth : hourWage;
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

        public decimal DayBonus => Bonus / hourInMonth;

        

    }
}