using Grammar;
using System;
using System.Collections.Generic;

namespace Lab3
{
	public class GrammarOps
	{
		public IList<Nonterminal> EmptyNonterminals { get; } = new List<Nonterminal>();
		public GrammarOps(IGrammar g)
		{
			this.g = g;
			Tabulka tab = new Tabulka(g);
			Tabulka tab_follow = new Tabulka(g);
			Console.WriteLine("Firsts");
			tab.Firsts(g.StartingNonterminal,null);
			tab.Count_first();
			tab.Printf_first();
			//tab.empty_tabulka();
			Console.WriteLine("Follows");
			tab_follow.Follows(g.StartingNonterminal, null);
			tab_follow.Count_folows();
			tab_follow.Printf_follows();
			Console.WriteLine(tab);
		}


		private IGrammar g;
	}
}
