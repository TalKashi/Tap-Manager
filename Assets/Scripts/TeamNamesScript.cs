using UnityEngine;
using System.Collections;

public class TeamNamesScript : MonoBehaviour {

	private string[] m_names = {"Turf Toe","Gunslingers","HeAteMe","Soup-A-Stars","The Cereal Killers",
		"The Big Dawgs","Convicted Llamas","Smartinis","Bucket of Truth","Monkeys with Crayons",
		"Here for Beer","Alco Holics","Beer","Penguins","Ninjas in Paris",
		"Ebola Ballers","Kim Jong Worm","Kobe Wan Kenobi","D12","IDC FC"};


	public string GetNameInIndex(int i){
		if (i >= m_names.Length) {
			return null;
		}
		return m_names[i];
	}


}
