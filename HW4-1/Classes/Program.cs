namespace HW4_1.Classes;

public static class Program
{
    public static void Main(string[] args)
    {
        MyArrayList<int> integerArrayList = new MyArrayList<int>();

        MyArrayList<string> stringArrayList = new MyArrayList<string>();

        integerArrayList.Add(10);
        integerArrayList.Add(-4);
        integerArrayList.Add(52);
        integerArrayList.Add(-2048);

        integerArrayList.Sort();
        integerArrayList.Print();

        Console.WriteLine("Минимум: " + integerArrayList.Min());
        Console.WriteLine("Максимум: " + integerArrayList.Max());

        stringArrayList.Add("Sara");
        stringArrayList.Add("John");
        stringArrayList.Add("Randy");
        stringArrayList.Add("Paige");
        stringArrayList.Add("Brooke");

        stringArrayList.Sort();
        stringArrayList.Print();

        Console.WriteLine("Минимум: " + stringArrayList.Min());
        Console.WriteLine("Максимум: " + stringArrayList.Max());
    }
}