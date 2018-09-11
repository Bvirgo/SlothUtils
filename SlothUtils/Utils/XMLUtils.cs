using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SlothUtils
{
    public static class XMLUtils
    {

        #region XML
        /// <summary>
        /// Read XML 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sPath"></param>
        /// <returns></returns>
        public static T ReadXML<T>(string sPath)
        {
            if (!File.Exists(sPath))
            {
                return default(T);
            }
            // 读取XML文件流：融化的铁水
            FileStream pReadStream = new FileStream(sPath, FileMode.Open);
            // 反射，构造出T对应的XML结构：造模型
            // XmlSerializer有出现内存泄露的风险，它的构造必须用以下的方式，否则就会出现内存泄露
            XmlSerializer xml = new XmlSerializer(typeof(T));
            // 反序列化：铁水流入指定模型，打造出对应物品
            T data = (T)xml.Deserialize(pReadStream);
            pReadStream.Close();
            return data;
        }

        /// <summary>
        /// Save Data To XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="sPath"></param>
        public static void WriteXML<T>(T data, string sPath)
        {
            // 指定编码格式uft8
            UTF8Encoding utf8 = new UTF8Encoding(false);
            // 按指定编码格式，在指定路径下，创建写入流
            StreamWriter pWriter = new StreamWriter(sPath, false, utf8);
            // 反射，根据T类型，创建对象的XML模型
            XmlSerializer xs = new XmlSerializer(typeof(T));
            xs.Serialize(pWriter, data);
            if (pWriter != null)
            {
                pWriter.Close();
            }
        }
        /*Eg:
    1、XML就是三个东西：标签 、属性（属性必须是：属性名 = “属性值”，属性值必须加引号，解析时候可以随意指定类型）、文本(string用''，
    解析时候不能随意指定类型)
         <root>
              <book  name = "葵花宝典">
                 xxxxxxxxxx
              </book>
         </root>
    2、标签可以嵌套标签，可以嵌套文本
    3、XML对大小写敏感

         * // 定义XML对象的类结构
        // XmlRootAttribute:声明根标签，大小写必须和xml中一致
        // XmlElementAttribute:声明子标签
        // XmlAttribute:声明属性

        //Eg1:只有属性
        //<skills>
        // <skill name ="葵花宝典" id="1" damage="99" cd ="5">
        // </skill>
        // <skill name ="长拳" id="1" damage="99" cd ="5">
        // </skill>
        //</skills>

        // Eg2:属性嵌套标签
        // <skills>
        //    <skill name ="葵花宝典">
        //        <id>1</id>
        //        <damage>99</damage>
        //        <cd>5</cd>
        //    </skill>
        //    <skill name ="太极拳">
        //        <id>2</id>
        //        <damage>1000</damage>
        //        <cd>10</cd>
        //    </skill>
        //</skills>

        // Eg3:一个根下包含多个不同子：skill 和 master
        // <skills>
        //    <skill name ="葵花宝典">
        //        <id>1</id>
        //        <damage>99</damage>
        //        <cd>5</cd>
        //    </skill>
        //    <skill name ="太极拳">
        //        <id>2</id>
        //        <damage>1000</damage>
        //        <cd>10</cd>
        //    </skill>
       //   <master name ="张三丰" >
       //      < skill>太极拳 </skill>
       //    </master>
       //  </skills>

        [XmlRootAttribute( "skills")]
        public class Skills
        {
            public Skills(){ }
            [ XmlElementAttribute("skill" )]
            public List< Skill> pSkill = new List< Skill>();
            [ XmlElementAttribute("master" )]// 对应Eg3
            public List< Master> pMaster = new List< Master>();
        }
        [XmlRootAttribute( "skill")]
        public class Skill
        {
            public Skill() {}

            [ XmlAttribute("name" )]
            public string m_sName{ get; set; }

            // [XmlAttribute("id")] 对应Eg1
            [ XmlElementAttribute("id" )]// 对应Eg2
            public int m_nID { get; set; }

            //[XmlAttribute("damage")]
            [ XmlElementAttribute("damage" )]
            public int m_nDamage { get; set; }

            //[XmlAttribute("cd")]
            [ XmlElementAttribute("cd" )]
            public int m_nCD { get; set; }
        }
        [XmlRootAttribute( "master")]
        public class Master
        {
            public Master() { }
            [ XmlAttribute("name" )]
            public string m_sName { get; set;}
            // 改进：没有必要同时用成员变量和属性，直接用属性
            //public string Name
            //{
            //    get { return m_sName; }
            //    set { m_sName = value; }
            //}
            [ XmlElementAttribute("skill" )]
            public string m_sSkill{ get; set; }
        }
    }
            static void Main( string[] args)
            {
                Skills pSkills;
                pSkills = ReadXML< Skills>( "SkillInfo.xml");
                for ( int i = 0; i < pSkills.pSkill.Count; ++i )
                {
                    Skill curSkill = pSkills.pSkill[i];
                    Console.WriteLine(curSkill.m_sName+ "ID:"+ curSkill.m_nID+"Damage:" +curSkill.m_nDamage);
                }
                for ( int i = 0; i < pSkills.pMaster.Count; ++i)
                {
                    Master curSkill = pSkills.pMaster[i];
                    Console.WriteLine(curSkill.m_sName + "Skill:" + curSkill.m_sSkill);
                }
                Console.ReadKey();
            }
         * 
         */
        #endregion
    }
}
