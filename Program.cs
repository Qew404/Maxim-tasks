using System.IO.IsolatedStorage;

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
        //Выбор сортировки
        Console.WriteLine("Выбирите желаемый алгоритм сортировки (1 - Быстрая сортировка, 2 - Сортировка деревом)");
        string choise = Console.ReadLine();
        string sortedResult = string.Empty;
        if (choise == "1")
        {
            sortedResult = QuickSort(process.ToCharArray());
        }
        else if (choise == "2")
        {
            sortedResult = TreeSort(process.ToCharArray());
        }
        else
        {
            Console.WriteLine("Неверный выбор сортировки");
            return;
        }
        Console.WriteLine("Отсортированная обработанная строка:" + sortedResult);

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
    //Метод бля быстрой сортировки
    static string QuickSort (char[] Array)
    {
        if (Array.Length <= 1) return new string(Array);
        //Выбор опорного элимента
        char pivot = Array[Array.Length / 2];
        //Элименты больше опорного
        char[] greater = Array.Where(x => x > pivot).ToArray();
        //Элименты меньше опорного
        char[] less = Array.Where(x => x < pivot).ToArray();
        //Элименты равны опорному
        char[] equal = Array.Where(x => x == pivot).ToArray();
        //Рекурсивная сортировка и объединение результата
        return QuickSort(less) + new string(equal) + QuickSort(greater);
    }
    public class TreeNode
    {
        public char Value;
        public TreeNode Left;
        public TreeNode Right;
        public TreeNode(char value)
        {
            Value = value;
        }
    }
    //Метод сортировки деревом
    static string TreeSort(char[] array)
    {
        TreeNode root = null;
        // Вставка в дерево элимента
        foreach (char value in array)
        {
            root = Insert(root, value);
        }

        // Отсортированные элименты
        List<char> sortedList = new List<char>();
        InOrderTraversal(root, sortedList);

        return new string(sortedList.ToArray());
    }

    // Метод для вставки элемента в дерево
    static TreeNode Insert(TreeNode node, char value)
    {
        if (node == null)
        {
            return new TreeNode(value); // Создаем новый узел
        }

        if (value < node.Value)
        {
            node.Left = Insert(node.Left, value); // Вставляем в левое поддерево
        }
        else
        {
            node.Right = Insert(node.Right, value); // Вставляем в правое поддерево
        }

        return node; // Возвращаем текущий узел
    }

    // Метод для обхода дерева в порядке возрастания
    static void InOrderTraversal(TreeNode node, List<char> sortedList)
    {
        if (node != null)
        {
            InOrderTraversal(node.Left, sortedList); // Обход левого поддерева
            sortedList.Add(node.Value); // Добавляем текущий узел
            InOrderTraversal(node.Right, sortedList); // Обход правого поддерева
        }
    }
}