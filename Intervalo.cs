using System;
using System.Text.RegularExpressions;

namespace NotaNamespace
{

    struct tRecDadosIntervalo
    {
//-----------------------------
// Fields
//-----------------------------
        private string tipoIntervalo;
        private int qtdNotasNaturais;
        private int qtdSemiTons;

//-----------------------------
// Constructos
//-----------------------------
        public tRecDadosIntervalo(string ti, int qtdNN, int qtdST){
            setIntervalo(ti, qtdNN, qtdST);
        }
//-----------------------------
// Properties
//-----------------------------
        public string TipoIntervalo
        {
            get {return tipoIntervalo;}
            set {tipoIntervalo = value;}
        }
        public int QtdNotasNaturais
        {
            get {return qtdNotasNaturais;}
            set {qtdNotasNaturais = value;}
        }
        public int QtdSemiTons
        {
            get {return qtdSemiTons;}
            set {qtdSemiTons = value;}
        }
//-----------------------------
// Publics
//-----------------------------
        public void setIntervalo(string ti, int qtdNN, int qtdST)
        {
            TipoIntervalo = ti;
            QtdNotasNaturais = qtdNN;
            QtdSemiTons = qtdST;

        }

    }

    class Intervalo
    {
//-----------------------------
// Fields
//-----------------------------
        public const int QTDINTERVALOS = 17;
        private Nota n1;
        private Nota n2;
//-----------------------------
// Properties
//-----------------------------
        public Nota N1{
            get {return n1;}
            set { 
                	string program="Intervalo.SetN1"; 
                    if (!value.NotaValida())
                    {
                        throw new ArgumentException(program + " / Nota invalida / " + value.ToString());
                    }

                    n1 = value;
                }
        }
        public Nota N2{
            get {return n2;}
            set { 
                	string program="Intervalo.SetN2"; 
                    if (!value.NotaValida())
                    {
                        throw new ArgumentException(program + " / Nota invalida / " + value.ToString());
                    }

                    n2 = value;
                }
        }

//-----------------------------
// Constructors
//-----------------------------
        public Intervalo()
        {
        }
        public Intervalo(int dificuldade)
        {
        	string program="Intervalo.Intervalo(int)"; 
            if (!Nota.DificuldadeValida(dificuldade))
            {
                throw new ArgumentException(program + " / dificuldade invalida / " + dificuldade.ToString());
            }

            Randomizar(dificuldade);
        }

        public Intervalo(Nota n, string tipoIntervalo){
            {//validacoes
                string program="Intervalo.Intervalo(Nota,string)"; 
                if (!n.NotaValida())
                {
                    throw new ArgumentException(program + " / intervalo invalido / " + n.ToString());
                }
                if (!StrEhIntervalo(tipoIntervalo))
                {
                    throw new ArgumentException(program + " / interalo invalda / " + tipoIntervalo);
                }
            }
            N1 = n;
            setN2(tipoIntervalo,1);
        }

//-----------------------------
// Publics
//-----------------------------
        public override string ToString()
        {
            string resposta="";

            resposta += N1.ToString();
            resposta += ":";
            resposta += N2.ToString() + " ";

            return resposta;
        }
        public void setN2(string tipoIntervalo, int orientacao = 1){
            {//validacoes
                string program="Intervalo.setN2(string,int)"; 
                if (!StrEhIntervalo(tipoIntervalo))
                {
                    throw new ArgumentException(program + " / intervalo invalido / " + tipoIntervalo);
                }
                if (!validarOrientacao(orientacao))
                {
                    throw new ArgumentException(program + " / orientacao invalda / " + orientacao.ToString());
                }
            }
            Nota n1=new(),n2=new();

            tRecDadosIntervalo intervalo=getQuantidades(tipoIntervalo);
            int qtdSemiTonsEntreAsDuasNotas=0, novoAcidente=0;
            
            n1 = N1;
            n2 = QualRelativa(n1,intervalo.QtdNotasNaturais,orientacao);

            qtdSemiTonsEntreAsDuasNotas = distanciaEmSemiTons(n1,n2);

            if (orientacao==1){
                novoAcidente = intervalo.QtdSemiTons - qtdSemiTonsEntreAsDuasNotas;
            }else{
                novoAcidente = qtdSemiTonsEntreAsDuasNotas    - intervalo.QtdSemiTons ;
            }

            n2.Acidente = novoAcidente;

            N2 = n2;

        }
        
