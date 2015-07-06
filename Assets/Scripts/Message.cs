using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class Message
{
    private static readonly string sr_MessagesFilePath = Application.persistentDataPath + "/messages.dat";

    public static List<Message> LoadMessagesData()
    {
        Debug.Log("Loading Messages");

        List<Message> messages = new List<Message>();
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        if (File.Exists(sr_MessagesFilePath))
        {
            FileStream file = File.OpenRead(sr_MessagesFilePath);
            messages = (List<Message>) binaryFormatter.Deserialize(file);
            file.Close();
            Debug.Log("Loaded All Messages");
        }
        else
        {
            Debug.Log("Messages file was not found");
        }

        return messages;
    }

    public static void SaveMessagesData(List<Message> i_Messages)
    {
        Debug.Log("Saving Messages");
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream file = File.Create(sr_MessagesFilePath);
        binaryFormatter.Serialize(file, i_Messages);
        file.Close();

        Debug.Log("Messages Saved");
    }

    private string m_Header;
    private string m_Content;
    private bool m_HasReadMessage;

    public string Header
    {
        get { return m_Header;}
    }

    public string Content
    {
        get
        {
            return m_Content;
        }
    }

    public bool HasReadMessage
    {
        get
        {
            return m_HasReadMessage;
        }
    }

    public Message(string i_Header, string i_Content)
    {
        m_Header = i_Header;
        m_Content = i_Content;
        m_HasReadMessage = false;
    }
}