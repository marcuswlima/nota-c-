using NotaNamespace;


class Program
{
    static void Main(string[] args)
    {
        Console.Write("Digite a quantidade desejada : ");   
        int qtd = int.Parse(Console.ReadLine());    

        GerarNotas(qtd); 
        GerarIntervalos(qtd); 
        GerarTriades(qtd); 
        //TesteAvulso();
        //ApresentarNotas();
        //ApresentarIntervalos();
        //ApresentarTriades();
    }
    static void GerarNotas(int quatidade)
    {
        Console.Write("*** Notas Geradas ***\n");   
        Nota n=new();
        for (int i=1; i<=quatidade; i++)
        {
            n.Randomizar();
            Console.Write(n.ToString() + ' ');
        }
        Console.Write('\n');   

    }
    static void GerarIntervalos(int quatidade)
    {
        Console.Write("*** Intervalos Gerados ***\n");
        Intervalo intervalo=new();
        for (int i=1; i<=quatidade; i++)
        {
            intervalo.Randomizar();
            Console.Write(intervalo.ToString()+ ' ');
        }
        Console.Write('\n');   
    }
    static void GerarTriades(int quatidade)
    {
        Console.Write("*** Triades Gerados ***\n");
        Triade triade=new();
        for (int i=1; i<=quatidade; i++)
        {
            triade.Randomizar();
            Console.Write(triade.ToString()+ ' ');
        }
        Console.Write('\n');   
    }

    static void TesteAvulso(){
        {
            Nota n=new(3,1,0);
            Intervalo i=new();
            i.N1=n;
            i.SetN2("2M");
            Console.Write(i.ToString()+'\n');
        }
/*
        {//nota
            Console.Write("**Nota**\n");
            Nota n1=new(3,1,0), n2=new(3,3,-1);
            Intervalo i=new(n1,n2);
            Console.Write(i.ToString()+'\n');
        }

        {//Triade
            Console.Write("**Triade**\n");
            Nota n1=new(3,1,0);
            Triade triade=new(n1,'d');
            Console.Write(triade.ToString()+'\n');
        }
        {
            char[] tiposTriade=new char[Triade.QTDTRIADES];
            Triade.getTiposTriade(tiposTriade);
        	int aleatorio;
            Console.Write(Triade.QTDTRIADES+"\n\n");
            for (int i=1; i<100; i++){
                aleatorio = Nota.GerarInteiro(1,Triade.QTDTRIADES);
                Console.Write(aleatorio+" ");
            }
        }
*/
    }
    static void ApresentarTriades()
    {
        Console.Write("*** Triades ***\n");   

        //infra
        Nota n=new(3,1,0);
        Triade triade=new();

        {//Randomizar
            Console.Write("Digite a quantidade de traiades geradas: ");   
            int qtd = int.Parse(Console.ReadLine());     
            for (int i=1; i<=qtd; i++)
            {
                triade.Randomizar();
                Console.Write(triade.ToString()+ ' ');
            }
            Console.Write('\n');   
        }
    }
    static void ApresentarIntervalos(){
        Console.Write("*** Intervalos ***\n");   

        //infra
        Nota n=new(3,1,0); //terceira oitrava, MI, sem acidete
        Intervalo intervalo=new();
        tRecDadosIntervalo[] dadosIntervalo = new tRecDadosIntervalo[Intervalo.QTDINTERVALOS];
        Intervalo.getIntervalos(dadosIntervalo);

        {//titulo
            Console.Write("F\t");   
            for (int i=0; i < Intervalo.QTDINTERVALOS; i++){
                Console.Write(dadosIntervalo[i].TipoIntervalo+'\t');   
            }
            Console.Write('\n');   
        }

        {//montar intervalos
            for (int i=1; i<=14; i++){
                Console.Write(n.ToString() + '\t');

                intervalo.N1 = n;
                for (int y=0; y<Intervalo.QTDINTERVALOS; y++){
                    intervalo.SetN2(dadosIntervalo[y].TipoIntervalo,1);
                    Console.Write(intervalo.N2.ToString() + '\t');
                }
                n.Up1SemiTom();
                Console.Write('\n');   
            }
        }

        {//Randomizar
            Console.Write("Digite a quantidade de intervalos: ");   
            int qtd = int.Parse(Console.ReadLine());     
            for (int i=1; i<=qtd; i++)
            {
                intervalo.Randomizar(1);
                Console.Write(intervalo.ToString()+ ' ');
            }
            Console.Write('\n');   
        }
    }
    static void ApresentarNotas(){
        Console.Write("*** Notas ***\n");   

        Nota n=new(3,1,0); //terceira oitrava, MI, sem acidete
        Console.Write("Digite a quantidade de notas: ");   
        int qtdNotas = int.Parse(Console.ReadLine());     
        for (int i=1; i<=qtdNotas; i++)
        {
            Console.Write(n.ToString() + ' ');
            n.Up1SemiTom();
        }
        Console.Write("\n\n");
        //randomizar notas
        for (int i=1; i<=qtdNotas; i++)
        {
            n.Randomizar(3);
            Console.Write(n.ToString() + ' ');
        }
        Console.Write('\n');

    }

}
