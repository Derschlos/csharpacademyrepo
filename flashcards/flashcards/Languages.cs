using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcards
{
    internal class Languages
    {
        public Languages(int id, string name = null)
        {   
            _id = id;
            if (name == null)
            {
                _name = SqlDriver.loadLangName(_id);
            }
            else { _name = name; }
            correct = 0;
            loadCards();
        }
        private int _id;
        public int Id { get { return _id; } }
        private string _name;
        public string Name { get { return _name; } /*set { _name = value; }*/ }
        private Dictionary<int, Card> _cardStack = new Dictionary<int, Card>();
        public Dictionary<int, Card> CardStack { get { return _cardStack; } }
        public int correct {get;set;}
        private void loadCards()
        {

            var idList = SqlDriver.allCardsId(_id);
            for (int i = 0; i < idList.Count; i++)
            {   
                var id = Convert.ToInt32(idList[i]);
                _cardStack.Add(id, new Card(id));
            }
        }
        public List<object> expData()
        {
            return new List<object> { _name, _cardStack.Count().ToString() };
        }

    }
    internal class Card
    {
        public Card(int id)
        {
            Id = id;
            loadContent();
        }
        public int Id { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        private void loadContent()
        {
            var content = SqlDriver.cardContent(Id);
            Front = content[0].ToString();
            Back = content[1].ToString();
        }
        
    }

}
