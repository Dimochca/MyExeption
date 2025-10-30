using System;
using System.IO;

public class MyCustomException : Exception
{
    public string ErrorCode { get; }

    public MyCustomException() : base("Произошло пользовательское исключение") { }

    public MyCustomException(string message) : base(message) { }

    public MyCustomException(string message, string errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    public MyCustomException(string message, Exception inner) : base(message, inner) { }
}

class Program
{
    static void Main(string[] args)
    {

        {
            try
            {

            }
            catch (MyCustomException ex)
            {
                Console.WriteLine($"Исключение: пользовательское исключение");
                Console.WriteLine($"Код ошибки: {ex.ErrorCode}");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Исключение: ArgumentNullException");
                Console.WriteLine($"Имя параметра: {ex.ParamName}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Исключение: ArgumentOutOfRangeException");
                Console.WriteLine($"Имя параметра: {ex.ParamName}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Исключение: DivideByZeroException");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Исключение: FileNotFoundException");
                Console.WriteLine($"Имя файла: {ex.FileName}");
            }
            finally
            {
                Console.WriteLine($"Блок finally выполнен");
            }
        }
        DemonstrateExceptions();
    }

    static void DemonstrateExceptions()
    {
        object[] testCases = new object[]
        {
            new object[] { "custom", null },
            new object[] { "argumentNull", "test" },
            new object[] { "divideByZero", 0 },
            new object[] { "fileNotFound", "nonexistent.txt" },
            new object[] { "indexOutOfRange", new int[5] }
        };

        foreach (object[] testCase in testCases)
        {
            string type = (string)testCase[0];
            object data = testCase[1];

            Console.WriteLine($"\n   Тест на {type}");

            try
            {
                switch (type)
                {
                    case "custom":
                        throw new MyCustomException("Собственное сключение");

                    case "argumentNull":
                        string text = null;
                        if (text == null)
                            throw new ArgumentNullException(nameof(text));
                        break;

                    case "divideByZero":
                        int divisor = (int)data;
                        int result = 10 / divisor;
                        break;

                    case "fileNotFound":
                        string fileName = (string)data;
                        if (!File.Exists(fileName))
                            throw new FileNotFoundException("Файл не существует", fileName);
                        break;

                    case "indexOutOfRange":
                        int[] array = (int[])data;
                        int value = array[10];
                        break;
                }
            }
            catch (MyCustomException ex)
            {
                Console.WriteLine($"Пользовательское исключение: {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"ArgumentNullException: {ex.Message}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"DivideByZeroException: {ex.Message}");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"FileNotFoundException: {ex.Message}");
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"IndexOutOfRangeException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Общее исключение: {ex.GetType().Name}: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("  Блок finally завершён");
            }
        }
    }
}
