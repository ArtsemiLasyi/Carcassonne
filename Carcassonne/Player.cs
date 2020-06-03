using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace Carcassonne
{
    public class Player
    {
        public enum PlayerColor
        {
            Red,
            Blue,
        }

        public string Name { get; private set; }
        public PlayerColor Color { get; private set; }
        public int Points { get; private set; }
        public int FreeServants { get; private set; }

        public TcpClient tcpClient;
        public NetworkStream stream;

        public Cell currentCell = null;

        public bool isReady = false;
        public bool isAlive = true;

        private List<SentServant> sentServants;


        public Player(string name, PlayerColor color)
        {
            this.Name = name;
            this.Color = color;
            this.Points = 0;
            this.FreeServants = 7;
            this.sentServants = new List<SentServant>();
            this.tcpClient = new TcpClient();
        }

        public void AddPoints(int points)
        {
            Points += points;
        }

        public void Connect(string host, int port)
        {
            try
            {
                tcpClient.Connect(host, port);  //подключение клиента
                stream = tcpClient.GetStream(); // получаем поток

                string message = this.Name;
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);

                // запускаем новый поток для получения данных
                Task.Factory.StartNew(ReceiveMessage);
            }
            catch (Exception ex) { }
        }


        // отправка сообщений
        public void SendMessage(string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }


        // отправка объекта
        public void SendMessage(Cell cell)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, cell);
        }

        // получение сообщений
        public void ReceiveMessage()
        {
            while (isAlive)
            {
                try
                {
                    byte[] data = new byte[1024]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    if (message.Contains(GameGlobals.IAMREADY))
                    {
                        GameGlobals.playersReady += 1;
                        if (GameGlobals.playersReady == GameGlobals.MAXPLAYERS)
                        {
                            GameGlobals.GAMESTATE = GameGlobals.GameState.game;
                        }
                    }
                    GameGlobals.chat += message + "\r\n";
                }
                catch (Exception ex) { Disconnect(); }
            }
        }

        public void Disconnect()
        {
            if (stream != null)
                stream.Close();//отключение потока
            if (tcpClient != null)
                tcpClient.Close();//отключение клиента
            isAlive = false;
        }

    }
}
