using UnityEngine;
using System.Collections;

public class NamesUtilsScript {

	private string[] m_teamNames = {"Turf Toe","Gunslingers","HeAteMe","Soup-A-Stars","The Cereal Killers",
		"The Big Dawgs","Convicted Llamas","Smartinis","Bucket of Truth","Monkeys with Crayons",
		"Here for Beer","Alco Holics","Beer","Penguins","Ninjas in Paris",
		"Ebola Ballers","Kim Jong Worm","Kobe Wan Kenobi","D12","IDC FC"};


	private string[] m_firstNames = {"Hilton", 
		"Stan",  
		"Clark",  
		"Elliot",  
		"Floyd",  
		"Cornell",  
		"Kristopher",  
		"Britt",  
		"Leon",  
		"Hector",  
		"Kristofer",  
		"Darron",  
		"Joan",  
		"Len",  
		"Vincenzo",  
		"Elden",  
		"Allan",  
		"Herschel",  
		"Vito",  
		"Hong",  
		"Rogelio",  
		"Solomon",  
		"Martin",  
		"Marshall",  
		"Leland",  
		"Hubert",  
		"Dewey",  
		"Bryan",  
		"Collin",  
		"Max"};

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
		"Schulman",  
		"Moroz",  
		"Morse",  
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
