using System;

namespace wageсalculation
{
    public class Wage
    {
        const int hourInMonth= 160;
        public decimal MonthWage { get; private set; }
        public decimal Bonus { get; private set; }
        public bool IsBonus { get; private set; }
        public bool IsMonthWage { get; private set; }
        public decimal HourWage { get; private set; }

        public Wage(decimal monthWage, decimal bonus,  
            bool isMonthWage = true, bool isBonus=true,decimal hourWage =0)
        {
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
        public decimal DayBonus => Bonus / hourInMonth;
    }
}