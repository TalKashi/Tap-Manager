using UnityEngine;
using System.Collections;

public class NamesUtilsScript {

	private string[] m_teamNames = {"Turf Toe","Gunslingers","HeAteMe","Soup-A-Stars","The Cereal Killers",
		"The Big Dawgs","Convicted Llamas","Smartinis","Bucket of Truth","Monkeys with Crayons",
		"Here for Beer","Alco Holics","Beer","Penguins","Ninjas in Paris",
		"Ebola Ballers","Kim Jong Worm","Kobe Wan Kenobi","D12","IDC FC"};


	private string[] m_firstNames = {"Hilton", 
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
		"Len",  
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

	private string[] m_lastNames = {"Mahmood",  
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


	public string GetFirstName()
	{
		return m_firstNames [Random.Range (0, m_firstNames.Length)];

	}

	public string GetLastName()
	{
		return m_lastNames [Random.Range (0, m_lastNames.Length)];
		
	}

	public string GetTeamNameInIndex(int i){
		if (i >= m_teamNames.Length) {
			return null;
		}
		return m_teamNames[i];
	}


}
