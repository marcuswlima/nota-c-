using NotaNamespace;

class Program
{
    static void Main(string[] args)
    {
        Nota n1=new(3,3,-1);
        Console.WriteLine(n1.ToString());
        Nota n2=new(3);
        Console.WriteLine(n2.ToString());
        Intervalo i=new(n2,"2m");
        Console.WriteLine(i.ToString());
    }
}
