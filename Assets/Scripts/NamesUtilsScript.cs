using UnityEngine;
using System.Collections;

public class NamesUtilsScript {

	private static string[] m_teamNames = {"AC Avocado","Carambola FC","HeAteMe","Soup-A-Stars","The Cereal Killers",
		"Hapoel Coconut","Vanilla United","Smartinis","Beitar Kiwi ","Maccabi Lychee",
		"HaKoah Mango","Alco Holics","Beer","Penguins","Ninjas in Paris",
		"Inter Papaya","Kim Jong Worm","Kobe Wan Kenobi","InGame All Stars","IDC FC"};


	private static string[] m_firstNames = {"Hilton", 
		"Stan",  
		"Tal",  
		"Elliot",  
		"Floyd",  
		"Cornell",  
		"Amos",  
		"Britt",  
		"Serge",  
		"Hector",  
		"Eran",  
		"Darron",  
		"Joan",  
		"Moli",  
		"Vincenzo",  
		"Dudi",  
		"Allan",  
		"Herschel",  
		"Vito",  
		"Doron",  
		"Rogelio",  
		"Solomon",  
		"Martin",  
		"Almog",  
		"Leland",  
		"Hubert",  
		"Dewey",  
		"Bryan",  
		"Collin",  
		"Gilad"};

	private static string[] m_lastNames = {"Mahmood",  
		"Boehmer",  
		"Sharlow",  
		"Vallarta",  
		"Sharp",  
		"Fennessey",  
		"Norvel",  
		"Southwood",  
		"Fried",  
		"Graham",  
		"Zdenek",  
		"Mendicino",  
		"Stoneman",  
		"Brainard",  
		"Sheely",  
		"Kashi",  
		"Moroz",  
		"Peles",  
		"Feiler",  
		"Bianchi",  
		"Youngman",  
		"Guercio",  
		"Kish",  
		"Riggio",  
		"Kos",  
		"Lisk",  
		"Cronk",  
		"Hannan",  
		"Schunk",
		"Demarcus",
		"Devine" }; 


	public static string GetFirstName()
	{
		return m_firstNames [Random.Range (0, m_firstNames.Length)];

	}

	public static string GetLastName()
	{
		return m_lastNames [Random.Range (0, m_lastNames.Length)];
		
	}

    public static string GetRandomName()
    {
        return string.Format("{0} {1}", GetFirstName(), GetLastName());
    }

	public static string GetTeamNameInIndex(int i){
		if (i >= m_teamNames.Length) {
			return null;
		}
		return m_teamNames[i];
	}


}
