using System;
using System.IO;
using System.Text;

namespace Text_quest
{
    [Serializable]
    public class Model
    {
        public int NumberE { get; set; }
        string[] str { get; set; }
        private bool end = false;
        public bool End { get { return end; } set { end = value; } }
        private bool swordEvent = false;
        private bool endarmorEvent, endswordEvent, endpotionEvent = false;
        public bool EndArmorEvent { get { return endarmorEvent;} set { endarmorEvent = value; } } 
        public bool EndSwordEvent { get { return endswordEvent;} set { endswordEvent = value; } } 
        public bool EndpotionEvent { get { return endpotionEvent; } set { endpotionEvent = value;} }
        public bool SwordEvent
        {
            get
            {
                return swordEvent;
            }
            set
            {
                swordEvent = value;
            }
        }
        private bool potionEvent = false;
        public bool PotionEvent
        {
            get
            {
                return potionEvent;
            }
            set
            {
                potionEvent = value;
            }
        }
        private bool armorEvent = false;
        public bool ArmorEvent
        {
            get
            {
                return armorEvent;
            }
            set
            {
                armorEvent = value;
            }
        }

        public int IndexLine { get; set; }
        public string path { get; set; }
        public void LoadText(string path)
        {
            str = File.ReadAllLines(path, Encoding.UTF8);
        }
        public string ReadText(int index)
        {
            return str[index];
        }
        public int TextSize()
        {
            return str.Length;
        }
       
    }
}
