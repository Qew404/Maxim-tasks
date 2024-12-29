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
for (int i = 0;  i < input.Length; i++)
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

// Метод для переворота строки
static string ReverseString(string str)
{
    char[] charArray = str.ToCharArray();
    Array.Reverse(charArray);
    return new string(charArray);
}