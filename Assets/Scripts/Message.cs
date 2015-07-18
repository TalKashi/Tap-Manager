using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class Inbox
{
    private static readonly string sr_InboxFilePath = Application.persistentDataPath + "/";

    private int m_NumOfUnreadMessages;
    private List<Message> m_Messages;

    public int TotalMessages { get; private set; }

    public static Inbox LoadMessagesData()
    {
        Debug.Log("Loading Messages");
        string path = sr_InboxFilePath + PlayerPrefs.GetString("id") + ".dat";

        Inbox messages = new Inbox();
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        if (File.Exists(path))
        {
            FileStream file = File.OpenRead(path);
            messages = (Inbox)binaryFormatter.Deserialize(file);
            file.Close();
            Debug.Log("Loaded All Messages");
        }
        else
        {
            Debug.Log("Messages file was not found");
        }

        return messages;
    }

    public static void SaveMessagesData(Inbox i_Messages)
    {
        Debug.Log("Saving Messages");
        string path = sr_InboxFilePath + PlayerPrefs.GetString("id") + ".dat";
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream file = File.Create(path);
        binaryFormatter.Serialize(file, i_Messages);
        file.Close();

        Debug.Log("Messages Saved");
    }

    public bool HasUnreadMessages
    {
        get { return m_NumOfUnreadMessages > 0; }
    }

    public Inbox()
    {
        m_Messages = new List<Message>();
    }

    public void AddNewMessage(Message i_NewMessage)
    {
        m_NumOfUnreadMessages++;
        TotalMessages++;
        m_Messages.Add(i_NewMessage);
    }

    public Message this[int i_Idx]
    {
        get
        {
            //int currectIndx = m_Messages.Count - i_Idx - 1;
            if (m_Messages[i_Idx].HasReadMessage == false)
            {
                m_Messages[i_Idx].HasReadMessage = true;
                m_NumOfUnreadMessages--;
            }
            return m_Messages[i_Idx];
        }
    }

    public List<Message> Messages { get { return m_Messages;} } 
}

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
        set { m_HasReadMessage = value; }
    }

    public Message(string i_Header, string i_Content)
    {
        m_Header = i_Header;
        m_Content = i_Content;
        m_HasReadMessage = false;
    }
}