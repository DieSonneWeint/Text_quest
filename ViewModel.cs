using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Net.Http.Headers;
using System.Security.Policy;

namespace Text_quest
{
    public class ViewModel
    {
       
         Model model = new Model();
         XmlSerializer xmlSerializable = new XmlSerializer(typeof(Model));
        private string LoadImage(int index) // Загрузка изображения
        {
            if (index == 1) return "Images/Sword_Event.png";
            if (index == 2) return "Images/Shop.png";
            if (index == 3) return "Images/Alch.jpg";
            if (index == 4) return "Images/Forest.jpg";
            return "Images/Image1.png";
        }
        public string NextLine() // переход к следующей строке
        {
            
            if(model.TextSize()-1 != model.IndexLine) model.IndexLine++;
            return model.ReadText(model.IndexLine);
            
        }   
        public int CheckOpenEvents()  // Проверка запуска событий 
        {
            if (model.IndexLine == model.TextSize() - 1 && !model.EndSwordEvent && !model.EndpotionEvent && !model.EndArmorEvent && !model.End && (!model.ArmorEvent && !model.PotionEvent && !model.SwordEvent)) { model.NumberE = 0; return 1; }
            if (model.IndexLine == model.TextSize() - 1 && model.SwordEvent && !model.EndSwordEvent) { model.NumberE = 1; return 2; }
            if (model.IndexLine == model.TextSize() - 1 && model.ArmorEvent && !model.EndArmorEvent) { model.NumberE = 2; return 3; }
            if (model.IndexLine == model.TextSize() - 1 && model.PotionEvent && !model.EndpotionEvent) { model.NumberE = 3; return 4; }
            if (model.EndArmorEvent && model.EndpotionEvent && model.EndSwordEvent && !model.End) { model.NumberE = 4; model.End = true; return 5; };
            return 0;
        }
        public string BackLine() // переход к предыдущей строке 
        {
            if (model.IndexLine != 0) model.IndexLine--;
            return model.ReadText(model.IndexLine);
        }     
        private void Event(int NumberEvent) // загрузка текста события 
        {
            if (NumberEvent== 0) model.path = "Text.txt";
            if (NumberEvent == 1)
            {
                model.SwordEvent = true;
                model.path = "SwordEvent.txt";
            }
            if (NumberEvent == 2)
            {
                model.ArmorEvent = true;
                model.path = "ArmorEvent.txt";
            }
            if (NumberEvent == 3)
            {
                model.PotionEvent = true;
                model.path = "AlchEvent.txt";
            }
            if (NumberEvent == 4) model.path = "END.txt";
            if (NumberEvent == -1) model.path = "0.txt";
     
        }
        public string LoadEvent(int NumberEvent) // загрузка события 
        {
            string image;
            Event(NumberEvent);
            model.IndexLine = -1;
            model.LoadText(model.path);
            model.NumberE = NumberEvent;
            image = LoadImage(NumberEvent);
            return image;
        }
        public int Sword_Event(int proc) // проверка для события с заточкой
        {
            int point= 0;
            if (proc  < 40) 
            {
                point = 1;  
            }
            if (proc >=40 && proc <=60 )
            {
                model.EndSwordEvent = true;
                model.SwordEvent = false;
                point = 2;
            }
            if (proc > 60) 
            {
                point = 3;
            }
            return point;
        }
        public int Potion_Event(string str) // проверка для события с зельем
        {
            if (Equals(str ,"312"))
            {
                model.EndpotionEvent = true;
                model.PotionEvent = false;
                return 1;
            }
            else return 2;
        }
        public bool CheckEvent(int indexEvent) // проверка на окончание событий
        {        
            if (indexEvent == 1) return model.EndSwordEvent;
            if (indexEvent == 2) return model.EndArmorEvent;
            if (indexEvent == 3) return model.EndpotionEvent;
            if (indexEvent == 4) return model.SwordEvent;
            if (indexEvent == 5) return model.ArmorEvent;
            if (indexEvent == 6) return model.PotionEvent;
            return false;
        }
        public void ArmorEvemt()
        {
            model.ArmorEvent = false;
            model.EndArmorEvent = true;

        }
        public void SaveTemp()
        {     
            FileWriter fs = new FileWriter("temp.xml");
            xmlSerializable.Serialize(fs,model);
            fs.Close();
        }
        public ImageBrush LoadTemp()
        {
            if (File.Exists("temp.xml"))
            {
                FileStream fs = new FileStream("temp.xml", FileMode.OpenOrCreate);              
                model = xmlSerializable.Deserialize(fs) as Model;
                ImageBrush image = new ImageBrush(new BitmapImage(new Uri(LoadImage(model.NumberE), UriKind.Relative)));
                model.LoadText(model.path);
                fs.Close();
                return image;
            }
            else
            {
                ImageBrush image = new ImageBrush(new BitmapImage(new Uri(LoadEvent(0), UriKind.Relative)));
                return image;
            }
        }
    }
}
