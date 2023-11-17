using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wageсalculation
{
    partial class FormConsole
    {
		//класс для вывода сообщений пользователю

		public void ShowMessage(string message)
		{
			Console.WriteLine(message);
		}
		public void ShowDo(Dictionary<int, string> commandAccessKey)
        {
            Console.WriteLine("Выберите желаемое действие (введите номер)");
            foreach (var item in commandAccessKey)
                Console.WriteLine("(" + item.Key + ") " + item.Value);
        }

		public string ReadNotEmptyLine(string title)
		{
			while (true)
			{
				Console.WriteLine($"{title}: ");
				var input = Console.ReadLine();

				if (!string.IsNullOrWhiteSpace(input))
				{
					return input;
				}

				WriteErrorMessage($"Значение {title} не должен быть пустым");
			}
		}

		public int ReadIntLine(string title)
		{
			while (true)
			{
				var input = ReadNotEmptyLine(title);
				if (int.TryParse(input, out int result))
				{
					return result;
				}

				WriteErrorMessage($"Введите целое число");
			}
		}
		//private DateTime InputDate()
		//{
		//	DateTime date;
		//	while (!DateTime.TryParse(Console.ReadLine(), out date))
		//		Console.WriteLine("Неправильная команда. Введите номер еще раз");
		//	return date;
		//}
		public DateTime ReadNotEmptyDateTime(string title)
		{
				DateTime date;
				Console.WriteLine($"{title}: ");
				while (!DateTime.TryParse(Console.ReadLine(), out date))
					WriteErrorMessage("Неправильный формат даты. Введите дату в формате ГГГГ.ММ.ДД ");
				return date;
		}



		public void WriteErrorMessage(string message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ОШИБКА! {message}");
            Console.ForegroundColor = color;
        }
    }
}
