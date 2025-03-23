namespace ConsoleApplication.Views
{
    public abstract class Menu
    {
        private readonly Dictionary<string, Func<Task>> _options;
        private readonly Dictionary<string, string> _optionDescriptions;

        protected Menu(Dictionary<string, Func<Task>> options, Dictionary<string, string> optionDescriptions)
        {
            _options = options;
            _optionDescriptions = optionDescriptions;
        }

        public async Task ShowAsync()
        {
            while (true)
            {
                Console.Clear();
                DisplayHeader();
                // Отображаем пункты меню с их описаниями
                foreach (var option in _optionDescriptions)
                {
                    Console.WriteLine($"{option.Key}. {option.Value}");
                }

                DisplayExit();
                Console.Write("Выберите действие: ");

                var choice = Console.ReadLine()?.Trim().ToLower();
                if (choice == "q")
                {
                    return;
                }

                if (_options.ContainsKey(choice!))
                {
                    await _options[choice!]();
                }
                else
                {
                    Console.WriteLine("Неверный выбор.");
                }

                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }

        protected virtual void DisplayHeader()
        {
            Console.WriteLine("=== Меню ===");
        }

        protected virtual void DisplayExit()
        {
            Console.WriteLine("Q. Выйти из программы");
        }
    }
}
