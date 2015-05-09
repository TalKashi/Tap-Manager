using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class Bucket
{   
    float m_valueForSecond;
    int m_maxAmount;
    float m_currentMoneyInBucket;
    private int m_level;

    public string ID { get; set; }

    public long LastFlush { get; set; }

    public Bucket(int i_maxAmount, int i_totalTimeToCollectInSeconds)
    {
        m_valueForSecond = (float)i_maxAmount / i_totalTimeToCollectInSeconds;
        m_maxAmount = i_maxAmount;
        m_currentMoneyInBucket = 0;
    }

    public Bucket()
    {
        // Nothing to do
    }

    public void SetValuePerSecond(float i_Value)
    {
        m_valueForSecond = i_Value;
    }

    public int EmptyBucket()
    {
        if (m_currentMoneyInBucket < m_maxAmount)
        {
            return 0;
        }
        int moneyToReturn = m_maxAmount;
        Debug.Log("moneyToReturn=" + moneyToReturn + ";m_currentMoneyInBucket=" + m_currentMoneyInBucket);
        m_currentMoneyInBucket = 0;

        Debug.Log("Emptying bucket. Returning " + moneyToReturn + " coins.");
        return moneyToReturn;
    }

    public void AddMoneyToBucket(float i_deltaTime)
    {
        //Debug.Log("Adding money to bucket of " + i_deltaTime + " seconds");
        if (m_currentMoneyInBucket == m_maxAmount)
        {
            Debug.Log("Can't add more money to bucket. Bucket is full!");
            return;
        }
        //Debug.Log("deltaTime=" + i_deltaTime + ";adding=" + m_valueForSecond * i_deltaTime);
        m_currentMoneyInBucket += m_valueForSecond * i_deltaTime;

        if (m_currentMoneyInBucket > m_maxAmount)
        {
            m_currentMoneyInBucket = m_maxAmount;
            Debug.Log("Bucket is full! (" + m_currentMoneyInBucket + ")");
        }


        //Debug.Log("New value in bucket: " + m_currentMoneyInBucket);
    }

    //public void SetMoneyInBucketAfterConnect(DateTime i_Now)
    //{
    //    float deltaTime = i_Now.Subtract(LastFlush).Milliseconds;
    //    AddMoneyToBucket(deltaTime);
    //}

    public void UpgradeBucket(int i_newLevel, int i_maxAmount, int i_totalTimeToCollectInSeconds)
    {
        m_level = i_newLevel;
        m_valueForSecond = (float)i_maxAmount / i_totalTimeToCollectInSeconds;
        m_maxAmount = i_maxAmount;
    }

    public int GetMoneyInBucket()
    {
        return (int)m_currentMoneyInBucket;
    }

    public float GetMoneyInBucketForDebug()
    {
        return m_currentMoneyInBucket;
    }

    // Returns the time left until the bucket is full or TimeSpan.MaxValue if it will never be full
    public TimeSpan GetTimeUntilBucketIsFull()
    {
        if (m_valueForSecond == 0)
        {
            return TimeSpan.MaxValue;
        }
        // The formula
        float moneyLeftToFill = m_maxAmount - m_currentMoneyInBucket;
        float timeLeftInSeconds = moneyLeftToFill * (1 / m_valueForSecond);

        return TimeSpan.FromSeconds(timeLeftInSeconds);
    }

    public int GetLevel()
    {
        return m_level;
    }

    public void SetLevel(int i_Level)
    {
        m_level = i_Level;
    }

    public float GetValuePerSecond()
    {
        return m_valueForSecond;
    }

    public int GetMaxAmount()
    {
        return m_maxAmount;
    }

    public void SetMaxAmount(int i_MaxAmount)
    {
        m_maxAmount = i_MaxAmount;
    }

    public bool IsFull()
    {
        return m_currentMoneyInBucket.Equals(m_maxAmount);
    }

    public void SetMoneyToZero()
    {
        m_currentMoneyInBucket = 0;
    }
}
