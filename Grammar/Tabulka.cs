using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammar
{
    struct Pozice
    {
        int x;
        int y;
    }
    
    class Tabulka
    {
        List<List<Symbol>> firsts = new List<List<Symbol>>();
        List<List<Symbol>> folows = new List<List<Symbol>>();
        public void Printf_first()
        {
            int i = 0;
            foreach(List<Symbol> karel in firsts)
            {

                Console.Write("First[" + gramatika.Rules[i].ToString()+"] = ");
                if (karel.Count == 0)
                {
                    Console.Write("{e}");
                }
                foreach(Symbol pepa in karel)
                {
                    Console.Write(pepa);

                }
                Console.Write("\n");
                i++;
            }
            //Console.WriteLine(string.Join(",", rewritable_to_e));
        }
        public void Printf_follows()
        {
            int i = 0;
            foreach (List<Symbol> karel in folows)
            {
                Console.Write("Follow[" + gramatika.Nonterminals[i].ToString() + "] = ");
                foreach (Symbol pepa in karel)
                {
                    Console.Write(pepa);

                }
                Console.Write("\n");
                i++;
            }
        }
        public void Count_folows()
        {
            
            foreach (Nonterminal ter in gramatika.Nonterminals)
            {
                List<Symbol> folow = new List<Symbol>();
                for (int i = y.Count; i < x.Count; i++)
                {
                    if (tabulka[get_position(y, ter), get_position(x, x[i])])
                    {
                        
                        bool mam = false;
                        foreach (Symbol s in folow)
                        {
                            if (s.CompareTo(x[i]) == 0)
                            {
                                mam = true;
                            }
                        }
                        if (!mam)
                        {
                            folow.Add(x[i]);
                        }
                    }
                }
                foreach (Nonterminal n in this.rewritable_to_e)
                {
                    if (n.CompareTo(ter) == 0)
                    {
                        bool mam = false;
                        foreach (Symbol s in folow)
                        {
                            if (s.CompareTo(new Terminal("$")) == 0)
                            {
                                mam = true;
                            }
                        }
                        if (!mam)
                        {
                            folow.Add(new Terminal("$"));
                        }
                        
                    }
                }
                folows.Add(folow);

            }
        }
        public void Count_first()
        {

            string[] vypis = new string[gramatika.Rules.Count];
            //string vypis[gramatika.Rules.LHS.Count];
            /*for (int i = 0; i < gramatika.Rules.Count; i++)
            {
                vypis[i] = gramatika.Rules[i].LHS.Name;
                for (int j = 0; j < gramatika.Rules[i].RHS.Count; j++)
                {
                    if ((char)gramatika.Rules[i].RHS[j].ToString()[0] > 97 && (char)gramatika.Rules[i].RHS[j].ToString()[0] < 122)
                    {
                        Symbol haha = gramatika.Rules[i].RHS[j];
                        vypis[i] = vypis[i] + haha.ToString()+" ";
                    }
                    else
                    {
                        Symbol haha = gramatika.Rules[i].RHS[j];
                        
                        vypis[i] = vypis[i] + haha.ToString();
                    }
                }


            }*/
            
            foreach (Rule pravidlo in gramatika.Rules)
            {
                List<Symbol> first = new List<Symbol>();
                int pocet_neterminalu = 0;
                foreach (Symbol symbol in pravidlo.RHS)
                {

                    if (symbol.Name[0] > 97 && symbol.Name[0] < 120)
                    {
                        
                        bool mam = false;
                        foreach (Symbol s in first)
                        {
                            if (s.CompareTo(symbol) == 0)
                            {
                                mam = true; 
                            }
                        }
                        if (!mam)
                        {
                            first.Add(symbol);
                        }
                        
                        break;
                        
                    }
                    else
                    {
                        pocet_neterminalu++;
                        for (int i = y.Count; i < x.Count; i++)
                        {
                            if (tabulka[get_position(y, symbol), get_position(x, x[i])])
                            {
                                
                                bool mam = false;
                                foreach (Symbol s in first)
                                {
                                    if (s.CompareTo(x[i]) == 0)
                                    {
                                        mam = true;
                                    }
                                }
                                if (!mam)
                                {
                                    first.Add(x[i]);
                                }
                            }
                        }
                        
                    }
                }
                if (pravidlo.RHS.Count == pocet_neterminalu)
                {
                    first.Add(new Nonterminal("{e}"));
                }
                firsts.Add(first);
                
            }
            



        }
        public bool[,] tabulka;
        List<Nonterminal> rewritable_to_e;
        IGrammar gramatika;
        IList<Symbol> x;
        IList<Symbol> y;

        public Tabulka(IGrammar g)
        {
            tabulka = new bool[g.Nonterminals.Count,g.Nonterminals.Count+g.Terminals.Count];
            rewritable_to_e = new List<Nonterminal>();
            gramatika = g;
            empty_tabulka();
            x = new List<Symbol>();
            y = new List<Symbol>();
            foreach (Symbol s in g.Nonterminals)
            {
                y.Add(s);
                x.Add(s);
            }
            for (int i = 0; i < g.Terminals.Count; i++)
            {
                x.Add(g.Terminals[i]);
            }
            

        }
        public override string ToString()
        {
            string rs = " " + string.Join("", x)+"\n";
            for (int i = 0; i < y.Count; i++)
            {
                rs += y[i];
                for (int j = 0; j <x.Count ; j++)
                {
                    if (tabulka[i, j])
                    {
                        rs += '*';
                    }
                    else
                    {
                        rs += " ";
                    }
                    
                }
                rs += "\n";
            }
            return rs;

        }
        public int get_position(IList<Symbol> osa, Symbol p)
        {
            for(int i=0;i<osa.Count;i++)
            {
                if(osa[i].CompareTo(p)==0)
                {
                    return i;
                }
            }
            return -1;
            
        }

        public void empty_tabulka()
        {
            for (int i = 0; i < gramatika.Nonterminals.Count - 1; i++)
            {
                for (int j = 0; j < gramatika.Nonterminals.Count- 1+gramatika.Terminals.Count-1; j++)
                {
                    tabulka[i, j] = false;
                }
            }
        }
        public void Follows(Nonterminal fir, Nonterminal? prev)
        {
            
            if (fir == this.gramatika.StartingNonterminal)
            {
                this.rewritable_to_e.Add(fir);
            }
            foreach (var rule in gramatika.Rules)
            {

                if (rule.LHS != fir)
                {
                    continue;
                }
                int count = 1;
                foreach (var symbol in rule.RHS)
                {
                    if (symbol is Nonterminal)
                    {
                        if ((Nonterminal)symbol != fir)
                        {

                            if ((Nonterminal)symbol != prev)
                            {
                                Follows((Nonterminal)symbol, fir);
                            }

                            if (symbol == rule.RHS.Last())
                            {
                                tabulka[get_position(y, symbol), get_position(x, fir)]=true;
                                this.rewritable_to_e.Add((Nonterminal)symbol);
                            }

                            for (int i = count; i < rule.RHS.Count; i++)
                            {
                                if (rule.RHS[count] is Nonterminal)
                                {
                                    foreach (var rul in gramatika.Rules)
                                    {
                                        if (rul.LHS != rule.RHS[count])
                                        {
                                            continue;
                                        }
                                        foreach (var sym in rul.RHS)
                                        {

                                            tabulka[get_position(y, symbol), get_position(x, sym)] = true;

                                        }
                                    }
                                }
                                tabulka[get_position(y, symbol), get_position(x, rule.RHS[count])] = true;



                                foreach (var ter in gramatika.Nonterminals)
                                {

                                    if (tabulka[get_position(y, ter), get_position(x, symbol)])
                                    {
                                        for (int j = 0; j < x.Count; j++)
                                        {
                                            if (tabulka[get_position(y, symbol), get_position(x, x[j])])
                                            {

                                                tabulka[get_position(y, ter), get_position(x, x[j])]=true;
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                    count++;
                }
            }

        }
        public void Firsts(Nonterminal fir, Nonterminal? prev)
        {
            foreach (var rule in gramatika.Rules)
            {

                if (rule.LHS != fir)
                {
                    continue;
                }
                if (rule.RHS.Count == 0)
                {
                    tabulka[get_position(y, fir), get_position(x, fir)] = true;
                    rewritable_to_e.Add(fir);
                }
                foreach (var symbol in rule.RHS)
                {
                    if (symbol is Terminal)
                    {
                        tabulka[get_position(y, fir), get_position(x, symbol)] = true;
                        if (prev != null)
                        {
                            tabulka[get_position(y, prev), get_position(x, symbol)] = true;
                        }
                        break;
                    }
                    if (symbol is Nonterminal)
                    {
                        tabulka[get_position(y, fir), get_position(x, symbol)] = true;
                        if (prev != null)
                        {
                            tabulka[get_position(y, prev), get_position(x, symbol)] = true;
                        }
                        if ((Nonterminal)symbol != fir)
                        {
                            Firsts((Nonterminal)symbol, fir);
                            for (int i = 0; i <x.Count; i++)
                            {
                                if (tabulka[get_position(y,symbol),get_position(x, x[i])])
                                {
                                    tabulka[get_position(y, fir), get_position(x, x[i])] = true;
                                }
                            }
                        }
                        if (symbol == rule.RHS.Last())
                        {
                            rewritable_to_e.Add(fir);
                        }
                        bool mam = false;
                        foreach (Symbol x in rewritable_to_e)
                        {
                            if (x.CompareTo(symbol) == 0)
                            {
                                mam=true;
                                break;
                            }
                        }
                        if (mam)
                        {
                            continue;
                        }
                        break;
                    }


                }

            }



        }
    }
}