        public void Randomizar(int dificuldade = 1)
        {
            Nota n=new(dificuldade);

            //randomizar intervalo
            tRecDadosIntervalo[] intervalos = new tRecDadosIntervalo[QTDINTERVALOS];
        	getIntervalos(intervalos);
        	int aleatorio = Nota.GerarInteiro(1,QTDINTERVALOS);

            this.N1 = n;

            setN2(intervalos[aleatorio-1].TipoIntervalo,1);
        }

        public static void getIntervalos(tRecDadosIntervalo []arr){


            tRecDadosIntervalo[] intervalos = {
                                                new tRecDadosIntervalo("1J",1, 1),
                                                new tRecDadosIntervalo("2m",2, 2),
                                                new tRecDadosIntervalo("2M",2, 3),
                                                new tRecDadosIntervalo("3m",3, 4),
                                                new tRecDadosIntervalo("3M",3, 5),
                                                new tRecDadosIntervalo("4d",4, 5),
                                                new tRecDadosIntervalo("4J",4, 6),
                                                new tRecDadosIntervalo("4A",4, 7),
                                                new tRecDadosIntervalo("5d",5, 7),
                                                new tRecDadosIntervalo("5J",5, 8),
                                                new tRecDadosIntervalo("5A",5, 9),
                                                new tRecDadosIntervalo("6m",6, 9),
                                                new tRecDadosIntervalo("6M",6,10),
                                                new tRecDadosIntervalo("7d",7,10),
                                                new tRecDadosIntervalo("7m",7,11),
                                                new tRecDadosIntervalo("7M",7,12),
                                                new tRecDadosIntervalo("8J",8,13)
                                              };
            for (int i=0 ; i<QTDINTERVALOS ; i++){
                arr[i].TipoIntervalo    = intervalos[i].TipoIntervalo; 
                arr[i].QtdNotasNaturais = intervalos[i].QtdNotasNaturais; 
                arr[i].QtdSemiTons      = intervalos[i].QtdSemiTons; 
            }

        }

//-----------------------------
// Privates
//-----------------------------
        private Nota QualRelativa(Nota n, int relativa, int orientacao){
            Nota resposta=new();

            int o=n.Oitava
               ,g=n.Grau
               ,a=n.Acidente;

            if (orientacao==1){
                g = g + relativa - 1;
                if (g>=8){
                    g -= 7;
                    o++;
                }
            }else if (orientacao==-1){
                g = g - relativa + 1;
                if (g<=0){
                    g += 7;
                    o--;
                }

            }

            resposta.Oitava=o;
            resposta.Grau=g;
            resposta.Acidente=a;

            return resposta;

        }
        private bool StrEhIntervalo(string tipoIntervalo){
            string pattern = @"^[1-8](J|M|m|A|d)$";
            return Regex.IsMatch(tipoIntervalo,pattern);
        }
        private bool validarOrientacao(int o )
        {
            return o == -1 || o == 1;
        }

        private tRecDadosIntervalo getQuantidades(string tipoIntervalo){
            tRecDadosIntervalo resposta=new();
            tRecDadosIntervalo[] intervalos = new tRecDadosIntervalo[QTDINTERVALOS];
        	getIntervalos(intervalos);

            for (int i=0; i<QTDINTERVALOS; i++){
                if (intervalos[i].TipoIntervalo==tipoIntervalo){
                    resposta = intervalos[i];
                    break; 
                }
            }
            return resposta;
        }

        int RetornarSubescrito(int n){
            int[] umaoitava={0,1,0,2,0,3,4,0,5,0,6,0,7};
            int resposta=0;
            for (int i=1;i<=12;i++){
                if (umaoitava[i]==n){
                    resposta = i;
                    break; 
                }
            }
            return resposta;
        }

        private int distanciaEmSemiTons(Nota n1,Nota n2){
            int g1   = n1.Grau          ,
                g2   = n2.Grau          ,
                i1   = RetornarSubescrito(g1),
                i2   = RetornarSubescrito(g2),
                resp = 0                     ;

            if (n1 < n2){ // intervalo ascendente
                if(g1 < g2){          // primeira nota com grau menor
                    resp = (i2-i1+1);
                }
                else if(g1 > g2) {    // segunda nota com grau menor
                    resp = (12-i1+1)+i2 ;
                }
                else if(g1==g2){
                    resp=13;
                };
            }else if (n1 > n2){ // intervalo descendente
                if(g1 < g2){           // primeira nota com grau menor
                    resp = (12-i2+1)+i1 ;
                }
                else if(g1 > g2) {     // segunda nota com grau menor
                    resp = (i1-i2+1);
                };
            }else {  // notas idênticas
                resp=1;

            }

            resp -= n1.Acidente;
            resp += n2.Acidente;
            return resp;

        }//distanciaEmSemitons
    }

}