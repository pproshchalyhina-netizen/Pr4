using System;

#region Завдання 1. Вкладення класів
// Базовий клас
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Role { get; set; }

    public Person(string name, string role, int age)
    {
        Name = name;
        Age = age;
        Role = role;
    }

    public virtual void MyRating() { }
}

// Клас StudentAssesment — обчислення рейтингу за модуль
class StudentAssesment
{
    private readonly string[] subject = { "C#", "ООП", "Основи SE", "БД", "Англійська", "WEB", "Операційні системи", "Алгоритми", "Математика", "Філософія" };
    private double[] assessment = new double[10];

    // Метод для заповнення випадковими оцінками (56-100)
    public void StRating()
    {
        Random rnd = new Random();
        for (int i = 0; i < assessment.Length; i++)
        {
            assessment[i] = rnd.Next(56, 101);
        }
    }

    // Метод для обчислення середнього рейтингу модуля
    public double StudentRating()
    {
        double rating = 0;
        foreach (double d in assessment)
            rating += d;
        return rating / assessment.Length;
    }
}

// Клас Student, який містить два екземпляри StudentAssesment
class Student : Person
{
    public string Faculty { get; set; }
    public string Group { get; set; }
    public int Course { get; set; }

    // Два модулі
    private StudentAssesment strating1 = new StudentAssesment();
    private StudentAssesment strating2 = new StudentAssesment();

    public Student(string name, string role, int age, string faculty, string group, int course)
        : base(name, role, age)
    {
        Faculty = faculty;
        Group = group;
        Course = course;
    }

    public override void MyRating()
    {
        // Заповнення балів
        strating1.StRating();
        strating2.StRating();

        // Обчислення
        double rating1 = strating1.StudentRating();
        double rating2 = strating2.StudentRating();
        double avgSemester = (rating1 + rating2) / 2;

        Console.WriteLine($"\nРейтинг за 1 модуль: {rating1:F2}");
        Console.WriteLine($"Рейтинг за 2 модуль: {rating2:F2}");
        Console.WriteLine($"Середній рейтинг за семестр: {avgSemester:F2}");

        if (avgSemester >= 82)
            Console.WriteLine("Привіт відмінникам!");
        else if (avgSemester <= 60)
            Console.WriteLine("Перездача! Треба краще вчитися!");
        else
            Console.WriteLine("Можна вчитися ще краще!");
    }
}
#endregion

#region Завдання 2–3. Вкладені та часткові класи
// Частковий клас Faculty
partial class Faculty
{
    public string FacultyName { get; set; }

    public Faculty(string name)
    {
        FacultyName = name;
    }

    // Вкладений клас Кафедра
    public partial class Department
    {
        private string Name;
        private int TeachersCount;
        private string[] disciplines;

        public void SetName(string name) => Name = name;
        public void SetTeachersCount(int count) => TeachersCount = count;

        // Індексатор для дисциплін
        public string this[int index]
        {
            get => disciplines[index];
            set => disciplines[index] = value;
        }

        public void InitDisciplines(int n)
        {
            disciplines = new string[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Введіть назву дисципліни {i + 1}: ");
                disciplines[i] = Console.ReadLine();
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Кафедра: {Name}, викладачів: {TeachersCount}");
            if (disciplines != null)
            {
                Console.WriteLine("Дисципліни кафедри:");
                foreach (var d in disciplines)
                    Console.WriteLine(" - " + d);
            }
        }
    }
}

// Друга частина класу Faculty
partial class Faculty
{
    private Department dep1 = new Department();
    private Department dep2 = new Department();

    public void InitDepartments()
    {
        dep1.SetName("Комп'ютерних наук та ІПЗ");
        dep1.SetTeachersCount(10);

        dep2.SetName("Вищої математики");
        dep2.SetTeachersCount(5);

        Console.WriteLine($"\nФакультет: {FacultyName}");
        dep1.ShowInfo();
        dep2.ShowInfo();
    }
}
#endregion

#region Завдання 4. Статичний клас
static class ArrayUtilities
{
    // 1) Лінійний пошук максимуму і мінімуму
    public static void FindMinMax(int[] arr)
    {
        int min = arr[0], max = arr[0];
        foreach (int i in arr)
        {
            if (i < min) min = i;
            if (i > max) max = i;
        }
        Console.WriteLine($"\nМінімальний елемент: {min}");
        Console.WriteLine($"Максимальний елемент: {max}");
    }

    // 2) Двійковий пошук
    public static int BinarySearch(int[] arr, int value)
    {
        Array.Sort(arr);
        int left = 0, right = arr.Length - 1;
        while (left <= right)
        {
            int mid = (left + right) / 2;
            if (arr[mid] == value)
                return mid;
            else if (arr[mid] < value)
                left = mid + 1;
            else
                right = mid - 1;
        }
        return -1;
    }
}
#endregion

class Program
{
    static void Main()
    {
        // === Завдання 1 ===
        var student = new Student("Шевченко", "студент", 19, "МНТУ", "К31", 3);
        Console.WriteLine($"Студент: {student.Name}, факультет: {student.Faculty}, група: {student.Group}, курс: {student.Course}");
        student.MyRating();

        // === Завдання 2–3 ===
        Faculty faculty = new Faculty("Комп'ютерних наук");
        faculty.InitDepartments();

        // === Завдання 4 ===
        int[] arr = { 4, 5, 2, 3, 8, 7, 6, 1 };
        ArrayUtilities.FindMinMax(arr);

        int[] bigArr = new int[100];
        for (int i = 0; i < 100; i++) bigArr[i] = i + 1;
        int searchValue = 73;
        int index = ArrayUtilities.BinarySearch(bigArr, searchValue);
        Console.WriteLine($"\nЕлемент {searchValue} {(index != -1 ? $"знайдено під індексом {index}" : "не знайдено")}");

        Console.ReadKey();
    }
}
