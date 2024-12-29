class Program
{
    static void Main()
    {
        Console.WriteLine("Ввод строки:");
        string input = Console.ReadLine();
        // Проверка пустой строки
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Пустая строка");
            return;
        }

        // Список для хранения неподходящих символов
        List<char> invalidChars = new List<char>();
        // Проверка символов в строке
        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (c < 'a' || c > 'z')
            {
                //Добавление неподходящего символа в список
                invalidChars.Add(c);
            }
        }

        // Если есть неподходящие символы, выводим ошибку
        if (invalidChars.Count > 0)
        {
            // Выводим список неподходящих символов
            Console.WriteLine("Ошибка: Неподходящие символы: " + string.Join(", ", invalidChars));
            return;
        }

        string process;
        // Проверка четное/нечетное
        if (input.Length % 2 == 0)
        {
            // Четное кол-во символов
            int mid = input.Length / 2;
            string firstHalf = input.Substring(0, mid);
            string secondHalf = input.Substring(mid);

            // Переворачиваем 
            string reversedFirstHalf = ReverseString(firstHalf);
            string reversedSecondHalf = ReverseString(secondHalf);
            process = reversedFirstHalf + reversedSecondHalf;
        }
        else
        {
            // Нечетное кол-во символов
            string reversedInput = ReverseString(input);
            process = reversedInput + input;
        }

        Console.WriteLine("Результат: " + process);
        //Вызов метода для подсчета символов
        NumberOfProcessedStringsAndCharacters(process);
        //Вызов метода для поиска подстройки
        string longestVowelSubstring = FindLongestVowelSubstring(process);
        Console.WriteLine("Наибольшая подстрока: " + longestVowelSubstring);
    }
    // Метод для переворота строки
    static string ReverseString(string str)
    {
        char[] charArray = str.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    // Метод для обработки строки и подсчета символов
    static void NumberOfProcessedStringsAndCharacters(string processedStrings)
    {
        Console.WriteLine($"Обработанная строка: {processedStrings}");
        // Подсчет повторений всех символов 
        var charCount = processedStrings.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        // Выводим кол-во каждого символа
        var keys = charCount.Keys.ToList(); // Список ключей для доступа по индексу
        for (int i = 0; i < keys.Count; i++)
        {
            char key = keys[i];
            int value = charCount[key];
            Console.WriteLine($"{key}: {value} раз(а)");
        }
    }
    //Метод для поиска наибольшей подстройки, который начинается и заканчивается на гласную
    static string FindLongestVowelSubstring(string str)
    {
        //Гласные буквы
        string vowels = "aeiouy";
        //Переменная для наибольшей подстройки
        string longestSubsring = string.Empty;
        //Вложенные циклы для поиска подстроек
        for (int start = 0; start < str.Length; start++) //Переберает каждый символ как возможное начало подстроки
        {
            for (int end = start + 1; end < str.Length; end++) //Начинает с внешнего цикла и до конца, чтобы найти конец подстроки
            {
                //Проверка являются ли начальный и конечный символы гласными
                if (vowels.Contains(str[start]) && vowels.Contains(str[end]))
                {
                    //Извлечение подстроки
                    string substring = str.Substring(start, end - start + 1);
                    //Проверка на длинну
                    if (substring.Length > longestSubsring.Length)
                    {
                        //Обновляем наибольшую подстройку
                        longestSubsring = substring;
                    }
                }
            }
        }
        //Возвращаем наибольшую подстройку
        return longestSubsring;
    }
}