using System;

namespace SlothUtils
{
    public static class TimeUtils
    {
        #region 时间相关 & 倒计时
        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <param name="formart"></param>
        /// <returns></returns>
        public static string Time2String(string formart = "yyyy-MM-dd_HH.mm.ss")
        {
            return DateTime.Now.ToString(formart);
        }

        /// <summary>
        /// Get Unix Time By Current Data Time
        /// </summary>
        /// <returns>double</returns>
        public static double OnGetUnixTimeByCurDataTime()
        {
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (System.DateTime.Now - startTime).TotalSeconds;
            return intResult;
        }

        /// <summary>
        /// Get Unix Time
        /// </summary>
        /// <param name="_t"></param>
        /// <returns></returns>
        public static double OnGetUnixTimeByDataTime(System.DateTime _t)
        {
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (_t - startTime).TotalSeconds;
            return intResult;
        }

        public static string TimeStamp2TimeString(long timestamp, string tforamt = "t")
        {
            DateTime st = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            st.AddSeconds(timestamp);

            return st.ToString(tforamt);
        }

        ///<summary>
        ///Get DateTime String By Unix
        ///</summary
        ///<param name="t">Unix</param>
        ///<returns>String</returns>
        public static string OnParseTimeSeconds(Int64 t)
        {
            string r = "";
            int nData, nHour, nMinute, nSecond;
            if (t >= 86400) //Date,
            {
                nData = Convert.ToInt16(t / 86400);
                nHour = Convert.ToInt16((t % 86400) / 3600);
                nMinute = Convert.ToInt16((t % 86400 % 3600) / 60);
                nSecond = Convert.ToInt16(t % 86400 % 3600 % 60);
                r = nData + ("D:") + nHour + ("H:") + nMinute + ("M:") + nSecond + ("S");

            }
            else if (t >= 3600)//Hour,
            {
                nHour = Convert.ToInt16(t / 3600);
                nMinute = Convert.ToInt16((t % 3600) / 60);
                nSecond = Convert.ToInt16(t % 3600 % 60);
                r = nHour + ("H:") + nMinute + ("M:") + nSecond + ("S");
            }
            else if (t >= 60)//Minute
            {
                nMinute = Convert.ToInt16(t / 60);
                nSecond = Convert.ToInt16(t % 60);
                r = nMinute + ("M:") + nSecond + ("S");
            }
            else
            {
                nSecond = Convert.ToInt16(t);
                r = nSecond + ("S");
            }
            return r;
        }

        public static DateTime GetDeadline(long start, long duration)
        {
            DateTime localUtc = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return localUtc + TimeSpan.FromSeconds(start + duration);
        }

        /* Eg：
         * private void ShowUpdateTime()
            {
                // Show Legal Time
                if (m_bIsLegalTime)
                {
                    m_fTempTimer -= Time.fixedDeltaTime;
                    if (m_fTempTimer <= 0)
                    {
                        m_fTempTimer = 1;
                        m_dRemainTime -= m_fTempTimer;
                        _txtTimer.text = Helper.OnParseTimeSeconds((Int64)m_dRemainTime);

                        m_nClientStartTime++;
                        m_bIsLegalTime = m_nClientStartTime < m_nEndTime;
                        if (!m_bIsLegalTime) _txtTimer.text = STROVER;
                    }
                }

                m_fRefreshTimer -= Time.fixedDeltaTime;
                if (m_fRefreshTimer < 0)
                {
                    m_fRefreshTimer = 30;
                    RefreshDesignByTimer();
                }
            }
         */

        #endregion
    }
}
