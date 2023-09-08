using System;

namespace NotaNamespace
{
    class Nota
    {
        //-----------------------------
        // Fields
        //-----------------------------
        public const int QTDNOTAS = 7;
        private int oitava;
        private int grau;
        private int acidente;
        //-----------------------------
        // Properties
        //-----------------------------
        public int Oitava{
            get {return oitava;}
            set { 
                	string program="Nota.SetOitava"; 
                    if (!OitavaValida(value))
                    {
                        throw new ArgumentException(program + " / oitava invalida / " + value.ToString());
                    }

                    oitava = value;
                }
        }
        public int Grau{
            get {return grau;}
            set { 
                	string program="Nota.SetGrau"; 
                    if (!GrauValido(value))
                    {
                        throw new ArgumentException(program + " / grau invalida / " + value.ToString());
                    }

                    grau = value;
                }
        }

        public int Acidente{
            get {return acidente;}
            set { 
                	string program="Nota.SetAcidente"; 
                    if (!AcidenteValido(value))
                    {
                        throw new ArgumentException(program + " / acidente invalida / " + value.ToString());
                    }

                    acidente = value;
                }
        }

//-----------------------------
// Constructors
//-----------------------------
        public Nota()
        {
        }

        public Nota(int dificuldade)
        {
        	string program="Nota.Nota(int)"; 
            if (!DificuldadeValida(dificuldade))
            {
                throw new ArgumentException(program + " / dificuldade invalida / " + Convert.ToString(dificuldade));
            }

            Randomizar(dificuldade);
        }

        public Nota(int oitava, int grau, int acidente)
        {
            SetNota(oitava,grau,acidente);
        }

        public Nota(Nota nota)
        {
            Oitava = nota.Oitava;
            Grau = nota.Grau;
            Acidente = nota.Acidente;
        }

//-----------------------------
// Operadores
//-----------------------------
/*
        public static bool operator == (Nota l, Nota r) {
            return NotaIgual(l,r);
        }

        public static bool operator != (Nota l, Nota r) {
            return (!NotaIgual(l,r));
        }
*/
        public static bool operator < (Nota l, Nota r) {
            return !NotaIgual(l,r) && !PrimeiraMaior(l,r);
        }
        public static bool operator > (Nota l, Nota r) {
            return !NotaIgual(l,r) && PrimeiraMaior(l,r);
        }
        public bool GreaterThan(Nota nota)
        {
            // Implement your own logic here
            return false;
        }

        public bool LessThan(Nota nota)
        {
            // Implement your own logic here
            return false;
        }
//-----------------------------
// Publics
//-----------------------------
        public override string ToString()
        {

            string strNota;

            strNota = Oitava.ToString();

            switch (Grau)
            {
                case 1:strNota+="Do" ;break;
                case 2:strNota+="Re" ;break;
                case 3:strNota+="Mi" ;break;
                case 4:strNota+="Fa" ;break;
                case 5:strNota+="Sol";break;
                case 6:strNota+="La" ;break;
                case 7:strNota+="Si" ;break;
            default:
                break;
            }

            switch (Acidente)
            {
                case -2:strNota += "bb";break;
                case -1:strNota += "b";break;
                case  1:strNota += "#";break;
                case  2:strNota += "*";break;
            default:
                break;
            }

            return strNota;
        }

        public void Randomizar(int in_dificuldade = 1)
        {
        	int oitava=0;
            int grau=0;
            int acidente=0;
            do{
                oitava = GerarInteiro(1,7);
                grau = GerarInteiro(1,7);

                switch (in_dificuldade)
                {
                    case 1:acidente = 0;break;
                    case 2:acidente = GerarInteiro(-1,1);break;
                    case 3:acidente = GerarInteiro(-2,2);break;
                    default:break;
                }

                Oitava = oitava;
                Grau = grau;
                Acidente = acidente;

            }while(!NotaValida());
        }

        static Random random;
        public static int GerarInteiro(int menor, int maior)
        {
            // Implement your own logic here
            if (random == null) 
                random = new Random();

            if (maior > menor){
                return random.Next(menor, maior);
            }
            else
                return 0;
        }

        public static bool DificuldadeValida(int dificuldade)
        {
        	return dificuldade >= 1 && dificuldade <= 3;
        }

        public bool NotaValida()
        {
            return OitavaValida(Oitava) && GrauValido(Grau) && AcidenteValido(Acidente);
        }

        public Nota QualRelativa(int qdtNotasDoIntervaloDesejado, int orientacao=1)
        {
            // Implement your own logic here
            return new Nota();
        }

//-----------------------------
// Privates
//-----------------------------
        private static bool NotaIgual(Nota n1, Nota n2)
        {
            return n1.Oitava==n2.Oitava && n1.Grau==n2.Grau && n1.Acidente==n2.Acidente;
        }
        private static bool PrimeiraMaior(Nota n1, Nota n2)
        {
            bool resposta=false;
            if (!NotaIgual(n1,n2)){
                if (n1.Oitava != n2.Oitava) {
                    resposta = n1.Oitava > n2.Oitava;
                }else if (n1.Grau != n2.Grau) {
                    resposta = n1.Grau > n2.Grau;
                }else if (n1.Acidente != n2.Acidente) {
                    resposta = n1.Acidente > n2.Acidente;
                }
            }
            return resposta;
        }
        private void SetNota(int oitava, int grau, int acidente)
        {
            Oitava = oitava;
            Grau = grau;
            Acidente = acidente;
        }

        private bool OitavaValida( int o ){
            // Sete oitavas completas em um piano [1:8]
            return o >= 1 && o <= 8;
        }

        private bool GrauValido( int g ){
            //graus validos [1:7]
            return g >= 1 && g <= 7;
        }

        private bool AcidenteValido( int a ){
            //acidentes validos [-2:2]
            return a >= -2 && a <= 2;
        }

//-----------------------------------
        public void SetNota(string nota)
        {
            // Implement your own logic here
        }

        public void Up1SemiTom()
        {
            // Implement your own logic here
        }

        public void Up1Tom()
        {
            // Implement your own logic here
        }

        public void Down1SemiTom()
        {
            // Implement your own logic here
        }

        public void Down1Tom()
        {
            // Implement your own logic here
        }

        public static bool StrEhNota(string str)
        {
            // Implement your own logic here
            return false;
        }

        public static void GetNotas(string[] notas)
        {
            // Implement your own logic here
        }

    }
}
