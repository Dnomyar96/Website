using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dnomyar.Data.MTG.Models;
using MySql.Data.MySqlClient;

namespace Dnomyar.Data.MTG
{
    public class DbHandler : IDbHandler
    {
        private bool connectionOpen;
        private MySqlConnection connection;

        public DbHandler()
        {
            StartConnection();
        }

        private void StartConnection()
        {
            connectionOpen = false;
            connection = new MySqlConnection()
            {
                ConnectionString = ConnectionString.CS
            };

            if (OpenLocalConnection())
            {
                connectionOpen = true;
            }
        }

        private bool OpenLocalConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Card GetCardById(int id)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand()
                {
                    Connection = connection,
                    CommandText = $"SELECT * FROM Cards WHERE Id = {id}"
                };
                MySqlDataReader reader = cmd.ExecuteReader();
                var card = new Card();

                try
                {
                    while (reader.Read())
                    {
                        card = GetCard(reader);
                    }

                    reader.Close();

                    return card;
                }
                catch (MySqlException e)
                {
                    string MessageString = "Read error occurred  / entry not found loading the Column details: "
                        + e.ErrorCode + " - " + e.Message + "; \n\nPlease Continue";
                    reader.Close();
                    return null;
                }
            }
            catch (MySqlException e)
            {
                string MessageString = "The following error occurred loading the Column details: "
                    + e.ErrorCode + " - " + e.Message;
                return null;
            }
        }

        public Card GetCardByName(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> GetCardsBySet(string set)
        {
            throw new NotImplementedException();
        }

        public string InsertCard(Card card)
        {
            try
            {
                var colors = "";

                if (card.Colors != null)
                    colors = string.Join(";", card.Colors);

                MySqlCommand cmd = new MySqlCommand()
                {
                    Connection = connection,
                    CommandText = $"INSERT INTO Cards VALUES " +
                        $"('', '{card.Name}', '{colors}', '{card.Rarity}', '{card.Set}', '{card.SetName}', '{card.Type}', '{card.Text}'," +
                        $" '{card.ImageUrl}', '{card.Loyalty}', '{card.ManaCost}', '{card.Power}', '{card.Toughness}', '{card.Flavor}')"
                };
                cmd.ExecuteNonQuery();

                return "";
            }
            catch (MySqlException e)
            {
                string MessageString = "The following error occurred loading the Column details: "
                    + e.ErrorCode + " - " + e.Message;
                return MessageString;
            }
        }

        public IEnumerable<Card> GetAllCards()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand()
                {
                    Connection = connection,
                    CommandText = $"SELECT * FROM Cards"
                };
                MySqlDataReader reader = cmd.ExecuteReader();
                var cards = new List<Card>();

                try
                {
                    while (reader.Read())
                    {
                        cards.Add(GetCard(reader));
                    }

                    reader.Close();

                    return cards;
                }
                catch (MySqlException e)
                {
                    string MessageString = "Read error occurred  / entry not found loading the Column details: "
                        + e.ErrorCode + " - " + e.Message + "; \n\nPlease Continue";
                    reader.Close();
                    return null;
                }
            }
            catch (MySqlException e)
            {
                string MessageString = "The following error occurred loading the Column details: "
                    + e.ErrorCode + " - " + e.Message;
                return null;
            }
        }

        private Card GetCard(MySqlDataReader reader)
        {
            var card = new Card();

            if (reader.IsDBNull(0) == false)
                card.Id = reader.GetInt32(0);

            if (reader.IsDBNull(1) == false)
                card.Name = reader.GetString(1);

            if (reader.IsDBNull(2) == false)
                card.Colors = reader.GetString(2).Split(';');

            if (reader.IsDBNull(3) == false)
                card.Rarity = reader.GetString(3);

            if (reader.IsDBNull(4) == false)
                card.Set = reader.GetString(4);

            if (reader.IsDBNull(5) == false)
                card.SetName = reader.GetString(5);

            if (reader.IsDBNull(6) == false)
                card.Type = reader.GetString(6);

            if (reader.IsDBNull(7) == false)
                card.Text = reader.GetString(7);

            if (reader.IsDBNull(8) == false)
                card.ImageUrl = reader.GetString(8);

            if (reader.IsDBNull(9) == false)
                card.Loyalty = reader.GetString(9);

            if (reader.IsDBNull(10) == false)
                card.ManaCost = reader.GetString(10);

            if (reader.IsDBNull(11) == false)
                card.Power = reader.GetString(11);

            if (reader.IsDBNull(12) == false)
                card.Toughness = reader.GetString(12);

            if (reader.IsDBNull(13) == false)
                card.Flavor = reader.GetString(13);

            return card;
        }
    }
}
