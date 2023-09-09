using System;
using System.Text.RegularExpressions;

namespace NotaNamespace
{
    class Triade
    {
//-----------------------------
// Fields
//-----------------------------
        public const int QTDTRIADES=4;
        private Intervalo int1;
        private Intervalo int2;
//-----------------------------
// Properties
//-----------------------------
        public Intervalo Int1{
            get {return int1;}
            set { 
                	string program="Intervalo.SetInt1"; 
                    if (!value.IntervalorValido())
                    {
                        throw new ArgumentException(program + " / Intervalo invalida / " + value.ToString());
                    }

                    int1 = value;
                }
        }
        public Intervalo Int2{
            get {return int2;}
            set { 
                	string program="Intervalo.SetInt2"; 
                    if (!value.IntervalorValido())
                    {
                        throw new ArgumentException(program + " / Intervalo invalida / " + value.ToString());
                    }

                    int2 = value;
                }
        }
//-----------------------------
// Constructors
//-----------------------------
        public Triade()
        {
        }
        public Triade(int dificuldade)
        {
        	string program="Triade(int)"; 
            if (!Nota.DificuldadeValida(dificuldade))
            {
                throw new ArgumentException(program + " / dificuldade invalida / " + dificuldade.ToString());
            }

            Randomizar(dificuldade);
        }
        public Triade(Nota n, char tipoTriade)
        {
            setTriade(n,tipoTriade);
        }
//-----------------------------
// Publics
//-----------------------------
        public void setTriade(Nota n, char tipoTriade)
        {
            {//validacoes
                string program="Triade(Nota,char)"; 
                if (!n.NotaValida())
                {
                    throw new ArgumentException(program + " / nota invalida / " + n.ToString());
                }
                if (!validarTipoTridade(tipoTriade))
                {
                    throw new ArgumentException(program + " / tipoTriade invalda / " + tipoTriade);
                }
            }
            MontarTriade(n,tipoTriade);
        }
        public void Randomizar(int dificuldade=1)
        {
        	Nota n=new(dificuldade);//randomizar a fundamental
            char tipoTriade=RandomizarTipoTriade();
            MontarTriade(n,tipoTriade);
        }
        public static void getTiposTriade(char []arr)
        {
            char[] tiposTriade={'M','m','A','d'};
            for (int i=0 ; i<QTDTRIADES ; i++){
                arr[i] = tiposTriade[i]; 
            }
        }
        public Nota GetFundamental()
        {
            return Int1.N1;
        }
        public Nota GetTerca()
        {
            return Int1.N2;
        }
        public Nota GetQuinta()
        {
            return Int2.N2;
        }
        public override string ToString()
        {
            return "[" + GetFundamental().ToString() + ':' +
                         GetTerca().ToString()       + ':' +
                         GetQuinta().ToString()      + "][" + 
                         Int1.GetTipoIntervalo()     +
                         Int2.GetTipoIntervalo()     + "][" +
                         GetTipoTriade() + "]";

        }
//-----------------------------
// Privates
//-----------------------------
        private string GetTipoTriade()
        {
            string tiposIntervalo="",resposta="";
            tiposIntervalo += Int1.GetTipoIntervalo().Substring(1,1);
            tiposIntervalo += Int2.GetTipoIntervalo().Substring(1,1);

            switch (tiposIntervalo)
            {
                case "Mm":resposta = "M" ; break;
                case "mM":resposta = "m" ; break;
                case "MM":resposta = "A" ; break;
                case "mm":resposta = "d" ; break;
            }

            return resposta;

        }
        private bool validarTipoTridade(char tipoTriade)
        {
            char[] tiposTriade=new char[QTDTRIADES];
            getTiposTriade(tiposTriade);
            bool resposta=false;
            for (int i=0; i<QTDTRIADES; i++){
                if (tiposTriade[i]==tipoTriade){
                    resposta=true;
                    break;
                }
            }
            return resposta;
        }
        private char RandomizarTipoTriade()
        {
            char[] tiposTriade=new char[QTDTRIADES];
            getTiposTriade(tiposTriade);
        	int aleatorio = Nota.GerarInteiro(1,QTDTRIADES);
            return tiposTriade[aleatorio-1];
        }

        private void MontarTriade(Nota n, char tipoTriade)
        {
            Intervalo int1=new(),int2=new();

            //Primeiro Intervalo
            int1.N1=n;
            if (tipoTriade=='M' || tipoTriade=='A'){
                int1.SetN2("3M");
            }else if (tipoTriade=='m' || tipoTriade=='d'){
                int1.SetN2("3m");
            }

            //Segundo Intervalo
            int2.N1=new(int1.N2);
            if (tipoTriade=='M' || tipoTriade=='d')
                int2.SetN2("3m");
            else if (tipoTriade=='m' || tipoTriade=='A')
                int2.SetN2("3M");

            //Console.Write(n.ToString()+'-'+tipoTriade+'\t'+int1.ToString()+'\t'+int2.ToString()+'\n');

            Int1 = int1;
            Int2 = int2;
        }
    }
}

